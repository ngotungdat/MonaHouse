using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Extensions;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Request;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class RoomReceiptService : DomainService<RoomReceipt, RoomReceiptSearch>, IRoomReceiptService
    {
        protected IElectricWaterBillService electricWaterBillService;
        protected IRoomUtilitiService roomUtilitiService;
        protected IRoomService roomService;
        protected IUserService userService;
        protected INotificationUserService notificationUserService;
        protected IRoomContractRepresentativeService roomContractRepresentativeService;
        protected readonly IAppDbContext Context;

        public RoomReceiptService(IAppUnitOfWork unitOfWork, IMapper mapper
            , IAppDbContext appDbContext
            , IElectricWaterBillService ElectricWaterBillService
            , IRoomUtilitiService RoomUtilitiService
            , IRoomService RoomService
            , IUserService UserService
            , INotificationUserService notificationUserService
            , IRoomContractRepresentativeService roomContractRepresentativeService
            ) : base(unitOfWork, mapper)
        {
            this.userService = UserService;
            this.electricWaterBillService = ElectricWaterBillService;
            this.roomUtilitiService = RoomUtilitiService;
            this.roomService = RoomService;
            this.notificationUserService = notificationUserService;
            this.roomContractRepresentativeService = roomContractRepresentativeService;
            this.Context = appDbContext;
        }
        protected override string GetStoreProcName()
        {
            return "RoomReceipt_GetPagingData";
        }
        public override async Task<bool> CreateAsync(RoomReceipt item)
        {
            // lấy thông tin phòng
            Room room = roomService.GetById((int)item.RoomId);
            // lấy ra danh sách ghi điện theo tháng và năm
            IList<ElectricWaterBill> ElectricWaterBills = new List<ElectricWaterBill>();
            if (room.ElectricWaterPackage == 0) {
                ElectricWaterBills = await electricWaterBillService.GetAsync(p => p.WriteDate.Value.Day > room.DateInToRoom.Value.Day && p.RoomId == item.RoomId && p.WriteDate.Value.Month == item.Date.Value.Month && p.WriteDate.Value.Year == item.Date.Value.Year);
            }
            else if (room.ElectricWaterPackage ==1) {
                ElectricWaterBills = new List<ElectricWaterBill>();
            }
            // kiểm tra ngày dọn vào của người đại diện trong phòng để tính toán điện nước
            // chưa có chức năng dọn vào !!!!!!!!!!!!!!!

            // tiền điện nước tháng đó
            double? electric_bill = 0;
            double? water_bill = 0;
            foreach (ElectricWaterBill electricWaterBill in ElectricWaterBills)
            {
                if (electricWaterBill.ElectricBill != null)
                {
                    electric_bill += electricWaterBill.ElectricBill;
                }
                if (electricWaterBill.WaterBill != null)
                {
                    water_bill += electricWaterBill.WaterBill;
                }
            }
            // lấy danh sách dịch vụ của phòng
            IList<RoomUtilities> roomUtilities = await roomUtilitiService.GetAsync(p => p.RoomId == item.RoomId);
            // tiền dich vụ của phòng
            double? room_utilitie_bill = 0;
            foreach (RoomUtilities roomUtilitie in roomUtilities)
            {
                if (roomUtilitie.Price != null)
                {
                    room_utilitie_bill += roomUtilitie.Price;
                }

            }
            // lấy giá phòng
            double? room_price_bill = 0;
            if (room_price_bill != null)
            {
                room_price_bill = roomService.GetById((int)item.RoomId).Price;
            }
            // tổng giá tiền phải chi chả theo tháng 
            double total_money_bill = 0;
            total_money_bill = (double)(room_price_bill + electric_bill + water_bill + room_utilitie_bill);

            //
            item.ElectricBill = electric_bill;
            item.WaterBill = water_bill;
            item.RoomUtilitieBill = room_utilitie_bill;
            item.RoomBill = room_price_bill;
            item.TotalBill = total_money_bill;
            item.FinalBill = total_money_bill + item.PlusBill - item.SubBill;
            item.MoneyRecive = 0;
            item.MoneyDebtRoomReceipt = item.FinalBill;
            // khi tính tiền xong tạo phiếu thu đợ thanh thanh toán
            bool res = await base.CreateAsync(item);
            //Nếu tạo phiếu tính tiền thành công -> tạo thông báo đòi tiền khách hàng
            if (res==true) {
                // thông tin khách thuê
                Users userInRoom= userService.GetById((int)room.UserInRoomRepresentative);
                var user = LoginContext.Instance.CurrentUser;
                NotificationUser notificationUser = new NotificationUser();
                notificationUser.UsersId = room.UserInRoomRepresentative;
                // tiêu đề thông báo
                notificationUser.Title = "Thông báo thu tiền Phòng";
                // nội dung thông báo
                notificationUser.Content = "Thông báo thu tiền phòng: số tiền bạn phải đóng là : "+item.FinalBill+" đ";
                notificationUser.isSeen = false;
                notificationUser.Active = true;
                notificationUser.Deleted = false;
                notificationUser.CreatedBy = user.UserName;
                notificationUser.TenantId = userInRoom.TenantId;
                await notificationUserService.CreateAsync(notificationUser);
            }

            return res;
        }

        public override async Task<bool> UpdateAsync(RoomReceipt item)
        {
            // kiểm tra user còn nợ ko
            Users user = userService.GetById(Int32.Parse(item.UserId));
            RoomReceipt roomReceipt = this.GetById(item.Id);
            // kiểm tra user đã hết trả trước chưa
            // status = 0 la hop dong chua het han
            List<RoomContractRepresentative> roomContractRepresentatives = (List<RoomContractRepresentative>)await roomContractRepresentativeService.GetAsync(p=>p.RoomId==item.RoomId && p.UserId==user.Id && p.Status==0 );
            if (roomContractRepresentatives.Count > 0)
            {
                double UserMoneyLeft = (double)roomContractRepresentatives[0].UserMoney - (double)roomReceipt.FinalBill;
                if (UserMoneyLeft > 0)
                {
                    item.MoneyDebtRoomReceipt = 0;
                    roomContractRepresentatives[0].UserMoney = UserMoneyLeft + item.MoneyRecive;
                    item.MoneyRecive = roomReceipt.FinalBill;
                    item.Status = 2;
                    item.Active = true;

                }
                if (UserMoneyLeft == 0)
                {
                    item.MoneyDebtRoomReceipt = 0;
                    roomContractRepresentatives[0].UserMoney = UserMoneyLeft + item.MoneyRecive;
                    item.MoneyRecive = roomReceipt.FinalBill;
                    item.Status = 2;
                    item.Active = true;

                }
                if (UserMoneyLeft < 0)
                {
                    //RoomReceipt roomReceipt = this.GetById(item.Id);
                    item.Active = true;
                    item.MoneyRecive = item.MoneyRecive + roomContractRepresentatives[0].UserMoney;
                    roomContractRepresentatives[0].UserMoney = 0;
                    // nếu tài khoản hết tiền
                    double tmp = (double)item.MoneyRecive + UserMoneyLeft;
                    // => số tiên user nợ được là tmp

                    item.MoneyDebtRoomReceipt = (double)(tmp);
                    if (item.MoneyDebtRoomReceipt < 0)
                    {
                        item.Status = 1; // 0:chưa thanh toán 1:còn thiếu 2: đã thanh toán
                                         // user chưa trả hết hóa đơn tiền phòng => tăng số tiền nợ của user
                        user.DebtMoney = user.DebtMoney - item.MoneyDebtRoomReceipt;
                    }
                    if (item.MoneyDebtRoomReceipt == 0)
                    {
                        item.Status = 2;
                    }
                    if (item.MoneyDebtRoomReceipt > 0)
                    {
                        if (user.DebtMoney > 0)
                        {
                            // trừ nợ
                            var val = user.DebtMoney - item.MoneyDebtRoomReceipt; // ở đây do trả dư 
                            if (val > 0)
                            {
                                // user sau khi trả nợ vẫn còn nợ
                                item.MoneyDebtRoomReceipt = 0;
                                item.Status = 2;
                                user.DebtMoney = val;

                            }
                            if (val == 0)
                            {
                                // user trả hết nợ
                                item.MoneyDebtRoomReceipt = 0;
                                item.Status = 2;
                                user.DebtMoney = 0;
                            }
                            if (val < 0)
                            {
                                // user trả dư số tiền
                                item.MoneyDebtRoomReceipt = 0;
                                roomContractRepresentatives[0].UserMoney = -val;
                                item.Status = 2;
                                user.DebtMoney = 0;
                            }
                        }
                    }
                }
            }
            else {
                throw new Exception("phòng không có hợp đồng dọn vào");
            }
            // update số dư
            var res = await roomContractRepresentativeService.UpdateAsync(roomContractRepresentatives[0]);
            if (res == false) { throw new Exception(" lỗi trong quá trình xử lý"); }

            // update tiền nợ
            var rs = await userService.UpdateAsync(user);
            if (rs == false) { throw new Exception(" lỗi trong quá trình xử lý"); }
            
            // update hóa đươn thanh toán
            rs = await base.UpdateAsync(item);
            // khi thu tiền thành công -> tạo thông báo đến khách hàng là đã nhận bao nhiêu tiền
            if (rs== true) {
                // thông tin khách thuê
                var userCurrentLogin = LoginContext.Instance.CurrentUser;
                Users userlogin = userService.GetById(userCurrentLogin.UserId);
                NotificationUser notificationUser = new NotificationUser();
                notificationUser.UsersId =Int32.Parse(roomReceipt.UserId);
                // tiêu đề thông báo
                notificationUser.Title = "Thông báo nhận tiền phòng";
                // nội dung thông báo
                notificationUser.Content = "Thông báo nhận tiền phòng: số chúng tôi nhận được là : " + item.MoneyRecive + " đ";
                notificationUser.isSeen = false;
                notificationUser.Active = true;
                notificationUser.Deleted = false;
                notificationUser.CreatedBy = userlogin.UserName;
                notificationUser.TenantId = userlogin.TenantId;
                await notificationUserService.CreateAsync(notificationUser);
            }
            return rs;
        }

        public Task<List<RoomReceiptsForYearAndBranchId>> GetRoomReceiptsWitthBranchAndYear(int branchId, int year, int userId)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("BranchId", branchId));
            sqlParameters.Add(new SqlParameter("Year", year));
            sqlParameters.Add(new SqlParameter("TenantId", userId));

            SqlParameter[] parameters = sqlParameters.ToArray();
            return Task.Run(() =>
            {
                List<RoomReceiptsForYearAndBranchId> pagedList = new List<RoomReceiptsForYearAndBranchId>();
                DataTable dataTable = new DataTable();
                SqlConnection connection = null;
                SqlCommand command = null;
                try
                {
                    connection = (SqlConnection)Context.Database.GetDbConnection();
                    command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = "Get_RoomReceiptsWithYearAndBranchId";
                    command.Parameters.AddRange(parameters);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);
                    pagedList = MappingDataTable.ConvertToList<RoomReceiptsForYearAndBranchId>(dataTable);

                    return pagedList;
                }
                finally
                {
                    if (connection != null && connection.State == System.Data.ConnectionState.Open)
                        connection.Close();

                    if (command != null)
                        command.Dispose();
                }
            });
        }
    }
}
