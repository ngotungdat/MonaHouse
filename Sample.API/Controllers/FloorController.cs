using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.BaseAPI.Controllers;
using Sample.Entities;
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
    [Description("Quản lý tầng")]
    [Authorize]
    public class FloorController : BaseController<Floor, FloorModel, FloorRequest, FloorSearch>
    {
        protected IFloorService floorService;
        private IConfiguration configuration;
        protected IUserService userService;
        public FloorController(IServiceProvider serviceProvider, ILogger<BaseController<Floor, FloorModel, FloorRequest, FloorSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IFloorService>();
            this.floorService = serviceProvider.GetRequiredService<IFloorService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            this.configuration = configuration;
        }
        /// <summary>
        /// Thêm mới item
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AppAuthorize(new string[] { CoreContants.AddNew })]
        public override async Task<AppDomainResult> AddItem([FromBody] FloorRequest itemModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool success = false;
            if (ModelState.IsValid)
            {
                var item = mapper.Map<Floor>(itemModel);
                var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
                if (item != null)
                {
                    item.TenantId = user.TenantId;
                    item.CreatedBy = user.UserName;
                    // Kiểm tra item có tồn tại chưa?
                    var messageUserCheck = await this.domainService.GetExistItemMessage(item);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    success = await this.domainService.CreateAsync(item);
                    if (success)
                    {
                        appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                        appDomainResult.ResultMessage = ApiMessage.CREATE_SUCCESS;
                    }
                    else
                        throw new Exception("Lỗi trong quá trình xử lý");
                    appDomainResult.Success = success;
                }
                else
                    throw new AppException("Item không tồn tại");
            }
            else
            {
                throw new AppException(ModelState.GetErrorMessage());
            }
            return appDomainResult;
        }
        [HttpGet]
        [Route("get-by-branch/{branchId:int}")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetByBranch(int? branchId)
        {
            IList<Floor> floor = await this.floorService.GetAsync(e => !e.Deleted && e.BranchId == (branchId ?? 0));
            IList<FloorModel> data = mapper.Map<IList<FloorModel>>(floor);
            PagedList<FloorModel> pagedDataModel = new PagedList<FloorModel> { TotalItem = 0, Items = data, PageIndex = 0, PageSize = 0 };
            return new AppDomainResult()
            {
                Data = pagedDataModel,
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }
    }
}
