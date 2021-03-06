using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities.Search
{
    public class PackageOfUserSearch : DomainEntities.BaseSearch
    {
        public int? PackageId { get; set; }
        public int? PackageOfUserId { get; set; }
        public int? UserId { get; set; }
        public int? Status { get; set; }
    }
}
