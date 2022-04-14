using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class PaymentMethodModel: AppDomainCatalogueModel
    {
        public string Logo { get; set; }
        public string PaymentCode { get; set; }
        public int UserId { get; set; }
    }
}
