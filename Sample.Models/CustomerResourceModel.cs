using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class CustomerResourceModel : AppDomainModel
    {
        // nguon khanh hang
        public string Name { get; set; }
        // trang thai 
        public string Status { get; set; }
    }
}
