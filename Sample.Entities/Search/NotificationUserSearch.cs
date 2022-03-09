using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Entities.Search
{
    public class NotificationUserSearch : BaseSearch
    {
        /// <summary>
        /// User người dùng
        /// </summary>
        public int? UsersID { get; set; }
        /// <summary>
        /// true-Đã xem false-Chưa xem
        /// </summary>
        public bool? isSeen { get; set; }
    }
}
