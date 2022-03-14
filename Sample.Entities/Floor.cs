using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class Floor : DomainEntities.AppDomain
    {
        /// <summary>
        /// ID của tòa nhà/dãy trọ
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// Tên dãy nhà
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
    }
}
