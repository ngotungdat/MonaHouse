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
    public class RoomRequest : AppDomainRequest
    {
        public string Name { get; set; }
        /// <summary>
        /// Dãy trọ
        /// </summary>
        public int? BranchId { get; set; }
        /// <summary>
        /// Tầng/Khu/Dãy
        /// </summary>
        public string BranchFloor { get; set; }
        /// <summary>
        /// Giá phòng
        /// </summary>
        public double? Price { get; set; }
        /// <summary>
        /// Diện tích phòng
        /// </summary>
        public string Acreage { get; set; }
        /// <summary>
        /// Ngày dự kiến phòng trống (dựa vào hợp đồng đăng ký phòng để tính)
        /// </summary>
        public DateTime? EmptyRoomDate { get; set; }
        /// <summary>
        /// Số tiền cọc
        /// </summary>
        public double? Deposit { get; set; }
        /// <summary>
        /// Trạng thái phòng 
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// Số lượng giường - đối với ký túc xá
        /// </summary>
        public int? BedAmount { get; set; }
        public int? RoomTypeId { get; set; }

    }
}
