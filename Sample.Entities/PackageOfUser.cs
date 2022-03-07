using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class PackageOfUser : DomainEntities.AppDomain
    {
        /// <summary>
        /// ID user
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// ID gói phần mềm
        /// </summary>

        [Required]
        public int PackageId { get; set; }
    }
}
