using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class DebtCollection : DomainEntities.AppDomain
    {
        public int? UserId { get; set; }
        public double? DebtConllection { get; set; }
        public double? DebtRemaining { get; set; }
        public DateTime? ConllecttionDate { get; set; }
        public string note { get; set; }
        public int? Status { get; set; }
    }
}
