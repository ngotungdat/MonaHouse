using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class BranchUtilities : DomainEntities.AppDomain
    {
        /// <summary>
        /// Trung tâm 
        /// </summary>
        public int? BranchId { get; set; }
        /// <summary>
        /// Tiện ích
        /// </summary>
        public int? UtilitiesId { get; set; }
    }
}
