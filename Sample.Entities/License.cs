using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class License : DomainEntities.AppDomainCatalogue
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string LicenseType { get; set; }
        public string CustomerName { get; set; }
        [NotMapped]
        public string FullName { get; set; }
    }
}
