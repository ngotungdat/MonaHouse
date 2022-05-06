using AutoMapper;
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
        protected IRoomImageService roomImageService;
        protected IElectricWaterBillService electricWaterBillService;
        protected IRoomUtilitiService roomUtilitiService;
        protected IRoomService roomService;
        public RoomService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext
            ,IUserService UserService
            ,IRoomImageService RoomImageService
            ,IElectricWaterBillService ElectricWaterBillService
            ,IRoomUtilitiService RoomUtilitiService
            ) : base(unitOfWork, mapper)
        {
            this.userService = UserService;
            this.roomImageService = RoomImageService;
            this.electricWaterBillService = ElectricWaterBillService;
            this.roomUtilitiService = RoomUtilitiService;
            this.coreDbContext = coreDbContext;
        }

        public async Task<bool> AddNewRoomWithImage(RoomWithImgRequest itemModel)
        {
            bool returnResult = false;
            var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
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
            if (room!=null)
            {
                returnResult = true;
                // hình ảnh phòng mới
                var ListLinkImage = itemModel.Images.Split(";");
                List<RoomImage> LsRoomImage = new List<RoomImage>();
                if (ListLinkImage.Length > 0)
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

            
            return returnResult;
        }

        public async Task<double> CheckOutRoomWithMonth(CheckOutRoomRequest request)
        {
            // lấy ra danh sách ghi điện theo tháng và năm
            IList<ElectricWaterBill> ElectricWaterBills = await electricWaterBillService.GetAsync(p => p.RoomId == request.RoomId && p.WriteDate.Value.Month == request.Month && p.WriteDate.Value.Year == request.Year);
            // kiểm tra ngày dọn vào của người đại diện trong phòng để tính toán điện nước
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

        protected override string GetStoreProcName()
        {
            return "Get_Room";
        }
       
    }
}
