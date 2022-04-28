using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class UserNote : DomainEntities.AppDomain
    {
        public string Content { get; set; }
        public int UserID { get; set; }
    }
}
