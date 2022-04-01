using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class LicenseModel : AppDomainCatalogueModel
    {
        public string Content { get; set; }
        public string CustomerId { get; set; }
        public string LicenseType { get; set; }
    }
}
