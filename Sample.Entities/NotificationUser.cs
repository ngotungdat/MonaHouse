using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class NotificationUser : DomainEntities.AppDomain
    {
        [Required]
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
