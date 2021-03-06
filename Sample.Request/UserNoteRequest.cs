using Sample.Entities;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class UserNoteRequest : AppDomainRequest
    {
        public string Content { get; set; }
        public int UserID { get; set; }
    }
}
