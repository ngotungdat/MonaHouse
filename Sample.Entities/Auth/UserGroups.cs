using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities.Auth
{
    /// <summary>
    /// Nhóm người dùng
    /// </summary>
    public class UserGroups : DomainEntities.AppDomain
    {

        #region Extension Properties

        [NotMapped]
        public List<int> UserIds { get; set; }

        /// <summary>
        /// Người dùng thuộc nhóm
        /// </summary>
        [NotMapped]
        public IList<UserInGroups> UserInGroups { get; set; }

        /// <summary>
        /// Chức năng + quyền của nhóm
        /// </summary>
        [NotMapped]
        public IList<PermitObjectPermissions> PermitObjectPermissions { get; set; }

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
