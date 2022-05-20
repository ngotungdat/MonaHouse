using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities.Search
{
    public class RoomContractRepresentativeSearch : DomainEntities.BaseSearch
    {
        public int? RoomId { get; set; }
        public int? UserId{ get; set; }
        public int? Status{ get; set; }

    }
}
