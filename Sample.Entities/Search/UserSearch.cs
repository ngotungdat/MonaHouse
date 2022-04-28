using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Entities.Search
{
    public class UserSearch : DomainEntities.BaseSearch
    {
        /// <summary>
        /// Tìm kiếm theo Email
        /// </summary>
        //public string Email { get; set; }
        ///// <summary>
        ///// Tìm kiếm theo số điện thoại
        ///// </summary>
        //public string Phone { get; set; }

        /// <summary>
        /// Theo nhom người dùng
        /// </summary>
        public int? UserGroupId { get; set; }
        public int? RoleNumber { get; set; }
    }
}
