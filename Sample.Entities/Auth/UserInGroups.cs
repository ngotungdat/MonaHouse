using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities.Auth
{
    /// <summary>
    /// Người dùng thuộc nhóm
    /// </summary>
    public class UserInGroups : DomainEntities.AppDomain
    {
        ///// <summary>
        ///// Người dùng
        ///// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Nhóm người dùng
        /// </summary>
        public int UserGroupId { get; set; }

        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public string UserGroupName { get; set; }
        [NotMapped]
        public string Phone { get; set; }
        [NotMapped]
        public string Email{ get; set; }
        [NotMapped]
        public string CitizenIdentification { get; set; }
        [NotMapped]
        public string Address { get; set; }
        [NotMapped]
        public DateTime? DateOfBirth { get; set; }
        [NotMapped]
        public DateTime? UserCreated { get; set; }
        [NotMapped]
        public int? Status { get; set; }
        #region Extension Properties

        /// <summary>
        /// Lấy thông tin Người dùng
        /// </summary>
        [NotMapped]
        public Users Users { get; set; }

        /// <summary>
        /// Lấy thông tin Nhóm người dùng
        /// </summary>
        [NotMapped]
        public UserGroups UserGroups { get; set; }

        #endregion


    }
}
