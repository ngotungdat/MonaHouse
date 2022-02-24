using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class District : DomainEntities.AppDomain
    {
        /// <summary>
        /// Tên huyện
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Id tỉnh
        /// </summary>
        public int? AreaId { get; set; }
        public string AreaName { get; set; }
    }
}
