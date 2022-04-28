using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities.Search
{
    public class UserNoteSearch : DomainEntities.BaseSearch
    {
        public int UserID { get; set; }
    }
}
