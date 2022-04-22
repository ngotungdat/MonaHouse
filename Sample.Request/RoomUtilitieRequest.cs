using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class RoomUtilitieRequest: AppDomainRequest
    {
        /// <summary>
        /// Trung tâm 
        /// </summary>
        public int? RoomId { get; set; }
        /// <summary>
        /// Tiện ích
        /// </summary>
        public int? UtilitiesId { get; set; }
        public double? Price { get; set; }
        public string UtilitiName { get; set; }
    }
}
