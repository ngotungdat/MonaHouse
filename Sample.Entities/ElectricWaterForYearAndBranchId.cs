using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class ElectricWaterForYearAndBranchId : DomainEntities.AppDomain
    {
        /// <summary>
        /// Trung tâm 
        /// </summary>
        /// 
        [NotMapped]
        public int? dien { get; set; }
        /// <summary>
        /// Tiện ích
        /// </summary>
        /// 
        [NotMapped]
        public int? nuoc { get; set; }
        [NotMapped]
        public double? tiendien { get; set; }
       
        [NotMapped]
        public double? tiennuoc { get; set; }
    }
}
