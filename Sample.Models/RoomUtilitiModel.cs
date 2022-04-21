using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class RoomUtilitiModel : AppDomainModel
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

    }
}
