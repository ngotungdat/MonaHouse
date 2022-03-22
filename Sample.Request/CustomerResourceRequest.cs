using Sample.Request.DomainRequests;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sample.Request
{
    public class CustomerResourceRequest : AppDomainRequest
    {
        [Required(ErrorMessage = "Vui lòng nguồn")]
        public string Name { get; set; }
        [DefaultValue(0)]
        public string Status { get; set; }
    }
}
