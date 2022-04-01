using Sample.Request.DomainRequests;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sample.Request
{
    public class FeedbackRequest : AppDomainRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Evaluate { get; set; }
        public string FeedbackTypeId { get; set; }
        public string Status { get; set; }
        public string Prioritized { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public string UserImg { get; set; }
        public string Handler { get; set; }

    }
}
