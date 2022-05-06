using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class CheckOutRoomRequest 
    {
        public int RoomId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
