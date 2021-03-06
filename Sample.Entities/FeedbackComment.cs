using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class FeedbackComment : DomainEntities.AppDomain
    {
        [Required]
        public string FeedbackId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string UserId { get; set; }
        public string UserImg { get; set; }
    }
}
