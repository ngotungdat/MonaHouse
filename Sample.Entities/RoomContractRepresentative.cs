using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class RoomContractRepresentative : DomainEntities.AppDomain
    {
        public int? RoomId { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateIntoRoom { get; set; }
        public DateTime? DateGoOutRoom { get; set; }
        public int? Deposit { get; set; }
        public int? Prepay { get; set; }
        public double? DepositMoney { get; set; }
        public double? PrepayMoney { get; set; }
        public int? PaymentMethod { get; set; }
        public double? TotalMoney { get; set; }
        public double? TakeMoney { get; set; }
        public double? DebtMoney { get; set; }
        public double? UserMoney { get; set; }
        public string HomeTown { get; set; }
        public int? NumCusInRoom { get; set; }
        public int? Status{ get; set; }
        public string Relationship { get; set; }
    }
}
