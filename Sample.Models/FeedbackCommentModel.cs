using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class FeedbackCommentModel : AppDomainModel
    {
        public string FeedbackId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserImg { get; set; }

    }
}
