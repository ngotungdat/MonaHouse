using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class UserNoteModel: AppDomainModel
    {
        public string Content { get; set; }
        public int UserID { get; set; }
    }
}
