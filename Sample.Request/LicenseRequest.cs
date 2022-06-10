using Sample.Request.Auth;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Request
{
    public class LicenseRequest : AppDomainCatalogueRequest
    {
        public string Content { get; set; }
        public string CustomerId { get; set; }
        public string LicenseType { get; set; }
        public string CustomerName { get; set; }
        public string FullName { get; set; }
    }
}
