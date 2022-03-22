using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class FeedBackType : DomainEntities.AppDomain
    {
        [Required]
        public string Name { get; set; }
    }
}
