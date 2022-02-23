using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Request
{
    public class UserFileRequest : AppDomainRequest
    {
        /// <summary>
        /// Loại file
        /// </summary>
        public int? TypeId { get; set; }
    }
}
