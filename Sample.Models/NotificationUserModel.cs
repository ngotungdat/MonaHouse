//using Sample.Models.Auth;
using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Sample.Utilities.CoreContants;

namespace Sample.Models
{
    public class NotificationUserModel : AppDomainModel
    {
        public int? UsersId { get; set; }
        public int? NotificationId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// true-Đã xem false-Chưa xem
        /// </summary>
        [DefaultValue(false)]
        public bool? isSeen { get; set; }
    }
}
