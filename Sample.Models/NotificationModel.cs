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
    public class NotificationModel : AppDomainModel
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsSendMail { get; set; }
    }
}
