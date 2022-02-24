using Sample.Request.Auth;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Request
{
    public class AreaRequest : AppDomainRequest
    {
        /// <summary>
        /// Tên tỉnh
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập tên tỉnh")]
        public string Name { get; set; }
    }
}
