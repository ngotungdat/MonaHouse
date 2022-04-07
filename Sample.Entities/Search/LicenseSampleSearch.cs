using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities.Search
{
    public class LicenseSampleSearch : DomainEntities.BaseSearch
    {
        public string Code { get; set; }
    }
}
