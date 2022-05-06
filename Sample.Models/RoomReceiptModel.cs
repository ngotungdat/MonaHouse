using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class RoomReceiptModel : AppDomainModel
    {
        public int RoomId { get; set; }
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
    }
}
