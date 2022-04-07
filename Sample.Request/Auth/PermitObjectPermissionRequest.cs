using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Request.Auth
{
    public class PermitObjectPermissionRequest : AppDomainRequest
    {
        /// <summary>
        /// Mã chức năng
        /// </summary>
        public int? PermitObjectId { get; set; }

        /// <summary>
        /// Mã quyền
        /// </summary>
        public int? PermissionId { get; set; }

        /// <summary>
        /// Mã nhóm
        /// </summary>
        public int? UserGroupId { get; set; }

        public string PermitObjectsCode { get; set; }
        public string PermitObjectsName { get; set; }
        public string PermitObjectsDescription { get; set; }

        public string PermissionsCode { get; set; }
        public string PermissionsName { get; set; }
        public string PermissionsDescription { get; set; }

        public string UserGroupsCode { get; set; }
        public string UserGroupsName { get; set; }
        public string UserGroupsDescription { get; set; }


        /// <summary>
        /// Mã người dùng
        /// </summary>
        /*public int? UserId { get; set; }*/
    }
}
