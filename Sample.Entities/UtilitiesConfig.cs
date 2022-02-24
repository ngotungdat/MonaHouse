using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class UtilitiesConfig : DomainEntities.AppDomain
    {
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Giá dịch vụ
        /// </summary>
        public double? Price { get; set; }

    }
}
