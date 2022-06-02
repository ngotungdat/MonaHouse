using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class RoomReport : DomainEntities.AppDomain
    {
        [NotMapped]
        public int? Room { get; set; }
        [NotMapped]
        public int? RoomEmpt { get; set; }
        [NotMapped]
        public int? RoomSoonIn { get; set; }
        [NotMapped]
        public int? RoomSoonOut { get; set; }
        [NotMapped]
        public int? RoomUsing { get; set; }
    }
}
