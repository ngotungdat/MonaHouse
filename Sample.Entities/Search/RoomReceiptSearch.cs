using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities.Search
{
    public class RoomReceiptSearch : DomainEntities.BaseSearch
    {
        public int? RoomId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
}
