using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class CustomerResources : DomainEntities.AppDomain
    {
        // nguon khanh hang
        [Required]
        public string Name { get; set; }
        // trang thai 
        public string Status { get; set; }
    }
}
