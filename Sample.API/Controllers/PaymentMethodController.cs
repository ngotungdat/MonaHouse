using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        [Description("Phương thức thanh toán")]
        [Authorize]
        public class PaymentMethodController : BaseController<PaymentMethod, PaymentMethodModel, PaymentMethodRequest, BaseSearch>
        {
            protected IPackageService packageService;
            protected IUserService userService;
            protected IPaymentMethodService paymentMethodService;
            public PaymentMethodController(IServiceProvider serviceProvider, ILogger<BaseController<PaymentMethod, PaymentMethodModel, PaymentMethodRequest, BaseSearch>> logger
                , IConfiguration configuration, IWebHostEnvironment env) : base(serviceProvider, logger, env)
            {
                this.domainService = serviceProvider.GetRequiredService<IPaymentMethodService>();
                paymentMethodService = serviceProvider.GetRequiredService<IPaymentMethodService>();
            }


            [HttpGet]
            [Route("GetPaymentMethodByUserAndPaymentCode")]
            [AppAuthorize(new string[] { CoreContants.ViewAll })]
            public async Task<AppDomainResult> GetPaymentMethodByUserAndPaymentCode([FromQuery] int userId, string PaymentMethodCode)
            {
                AppDomainResult appDomainResult = new AppDomainResult();

                if (PaymentMethodCode != null) {
                var result = await paymentMethodService.GetPaymentMethodByUserIdAndPaymentMethodCode(userId, PaymentMethodCode);
                List<PaymentMethod> data = mapper.Map<List<PaymentMethod>>(result);
                appDomainResult = new AppDomainResult
                {
                    Data = data,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK,
                    ResultMessage = ApiMessage.GETALL_SUCCESS
                };


                return appDomainResult;
                }
                throw new Exception("Lỗi trong quá trình xủ lý");
            }

    }
        

}
