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
    public class RoomContractRepresentativeRequest : AppDomainRequest
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime? DateIntoRoom { get; set; }
        public int Deposit { get; set; }
        public int Prepay { get; set; }
        public int PaymentMethod { get; set; }
        public double TotalMoney { get; set; }
        public double TakeMoney { get; set; }
        public double DebtMoney { get; set; }
        public string HomeTown { get; set; }
        public int NumCusInRoom { get; set; }
        public string Relationship { get; set; }
    }
}
