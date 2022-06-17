using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.BaseAPI.Controllers;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Extensions;
using Sample.Interface.Services;
using Sample.Models;
using Sample.Request;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Description("Quản lý thông tin phòng")]
    public class RoomController : BaseController<Room, RoomModel, RoomRequest, RoomSearch>
    {
        protected IBranchService branchService;
        protected IUserInRoomService userInRoomService;
        protected IElectricWaterBillService electricWaterBillService;
        protected IRoomReceiptService roomReceiptService;
        protected IRoomContractRepresentativeService RoomContractRepresentativeService;
        protected IRoomUtilitiService roomUtilitiService;
        protected IUserService userService;
        private IConfiguration configuration;
        protected IRoomService roomService;
        protected IFloorService floorService;
        public RoomController(IServiceProvider serviceProvider, ILogger<BaseController<Room, RoomModel, RoomRequest, RoomSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.userInRoomService = serviceProvider.GetRequiredService<IUserInRoomService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            this.electricWaterBillService = serviceProvider.GetRequiredService<IElectricWaterBillService>();
            this.roomReceiptService = serviceProvider.GetRequiredService<IRoomReceiptService>();
            this.RoomContractRepresentativeService = serviceProvider.GetRequiredService<IRoomContractRepresentativeService>();
            this.roomUtilitiService = serviceProvider.GetRequiredService<IRoomUtilitiService>();
            this.domainService = serviceProvider.GetRequiredService<IRoomService>();
            this.roomService = serviceProvider.GetRequiredService<IRoomService>();
            this.floorService = serviceProvider.GetRequiredService<IFloorService>();
            this.branchService = serviceProvider.GetRequiredService<IBranchService>();
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("AddNewRoom")]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public async Task<AppDomainResult> AddNewRoom([FromBody] RoomWithImgRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                    success = await roomService.AddNewRoomWithImage(itemModel);
                    if (success)
                        appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                    else
                        throw new Exception("Lỗi trong quá trình xử lý");
                    appDomainResult.Success = success;
                }
                else
                    throw new AppException("Item không tồn tại");
            
            return appDomainResult;
        }

        [HttpGet]
        [Route("CheckOutRoom")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> CheckOutRoom([FromQuery] CheckOutRoomRequest request)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                double total_money_checkout = await roomService.CheckOutRoomWithMonth(request);
                appDomainResult.Data = total_money_checkout;
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                appDomainResult.Success = true;
            }
            else
                throw new AppException("Item không tồn tại");

            return appDomainResult;
        }

        [HttpGet]
        [Route("MoveOutRoom")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> MoveOutRoom([FromQuery] int roomid)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                // thông tin phòng
                Room room = domainService.GetById(roomid);
                // thông tin khách trong phòng
                List<UserInRoom> userInRooms = (List<UserInRoom>)await userInRoomService.GetAsync(d => d.RoomId == room.Id && d.UsersId != room.UserInRoomRepresentative);
                foreach (UserInRoom userInRoom in userInRooms) {
                    Users user = userService.GetById((int)userInRoom.UsersId);
                    if (user.FullName != null)
                    {
                        userInRoom.FullName = user.FullName;
                    }
                    else {
                        userInRoom.FullName = "";
                    }
                }
                // thông tin các hóa đơn đã có( đã tính tiền)
                //List<RoomReceipt> roomReceipts = (List<RoomReceipt>)await roomReceiptService.GetAsync(d=>d.RoomId== roomid && Int32.Parse(d.UserId) == room.UserInRoomRepresentative && d.Date <= DateTime.Now);

                // thông tin hóa đơn mỗi tháng thuê phòng từ ngày dọn vào
                var date = new DateTime(room.DateInToRoom.Value.Year, room.DateInToRoom.Value.Month, 1);
                List<RoomReceipt> MoveOut_RoomReceipts = new List<RoomReceipt>();
                while (date <= DateTime.Now)
                {
                    List<RoomReceipt> roomReceipts = (List<RoomReceipt>)await roomReceiptService.GetAsync(d => d.RoomId == roomid
                    && d.UserId == room.UserInRoomRepresentative.ToString()
                    && d.Date.Value.Month == date.Month
                    && d.Date.Value.Year == date.Year
                    );
                    if (roomReceipts.Count > 1)
                    {
                        throw new Exception("Lỗi trong quá trình xử lý!");
                    }
                    else if (roomReceipts.Count == 1)
                    {
                        MoveOut_RoomReceipts.Add(roomReceipts[0]);
                    }
                    else if (roomReceipts.Count == 0)
                    {
                        // nếu là tháng đầu tiên
                        if (date.Year == room.DateInToRoom.Value.Year && date.Month == room.DateInToRoom.Value.Month)
                        {
                            // Tạo mới phiếu tính tiền
                            RoomReceipt roomReceipt = new RoomReceipt();
                            roomReceipt.RoomId = roomid;
                            roomReceipt.UserId = room.UserInRoomRepresentative.ToString();
                            roomReceipt.Date = DateTime.Now;
                            // tính tiền điện
                            
                            GetElectricWaterBillWithMonthRequest rq = new GetElectricWaterBillWithMonthRequest();
                            rq.Month = date.Month;
                            rq.Year = date.Year;
                            rq.RoomId = roomid;
                            List<ElectricWaterBill> electricWaterBills = (List<ElectricWaterBill>)await electricWaterBillService.GetElectricWaterBillWhenCheckOutWithMonth(rq);
                            double waterBill = 0;
                            double electricBill = 0;
                            foreach (ElectricWaterBill electricWaterBill in electricWaterBills)
                            {
                                waterBill += (double)electricWaterBill.WaterBill;
                                electricBill += (double)electricWaterBill.ElectricBill;
                            }
                            roomReceipt.ElectricBill = electricBill;
                            roomReceipt.WaterBill = waterBill;
                            // Tính tiền tiện ích phòng
                            List<RoomUtilities> roomUtilities = (List<RoomUtilities>)await roomUtilitiService.GetAsync(d => d.RoomId == roomid);
                            double roomUtilitiBill = 0;
                            foreach (RoomUtilities ru in roomUtilities)
                            {
                                roomUtilitiBill += (double)ru.Price;
                            }
                            // tính tiền phòng
                            double roomPriceBill = (double)room.Price;

                            int dateInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                            // Giá tiện ích phòng theo ngày
                            double room_utilitie_bill_PerDay = (double)(roomUtilitiBill / (double)dateInMonth);
                            // Giá phòng theo ngày
                            double roomPricePerDay = (double)(roomPriceBill / (double)dateInMonth);
                            // Ngày khách ở trong tháng này
                            int daysInRoom = dateInMonth - room.DateInToRoom.Value.Day + 1;
                            if (daysInRoom < 1)
                            {
                                throw new Exception("Khách hàng chưa ở ngày nào!");
                            }
                            roomUtilitiBill = room_utilitie_bill_PerDay * daysInRoom;
                            roomPriceBill = roomPricePerDay * daysInRoom;
                            //
                            roomReceipt.RoomUtilitieBill = roomUtilitiBill;
                            roomReceipt.RoomBill = roomPriceBill;
                            roomReceipt.TotalBill = electricBill + waterBill + roomUtilitiBill + roomPriceBill;
                            roomReceipt.PlusBill = 0;
                            roomReceipt.SubBill = 0;
                            roomReceipt.FinalBill = electricBill + waterBill + roomUtilitiBill + roomPriceBill + 0 + 0;
                            roomReceipt.TenantId = LoginContext.Instance.CurrentUser.UserId;
                            roomReceipt.Deleted = false;
                            roomReceipt.Active = true;
                            roomReceipt.Note = "";
                            roomReceipt.Status = 0;
                            roomReceipt.MoneyDebtRoomReceipt = electricBill + waterBill + roomUtilitiBill + roomPriceBill + 0 + 0;
                            roomReceipt.MoneyRecive = 0;
                            roomReceipt.NoteRecive = "";
                            roomReceipt.PaymenMethod = 0; // default : Chuyển khoản
                            //
                            MoveOut_RoomReceipts.Add(roomReceipt);
                        }
                        //tính tiền tháng cuối
                        else if (date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month)
                        {
                            // Tạo mới phiếu tính tiền
                            RoomReceipt roomReceipt = new RoomReceipt();
                            roomReceipt.RoomId = roomid;
                            roomReceipt.UserId = room.UserInRoomRepresentative.ToString();
                            roomReceipt.Date = DateTime.Now;
                            // tính tiền điện
                            MoveOut_RoomReceipts.Add(roomReceipt);
                            GetElectricWaterBillWithMonthRequest rq = new GetElectricWaterBillWithMonthRequest();
                            rq.Month = date.Month;
                            rq.Year = date.Year;
                            rq.RoomId = roomid;
                            List<ElectricWaterBill> electricWaterBills = (List<ElectricWaterBill>)await electricWaterBillService.GetElectricWaterBillWhenCheckOutWithMonth(rq);
                            double waterBill = 0;
                            double electricBill = 0;
                            foreach (ElectricWaterBill electricWaterBill in electricWaterBills)
                            {
                                waterBill += (double)electricWaterBill.WaterBill;
                                electricBill += (double)electricWaterBill.ElectricBill;
                            }
                            roomReceipt.ElectricBill = electricBill;
                            roomReceipt.WaterBill = waterBill;
                            // Tính tiền tiện ích phòng
                            List<RoomUtilities> roomUtilities = (List<RoomUtilities>)await roomUtilitiService.GetAsync(d => d.RoomId == roomid);
                            double roomUtilitiBill = 0;
                            foreach (RoomUtilities ru in roomUtilities)
                            {
                                roomUtilitiBill += (double)ru.Price;
                            }
                            // tính tiền phòng
                            double roomPriceBill = (double)room.Price;
                            int dateInMonth = 0;
                            try { 
                                dateInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                            }
                            catch (Exception ex) {
                                throw new Exception("loi cmnr");
                            }
                            // Giá tiện ích phòng theo ngày
                            double room_utilitie_bill_PerDay = (double)(roomUtilitiBill / (double)dateInMonth);
                            // Giá phòng theo ngày
                            double roomPricePerDay = (double)(roomPriceBill / (double)dateInMonth);
                            // Ngày khách ở trong tháng này
                            int daysInRoom =  DateTime.Now.Day ;
                            if (daysInRoom < 1)
                            {
                                throw new Exception("Khách hàng chưa ở ngày nào!");
                            }
                            roomUtilitiBill = room_utilitie_bill_PerDay * daysInRoom;
                            roomPriceBill = roomPricePerDay * daysInRoom;
                            //
                            roomReceipt.RoomUtilitieBill = roomUtilitiBill;
                            roomReceipt.RoomBill = roomPriceBill;
                            roomReceipt.TotalBill = electricBill + waterBill + roomUtilitiBill + roomPriceBill;
                            roomReceipt.PlusBill = 0;
                            roomReceipt.SubBill = 0;
                            roomReceipt.FinalBill = electricBill + waterBill + roomUtilitiBill + roomPriceBill + 0 + 0;
                            roomReceipt.TenantId = LoginContext.Instance.CurrentUser.UserId;
                            roomReceipt.Deleted = false;
                            roomReceipt.Active = true;
                            roomReceipt.Note = "";
                            roomReceipt.Status = 0;
                            roomReceipt.MoneyDebtRoomReceipt = electricBill + waterBill + roomUtilitiBill + roomPriceBill + 0 + 0;
                            roomReceipt.MoneyRecive = 0;
                            roomReceipt.NoteRecive = "";
                            roomReceipt.PaymenMethod = 0; // default : Chuyển khoản
                            //
                            MoveOut_RoomReceipts.Add(roomReceipt);
                        }
                        else {
                            // Tạo mới phiếu tính tiền
                            RoomReceipt roomReceipt = new RoomReceipt();
                            roomReceipt.RoomId = roomid;
                            roomReceipt.UserId = room.UserInRoomRepresentative.ToString();
                            roomReceipt.Date = date;
                            // tính tiền điện
                            MoveOut_RoomReceipts.Add(roomReceipt);
                            GetElectricWaterBillWithMonthRequest rq = new GetElectricWaterBillWithMonthRequest();
                            rq.Month = date.Month;
                            rq.Year = date.Year;
                            rq.RoomId = roomid;
                            List<ElectricWaterBill> electricWaterBills = (List<ElectricWaterBill>)await electricWaterBillService.GetElectricWaterBillWhenCheckOutWithMonth(rq);
                            double waterBill = 0;
                            double electricBill = 0;
                            foreach (ElectricWaterBill electricWaterBill in electricWaterBills)
                            {
                                waterBill += (double)electricWaterBill.WaterBill;
                                electricBill += (double)electricWaterBill.ElectricBill;
                            }
                            roomReceipt.ElectricBill = electricBill;
                            roomReceipt.WaterBill = waterBill;
                            // Tính tiền tiện ích phòng
                            List<RoomUtilities> roomUtilities = (List<RoomUtilities>)await roomUtilitiService.GetAsync(d => d.RoomId == roomid);
                            double roomUtilitiBill = 0;
                            foreach (RoomUtilities ru in roomUtilities)
                            {
                                roomUtilitiBill += (double)ru.Price;
                            }
                            // tính tiền phòng
                            double roomPriceBill = (double)room.Price;

                            roomReceipt.RoomUtilitieBill = roomUtilitiBill;
                            roomReceipt.RoomBill = roomPriceBill;
                            roomReceipt.TotalBill = electricBill + waterBill + roomUtilitiBill + roomPriceBill;
                            roomReceipt.PlusBill = 0;
                            roomReceipt.SubBill = 0;
                            roomReceipt.FinalBill = electricBill + waterBill + roomUtilitiBill + roomPriceBill + 0 + 0;
                            roomReceipt.TenantId = LoginContext.Instance.CurrentUser.UserId;
                            roomReceipt.Deleted = false;
                            roomReceipt.Active = true;
                            roomReceipt.Note = "";
                            roomReceipt.Status = 0;
                            roomReceipt.MoneyDebtRoomReceipt = electricBill + waterBill + roomUtilitiBill + roomPriceBill + 0 + 0;
                            roomReceipt.MoneyRecive = 0;
                            roomReceipt.NoteRecive = "";
                            roomReceipt.PaymenMethod = 0; // default : Chuyển khoản
                            //
                            MoveOut_RoomReceipts.Add(roomReceipt);
                        }

                    }
                    else
                    {
                        throw new Exception("lỗi trong quá trình xử lý!");
                    }
                    Console.WriteLine(date.Month);
                    date = date.AddMonths(1);
                }
                //
                //var rs = await roomReceiptService.CreateAsync(MoveOut_RoomReceipts);
                //
                /*if (rs == false) {
                    throw new Exception("Lỗi trong quá trình xử lý");
                }*/
                // thông tin hợp đồng phòng
                List<RoomContractRepresentative> roomContractRepresentatives = (List<RoomContractRepresentative>)await RoomContractRepresentativeService.GetAsync(d => d.UserId == room.UserInRoomRepresentative && d.RoomId == room.Id && d.Status == 0);
                RoomContractRepresentative roomContractRepresentative = null;
                if (roomContractRepresentatives.Count > 0)
                {
                    roomContractRepresentative = roomContractRepresentatives[0];
                }
                else
                {
                    throw new Exception("Phòng ở không có hợp đồng");
                }
                //
                double TotalDebt = 0;
                foreach (RoomReceipt rr in MoveOut_RoomReceipts) {
                    TotalDebt +=(double) rr.MoneyDebtRoomReceipt;
                }

                Object obj = new  {
                    userInRooms = userInRooms,
                    moveOut_RoomReceipts= MoveOut_RoomReceipts,
                    roomContractRepresentative= roomContractRepresentative,
                    totalDebt = TotalDebt
                };
                

                appDomainResult.Data = obj;
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                appDomainResult.Success = true;
            }
            else {
                throw new AppException("Item không tồn tại");
            }

            return appDomainResult;
        }

        [HttpGet]
        [Route("GetReportRoom")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetReportRoom([FromQuery] int userId)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                List<RoomReport> rs = await roomService.GetReportRoom(userId);
                appDomainResult.Data = rs;
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                appDomainResult.Success = true;
            }
            else
                throw new AppException("Item không tồn tại");

            return appDomainResult;
        }

        [HttpPost]
        [Route("GoOutRoom")]
        [AppAuthorize(new string[] { CoreContants.Update })]
        public async Task<AppDomainResult> GoOutRoom([FromQuery] int roomId, DateTime dateTime)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                success = await roomService.GetOutRoom(roomId, dateTime);
                if (success)
                    appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                else
                    throw new Exception("Lỗi trong quá trình xử lý");
                appDomainResult.Success = success;
            }
            else
                throw new AppException("Item không tồn tại");

            return appDomainResult;
        }

        [HttpGet]
        [Route("GetAllRoomByTenantId")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetAllRoomByTenantId([FromQuery] int userId)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                List<Room> rooms = (List<Room>)await roomService.GetAsync(d=>d.TenantId==userId);
                List<Floor> floors = (List<Floor>)await floorService.GetAsync(d=>d.TenantId==userId);
                List<Branch> branchs = (List<Branch>)await branchService.GetAsync(d=>d.TenantId==userId);

                var rs = from room in rooms
                         join floor in floors on room.FloorId equals floor.Id
                         join Branch in branchs on room.BranchId equals Branch.Id
                         where room.TenantId == userId
                         select new
                         {
                             RoomId=room.Id,
                             RoomName= room.Name,
                             FloorId= floor.Id,
                             FloorName= floor.Name,
                             BranchId = Branch.Id,
                             BranchName= Branch.Name,
                             RoomStatus= room.Status,
                         };
                
                appDomainResult.Data = rs;
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                appDomainResult.Success = true;
            }
            else
                throw new AppException("Item không tồn tại");

            return appDomainResult;
        }
    }
}
