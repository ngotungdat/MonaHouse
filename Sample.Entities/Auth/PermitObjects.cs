using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sample.Entities.Auth
{
    /// <summary>
    /// Chức năng người dùng
    /// </summary>
    public class PermitObjects : DomainEntities.AppDomain
    {
        /// <summary>
        /// Tên controller
        /// </summary>
        public string ControllerNames { get; set; }

        #region Extension Properties

        /// <summary>
        /// Danh sách tên controller
        /// </summary>
        /*[NotMapped]*/
        /*public IList<string> Controllers
        {
            get
            {
                return (!string.IsNullOrEmpty(ControllerNames)) ? ControllerNames.Split(';').ToList() : new List<string>();
            }
        }*/

        #endregion

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(500)]
        [Required]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }
}
