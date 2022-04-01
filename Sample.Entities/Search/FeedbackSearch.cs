using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities.Search
{
    public class FeedbackSearch : DomainEntities.BaseSearch
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
    }
}
