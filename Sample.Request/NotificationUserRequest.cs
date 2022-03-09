using Sample.Request.Auth;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Request
{
    public class NotificationUserRequest : AppDomainRequest
    {
        public int? UsersId { get; set; }
        [DefaultValue(0)]
        public int? NotificationId { get; set; }
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// true-Đã xem false-Chưa xem
        /// </summary>
        public bool? isSeen { get; set; }
    }
}
