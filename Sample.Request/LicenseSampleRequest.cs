using Sample.Request.Auth;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Request
{
    public class LicenseSampleRequest : AppDomainCatalogueRequest
    {
        public string Content { get; set; }
    }
}
