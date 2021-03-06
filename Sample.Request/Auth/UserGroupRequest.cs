using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Request.Auth
{
    public class UserGroupRequest : AppDomainRequest
    {
        #region Extension Properties

        /// <summary>
        /// List id user của nhóm
        /// </summary>
        public List<int> UserIds { get; set; }

        /// <summary>
        /// Người dùng thuộc nhóm
        /// </summary>
        public IList<UserInGroupRequest> UserInGroups { get; set; }

        /// <summary>
        /// Chức năng + quyền của nhóm
        /// </summary>
        public IList<PermitObjectPermissionRequest> PermitObjectPermissions { get; set; }

        #endregion
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}
