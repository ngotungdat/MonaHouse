using Sample.Entities;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class PaymentMethodRequest : AppDomainCatalogueRequest
    {
        public string Logo { get; set; }
        public string PaymentCode { get; set; }
        public int UserId { get; set; }
    }
}
