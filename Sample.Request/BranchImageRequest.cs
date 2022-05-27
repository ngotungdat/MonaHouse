using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class BranchImageRequest: AppDomainRequest
    {
        /// <summary>
        /// Link hình ảnh
        /// </summary>
        public string Link { get; set; }
        public int? BranchId { get; set; }

    }
}
