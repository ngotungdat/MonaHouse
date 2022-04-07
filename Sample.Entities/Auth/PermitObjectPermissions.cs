using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities.Auth
{
    /// <summary>
    /// Danh mục quyền ứng với chức năng người dùng
    /// </summary>
    public class PermitObjectPermissions : DomainEntities.AppDomain
    {
        /// <summary>
        /// Chức năng
        /// </summary>
        public int PermitObjectId { get; set; }

        /// <summary>
        /// Quyền
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// Nhóm người dùng
        /// </summary>
        public int? UserGroupId { get; set; }
        [NotMapped]
        public string PermitObjectsCode { get; set; }
        [NotMapped]
        public string PermitObjectsName { get; set; }
        [NotMapped]
        public string PermitObjectsDescription { get; set; }
        [NotMapped]
        public string PermissionsCode { get; set; }
        [NotMapped]
        public string PermissionsName { get; set; }
        [NotMapped]
        public string PermissionsDescription { get; set; }
        [NotMapped]
        public string UserGroupsCode { get; set; }
        [NotMapped]
        public string UserGroupsName { get; set; }
        [NotMapped]
        public string UserGroupsDescription { get; set; }
        ///// <summary>
        ///// Người dùng
        ///// </summary>
        //public int? UserId { get; set; }
    }
}
