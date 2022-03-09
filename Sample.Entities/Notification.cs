using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class Notification : DomainEntities.AppDomain
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
