using System;
using System.Collections.Generic;
using System.Text;
using Sample.Request.DomainRequests;

namespace Sample.Request.Auth
{
    public class UserInGroupRequest : AppDomainCatalogueRequest
    {
        /// <summary>
        /// Người dùng
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Nhóm người dùng
        /// </summary>
        public int UserGroupId { get; set; }

        #region Extension Properties

        /// <summary>
        /// Lấy thông tin Người dùng
        /// </summary>
        public UserRequest Users { get; set; }

        /// <summary>
        /// Lấy thông tin Nhóm người dùng
        /// </summary>
        public UserGroupRequest UserGroups { get; set; }

        #region prop_by_baoNguyen
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CitizenIdentification { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? UserCreated { get; set; }
        public int? Status { get; set; }
        #endregion
        #endregion
    }
}
