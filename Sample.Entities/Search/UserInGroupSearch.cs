using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Entities.Search
{
    public class UserInGroupSearch : DomainEntities.BaseSearch
    {
        /// <summary>
        /// Id người dùng
        /// </summary>
        //public int? UserId { get; set; }

        ///// <summary>
        ///// Nhóm người dùng
        ///// </summary>
        public int? UserGroupId { get; set; }

        public int? DebtMoney { get; set; }
        public int? Status { get; set; }
    }
}
