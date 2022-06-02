using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class ReportEmptyModel : AppDomainModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int RoomStatus { get; set; }

    }
}
