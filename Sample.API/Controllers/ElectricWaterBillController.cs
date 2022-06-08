using Microsoft.AspNetCore.Authorization;
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
    [Description("Quản lý điện nước")]
    [Authorize]
    public class ElectricWaterBillController : BaseController<ElectricWaterBill, ElectricWaterBillModel, ElectricityWaterBillRequest, ElectricityWaterBillSearch>
    {
        protected IFloorService floorService;
        protected IUserService userService;
        protected IElectricWaterBillService ElectricWaterBillService;
        protected IRoomService RoomService;
        public ElectricWaterBillController(IServiceProvider serviceProvider, ILogger<BaseController<ElectricWaterBill, ElectricWaterBillModel, ElectricityWaterBillRequest, ElectricityWaterBillSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IElectricWaterBillService>();
            this.floorService = serviceProvider.GetRequiredService<IFloorService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            this.ElectricWaterBillService = serviceProvider.GetRequiredService<IElectricWaterBillService>();
            this.RoomService = serviceProvider.GetRequiredService<IRoomService>();
        }
        [HttpGet]
        [Description("Lấy bản ghi điện nước theo tháng")]
        [Route("get_electric_water_bill_with_month")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetElectricWaterBillWithMonth([FromQuery]GetElectricWaterBillWithMonthRequest request)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            
            if (ModelState.IsValid)
            {
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                Room room = RoomService.GetById(request.RoomId);
                IList<ElectricWaterBillModel> ElectricWaterBillModels = null;
                if (room.DateInToRoom == null)
                {
                    throw new AppException("Phòng trống không thể tính tiền");
                }
                if (room.ElectricWaterPackage == 0)
                {
                    IList<ElectricWaterBill> ElectricWaterBills = await ElectricWaterBillService.GetAsync(p => p.WriteDate.Value.Day> room.DateInToRoom.Value.Day && p.RoomId == request.RoomId && p.WriteDate.Value.Month == request.Month && p.WriteDate.Value.Year == request.Year);
                    ElectricWaterBillModels = (IList<ElectricWaterBillModel>)mapper.Map<IList<ElectricWaterBillModel>>(ElectricWaterBills);
                }
                else if (room.ElectricWaterPackage == 1) {
                    ElectricWaterBillModels = new List<ElectricWaterBillModel>();
                }
                appDomainResult = new AppDomainResult
                {
                    Data = ElectricWaterBillModels,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK,
                    ResultMessage = ApiMessage.GETALL_SUCCESS
                };
            }
            else
            {
                throw new AppException(ModelState.GetErrorMessage());
            }
            return appDomainResult;
        }

        [HttpGet]
        [Description("Thông kê điện nước theo tòa nhà và năm")]
        [Route("get_electric_water_bill_with_year_and_branchid")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetElectricWaterBillWitthBranchAndYear(int BranchId, int Year, int userId)
        {
            AppDomainResult appDomainResult = new AppDomainResult();

            if (ModelState.IsValid)
            {
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (user == null /*|| user.RoleNumber != 0 || user.RoleNumber != 3 || user.RoleNumber != 4*/) {
                    throw new Exception("Không phải là admin, CSKH, chủ trọ, nhân viên chủ trọ");
                }
                List<ElectricWaterForYearAndBranchId> ElectricWaterBills = await ElectricWaterBillService.GetElectricWaterBillWitthBranchAndYear(BranchId, Year, userId);

                appDomainResult = new AppDomainResult
                {
                    Data = ElectricWaterBills,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK,
                    ResultMessage = ApiMessage.GETALL_SUCCESS
                };
            }
            else
            {
                throw new AppException(ModelState.GetErrorMessage());
            }
            return appDomainResult;
        }
    }
}
