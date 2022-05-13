using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities.Search
{
    public class RoomSearch : DomainEntities.BaseSearch
    {
        public int? status { get; set; }
        public int? roomtypeid { get; set; }
        public int? BranchId { get; set; }
        public int? Electric { get; set; }
        public int? TakeMoney { get; set; }
        public int? DebtMoney { get; set; }
        public DateTime? FromDateIntoRoom { get; set; }
        public DateTime? ToDateIntoRoom { get; set; }
        public DateTime? FromDateOutRoom { get; set; }
        public DateTime? ToDateOutRoom { get; set; }
    }
}
