using Sample.Request.Auth;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Request
{
    public class NotificationRequest : AppDomainRequest
    {
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
