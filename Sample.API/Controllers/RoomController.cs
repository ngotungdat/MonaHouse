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
        protected IUserService userService;
        private IConfiguration configuration;
        protected IRoomService roomService;
        protected IFloorService floorService;
        public RoomController(IServiceProvider serviceProvider, ILogger<BaseController<Room, RoomModel, RoomRequest, RoomSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
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
