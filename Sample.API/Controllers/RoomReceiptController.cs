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
    [Description("Quản lý hóa đơn tiền phòng")]
    [Authorize]
    public class RoomReceiptController : BaseController<RoomReceipt, RoomReceiptModel, RoomReceiptRequest, RoomReceiptSearch>
    {
        protected IUserService userService;
        public RoomReceiptController(IServiceProvider serviceProvider, ILogger<BaseController<RoomReceipt, RoomReceiptModel, RoomReceiptRequest, RoomReceiptSearch>> logger
            , IConfiguration configuration
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IRoomReceiptService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
        }

        [HttpGet]
        [Route("GetRoomReceiptByRoomId-Month-Year")]
        [AppAuthorize(new string[] { CoreContants.View })]
        public async Task<AppDomainResult> GetRoomReceipt([FromQuery] int roomId ,int month, int year)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                List<RoomReceipt> roomReceipts = (List<RoomReceipt>)await domainService.GetAsync(p=>p.RoomId==roomId&& p.Date.Value.Month==month&& p.Date.Value.Year==year && p.UserId!= null);
                List<Users> users = new List<Users>();
                foreach (var roomReceipt in roomReceipts) {
                    if (roomReceipt.UserId != null) {
                        var user = userService.GetById(Int32.Parse(roomReceipt.UserId));
                        users.Add(user);
                    }
                }
                var result = from r in roomReceipts
                             join us in users on Int32.Parse(r.UserId) equals us.Id
                             select new{
                                Id=r.Id,
                                TenantId = r.TenantId,
                                Active=r.Active,
                                 Created=r.Created,
                                 CreatedBy=r.CreatedBy,
                                 Deleted= r.Deleted,
                                 Updated=r.Updated,
                                 UpdatedBy=r.UpdatedBy,
                                RoomId =r.RoomId,
                                UserId=r.UserId,
                                Date =r.Date,
                                ElectricBill=r.ElectricBill ,
                                WaterBill =r.WaterBill,
                                RoomUtilitieBill =r.RoomUtilitieBill,
                                RoomBill = r.RoomBill,
                                TotalBill = r.TotalBill,
                                PlusBill =r.PlusBill,
                                SubBill =r.SubBill,
                                FinalBill =r.FinalBill,
                                Status =r.Status,
                                Note =r.Note,
                                UserName =us.UserName,
                                FullName =us.FullName,
                                DebtMoney =us.DebtMoney
                                    };
                appDomainResult.Data = result;
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
                appDomainResult.Success = true;
            }
            else
                throw new AppException("Item không tồn tại");

            return appDomainResult;
        }
    }
}
