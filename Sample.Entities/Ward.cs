using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class Ward : DomainEntities.AppDomain
    {
        /// <summary>
        /// Tên
        /// </summary>
        [Required]
        public int? Name { get; set; }
        /// <summary>
        /// Tỉnh
        /// </summary>
        public int? AreaID { get; set; }
        public string AreaName { get; set; }
        /// <summary>
        /// Huyện
        /// </summary>
        public int? DistrictID { get; set; }
        public string DistrictName { get; set; }
    }
}
