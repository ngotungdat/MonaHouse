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
    public class FeedbackCommentRequest : AppDomainRequest
    {
        public string FeedbackId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserImg { get; set; }

    }
}
