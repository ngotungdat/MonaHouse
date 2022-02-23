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

        /// <summary>
        /// Mã người dùng
        /// </summary>
        public int? UserId { get; set; }
    }
}
