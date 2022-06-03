using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Extensions;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.Services.DomainServices;
using Sample.Interface.UnitOfWork;
using Sample.Request;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class RoomService : DomainService<Room, RoomSearch>, IRoomService
    {
        protected IAppDbContext coreDbContext;
        protected IUserService userService;
        protected IUserInRoomService userInRoomService;
        protected IRoomImageService roomImageService;
        protected IElectricWaterBillService electricWaterBillService;
        protected IRoomUtilitiService roomUtilitiService;
        protected IPackageOfUserService packageOfUserService;
        protected IPackageService packageService;
        public RoomService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext
            ,IUserService UserService
            ,IUserInRoomService UserInRoomService
            , IRoomImageService RoomImageService
            ,IElectricWaterBillService ElectricWaterBillService
            ,IRoomUtilitiService RoomUtilitiService
            ,IPackageOfUserService PackageOfUserService
            , IPackageService PackageService
            ) : base(unitOfWork, mapper)
        {
            this.userService = UserService;
            this.userInRoomService = UserInRoomService;
            this.roomImageService = RoomImageService;
            this.electricWaterBillService = ElectricWaterBillService;
            this.roomUtilitiService = RoomUtilitiService;
            this.packageOfUserService = PackageOfUserService;
            this.packageService = PackageService;
            this.coreDbContext = coreDbContext;
        }

        public async Task<bool> AddNewRoomWithImage(RoomWithImgRequest itemModel)
        {
            bool returnResult = false;
            // kiem tra goi su dung
            var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
            if (user != null)
            {
                // đếm số lượng phòng hiện tại
                List<Room> Rooms = (List<Room>)await this.GetAsync(R => R.TenantId == user.Id);
                // số lượng phòng cho phép của gói user đang sử dụng
                var packageOfUser = await packageOfUserService.GetAsync(POU => POU.UserId == user.Id && POU.Status == 1); // status =1 : user dng su dung hop dong nay.
                // vi tren la iList nhung theo logic moi user chi co' 1 goi' dang su dung nen danh sach luon tra ve mot doi tuong packageOfUser
                var package = packageService.GetById(packageOfUser[0].PackageId);
                // so sanh so luong phong hien tai va so luong phong cho phep
                // nếu số lượng phòng hiện tại < sô lượng phòng cho phép => ok
                if(Rooms.Count< packageOfUser[0].RoomLimited)
                {
                    // phòng mới
                    Room room = new Room();
                    room.Id = itemModel.Id;
                    room.Name = itemModel.Name;
                    room.Price = itemModel.Price;
                    room.Status = itemModel.Status;
                    room.TenantId = user.TenantId;
                    room.CreatedBy = user.UserName;
                    room.Created = DateTime.Now;
                    room.Acreage = itemModel.Acreage;
                    room.BedAmount = itemModel.BedAmount;
                    room.BranchFloor = itemModel.BranchFloor;
                    room.BranchId = itemModel.BranchId;
                    room.Deposit = itemModel.Deposit;
                    room.Deleted = itemModel.Deleted;
                    room.Active = itemModel.Active;
                    room.RoomTypeId = itemModel.RoomTypeId;
                    room.FloorId = itemModel.FloorId;
                    //giá diện nước mặc định 
                    room.ElectricPrice = 3000; // 3000 / kw điện
                    room.WaterPrice = 8000; // 8000/ m3 nước
                                            //
                                            //var result = await this.CreateAsync(room);
                    await this.unitOfWork.Repository<Room>().CreateAsync(room);
                    await this.unitOfWork.SaveAsync();
                    if (room != null)
                    {
                        returnResult = true;
                        // hình ảnh phòng mới
                        var ListLinkImage = itemModel.Images.Split(";");
                        List<RoomImage> LsRoomImage = new List<RoomImage>();
                        if (ListLinkImage.Length > 0 && itemModel.Images!= "")
                        {
                            foreach (var d in ListLinkImage)
                            {
                                RoomImage roomImage = new RoomImage();
                                roomImage.RoomId = room.Id;
                                roomImage.Link = d;
                                LsRoomImage.Add(roomImage);
                            }
                        }
                        var rs = await roomImageService.CreateAsync(LsRoomImage);
                        if (rs)
                        {
                            returnResult = true;
                        }
                        else
                        {
                            returnResult = false;
                        }
                    }
                    else
                    {
                        returnResult = false;
                    }
                }
                else
                {
                    throw new Exception("Số phòng theo gói đã hết!");
                }
            }
            else
            {
                throw new Exception("lỗi trong quá trình xử lý");
            }

            
            return returnResult;
        }

        public async Task<double> CheckOutRoomWithMonth(CheckOutRoomRequest request)
        {
            // lấy thông tin phòng
            Room room = this.GetById(request.RoomId);
            if (room.DateInToRoom == null) {
                throw new Exception("Phòng trống không thể tính toán");
            }
            // kiểm tra ngày dọn vào của người đại diện trong phòng để tính toán điện nước
            // lấy ra danh sách ghi điện theo tháng và năm
            IList<ElectricWaterBill> ElectricWaterBills = await electricWaterBillService.GetAsync(p => DateTime.Compare((DateTime)p.WriteDate, (DateTime)room.DateInToRoom) >0 && p.RoomId == request.RoomId && p.WriteDate.Value.Month == request.Month && p.WriteDate.Value.Year == request.Year);
            // chưa có chức năng dọn vào !!!!!!!!!!!!!!!

            // tiền điện nước tháng đó
            double? electric_bill = 0;
            double? water_bill = 0;
            foreach(ElectricWaterBill electricWaterBill in ElectricWaterBills)
            {
                if(electricWaterBill.ElectricBill != null)
                {
                    electric_bill += electricWaterBill.ElectricBill;
                }
                if (electricWaterBill.WaterBill != null)
                {
                    water_bill += electricWaterBill.WaterBill;
                }
            }
            // lấy danh sách dịch vụ của phòng
            IList<RoomUtilities> roomUtilities = await roomUtilitiService.GetAsync(p=> p.RoomId==request.RoomId);
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
                room_price_bill = this.GetById(request.RoomId).Price;
            }
            // tổng giá tiền phải chi chả theo tháng 
            double total_money_bill = 0;
            total_money_bill = (double)(room_price_bill+ electric_bill + water_bill + room_utilitie_bill );
            return total_money_bill;
        }

        public async Task<bool> GetOutRoom(int roomId, DateTime dateTime)
        {
            //update thông tin phòng
            Room room = this.GetById(roomId);
            
            //update thông tin UserInroom
            var userInRoom = await userInRoomService.GetAsync(p=> p.UsersId==room.UserInRoomRepresentative&& p.RoomId==roomId);
            // lấy thông tin khách hàng  trong phòng
            // nếu khi người đại diện dọn đi mà vẫn còn người trong phòng\
            // những khách hàng còn ở lại phòng
            var UserInRooms = await userInRoomService.GetAsync(d=> d.UsersId!= room.UserInRoomRepresentative && d.RoomId == roomId && d.Status==1);
            if (userInRoom.Count == 0)
            {
                room.Status = 0; // phòng trống
            }

            room.UserInRoomRepresentative = -1; // không có người đại diện
            room.DateInToRoom = null;
            room.DateGetOutRoom = dateTime; 
            //user
            userInRoom[0].Status = 0; //0: don di; 1:dang o
            var result = false;
            result = await this.UpdateAsync(room);
            result = await userInRoomService.UpdateAsync(userInRoom[0]);
            return result;
        }

        public Task<List<RoomReport>> GetReportRoom(int userId)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("UserId", userId));

            SqlParameter[] parameters = sqlParameters.ToArray();
            return Task.Run(() =>
            {
                List<RoomReport> rs = new List<RoomReport>();
                DataTable dataTable = new DataTable();
                SqlConnection connection = null;
                SqlCommand command = null;
                try
                {
                    connection = (SqlConnection)coreDbContext.Database.GetDbConnection();
                    command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = "GET_reportRoom";
                    command.Parameters.AddRange(parameters);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);
                    rs = MappingDataTable.ConvertToList<RoomReport>(dataTable);

                    return rs;
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

        protected override string GetStoreProcName()
        {
            return "Get_Room";
        }
       
    }
}
