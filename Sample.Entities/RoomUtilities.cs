using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class RoomUtilities : DomainEntities.AppDomain
    {
        /// <summary>
        /// Trung tâm 
        /// </summary>
        public int? RoomId { get; set; }
        /// <summary>
        /// Tiện ích
        /// </summary>
        public int? UtilitiesId { get; set; }
    }
}
