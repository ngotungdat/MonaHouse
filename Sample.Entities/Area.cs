using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class Area : DomainEntities.AppDomain
    {
        /// <summary>
        /// Tên tỉnh
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
