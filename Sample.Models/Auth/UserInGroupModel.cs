using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Models.Auth
{
    /// <summary>
    /// Người dùng thuộc nhóm
    /// </summary>
    public class UserInGroupModel : AppDomainModel
    {
        /// <summary>
        /// Người dùng
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Nhóm người dùng
        /// </summary>
        public int UserGroupId { get; set; }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserGroupName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string CitizenIdentification { get; set; }
        public string Address { get; set; }
        public double? DebtMoney { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? UserCreated { get; set; }
        public int? Status { get; set; }
        public string Avatar { get; set; }
        #region Extension Properties

        /// <summary>
        /// Lấy thông tin Người dùng
        /// </summary>
        public UserModel Users { get; set; }

        /// <summary>
        /// Lấy thông tin Nhóm người dùng
        /// </summary>
        public UserGroupModel UserGroups { get; set; }

        #endregion
    }
}
