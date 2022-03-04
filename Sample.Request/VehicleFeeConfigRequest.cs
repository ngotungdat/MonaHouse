using Sample.Request.Auth;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Request
{
    public class VehicleFeeConfigRequest : AppDomainRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập loại xe")]
        public string Name { get; set; }
        [DefaultValue(0)]
        public double? Price { get; set; }
    }
}
