using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Request.Auth
{
    public class PermitObjectPermissionAddNewRequest : AppDomainRequest
    {
        /// <summary>
        /// Mã chức năng
        /// </summary>
        public string ? PermitObjectId { get; set; }

        /// <summary>
        /// Mã quyền
        /// </summary>
        public string ? PermissionId { get; set; }

        /// <summary>
        /// Mã nhóm
        /// </summary>
        public string ? UserGroupId { get; set; }

    }
}
