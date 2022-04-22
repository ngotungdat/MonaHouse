using Sample.Request.DomainRequests;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sample.Request
{
    public class UpdateUtilitiesRoomRequest : AppDomainRequest
    {
        public int RoomId { get; set; }
        public string ListUtilitiesAndPrice { get; set; }
    }
}
