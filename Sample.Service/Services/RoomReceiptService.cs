using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Request;
using Sample.Service.Services.DomainServices;
using System;
using System.Collections.Generic;
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
        protected IRoomContractRepresentativeService roomContractRepresentativeService;

        public RoomReceiptService(IAppUnitOfWork unitOfWork, IMapper mapper
            , IElectricWaterBillService ElectricWaterBillService
            , IRoomUtilitiService RoomUtilitiService
            , IRoomService RoomService
            , IUserService UserService
            , IRoomContractRepresentativeService roomContractRepresentativeService
            ) : base(unitOfWork, mapper)
        {
            this.userService = UserService;
            this.electricWaterBillService = ElectricWaterBillService;
            this.roomUtilitiService = RoomUtilitiService;
            this.roomService = RoomService;
            this.roomContractRepresentativeService = roomContractRepresentativeService;
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
            IList<ElectricWaterBill> ElectricWaterBills = await electricWaterBillService.GetAsync(p => DateTime.Compare((DateTime)p.WriteDate, (DateTime)room.DateInToRoom) > 0 && p.RoomId == item.RoomId && p.WriteDate.Value.Month == item.Date.Value.Month && p.WriteDate.Value.Year == item.Date.Value.Year);
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
            
            return await base.CreateAsync(item);
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
                }
                if (UserMoneyLeft == 0)
                {
                    item.MoneyDebtRoomReceipt = 0;
                    roomContractRepresentatives[0].UserMoney = UserMoneyLeft + item.MoneyRecive;
                    item.MoneyRecive = roomReceipt.FinalBill;
                    item.Status = 2;
                }
                if (UserMoneyLeft < 0)
                {
                    roomContractRepresentatives[0].UserMoney = 0;
                    // nếu tài khoản hết tiền
                    double tmp = (double)item.MoneyRecive + UserMoneyLeft;
                    // => số tiên user nợ được là tmp

                    //RoomReceipt roomReceipt = this.GetById(item.Id);
                    item.Active = true;
                    item.MoneyRecive = item.MoneyRecive - UserMoneyLeft;
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
            return await base.UpdateAsync(item);
        }
    }
}
