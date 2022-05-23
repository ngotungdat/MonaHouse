using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class DebtCollectionModel : AppDomainModel
    {
        public int? UserId { get; set; }
        public double? DebtConllection { get; set; }
        public double? DebtRemaining { get; set; }
        public string note { get; set; }
        public DateTime? ConllecttionDate { get; set; }
        public int? Status { get; set; }
    }
}
