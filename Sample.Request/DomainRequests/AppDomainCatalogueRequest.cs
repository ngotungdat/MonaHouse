﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Request.DomainRequests
{
    public class AppDomainCatalogueRequest : AppDomainRequest
    {
        [StringLength(50, ErrorMessage = "Vui lòng nhập Mã nhỏ hơn 50 kí tự")]
        public string Code { get; set; }
        [StringLength(1000, ErrorMessage = "Vui lòng nhập Tên nhỏ hơn 1000 kí tự")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
