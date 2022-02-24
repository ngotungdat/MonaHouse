using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class BranchRequest : AppDomainRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập tên nhà!")]
        public string Name { get; set; }
    }
}
