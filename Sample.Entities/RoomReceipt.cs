using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class RoomReceipt : DomainEntities.AppDomain
    {
        public int? RoomId { get; set; }
        public string UserId { get; set; }
        public DateTime? Date { get; set; }
        public double? ElectricBill { get; set; }
        public double? WaterBill { get; set; }
        public double? RoomUtilitieBill { get; set; }
        public double? RoomBill { get; set; }
        public double? TotalBill { get; set; }
        public double? PlusBill { get; set; }
        public double? SubBill { get; set; }
        public double? FinalBill { get; set; }
        public int? Status { get; set; }
        public string Note { get; set; }

        public int? PaymenMethod { get; set; }
        public double? MoneyRecive { get; set; }
        public double? MoneyDebtRoomReceipt { get; set; }
        public string NoteRecive { get; set; }

        // not mapping
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public double DebtMoney { get; set; }
    }
}
