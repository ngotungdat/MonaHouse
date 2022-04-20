using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Models.Auth
{
    /// <summary>
    /// Nhóm người dùng
    /// </summary>
    public class UserGroupModel : AppDomainModel
    {

        #region Extension Properties

        /// <summary>
        /// List id user của nhóm
        /// </summary>
        public List<int> UserIds { get; set; }

        /// <summary>
        /// Người dùng thuộc nhóm
        /// </summary>
        public IList<UserInGroupModel> UserInGroups { get; set; }

        /// <summary>
        /// Chức năng + quyền của nhóm
        /// </summary>
        public IList<PermitObjectPermissionModel> PermitObjectPermissions { get; set; }

        #endregion
      
        public string Code { get; set; }
       
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}
