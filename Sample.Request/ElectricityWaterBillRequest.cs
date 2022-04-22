using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class ElectricityWaterBillRequest : AppDomainRequest
    {
        public int RoomId { get; set; }
        public double? ElectricPrice { get; set; }
        public double? WaterPrice { get; set; }
        public double? OldNumberElectric { get; set; }
        public double? NewNumberElectric { get; set; }
        public double? OldNumberWater { get; set; }
        public double? NewNumberWater { get; set; }
        public DateTime? WriteDate { get; set; }
        public string WaterImage { get; set; }
        public string ElectricImage { get; set; }
        public double? ElectricBill { get; set; }
        public double? WaterBill { get; set; }
    }
}
