using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class Contract : DomainEntities.AppDomain
    {
        /// <summary>
        /// Nội dung hợp đồng
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Mã hợp đồng
        /// </summary>
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// Ngày bắt đầu hợp đồng
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Dãy nhà
        /// </summary>
        public int? BranchId { get; set; }
        /// <summary>
        /// Phòng
        /// </summary>
        public int? RoomId { get; set; }
        /// <summary>
        /// Giường 
        /// </summary>
        public int? BedId { get; set; }
        /// <summary>
        /// Số tháng
        /// </summary>
        public int? MonthAmount { get; set; }
        /// <summary>
        /// Giá thuê
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Đặt cọc
        /// </summary>
        public double? Deposit { get; set; }
        public int? UserInRoomId { get; set; }
        public string UserInRoomName { get; set; }
        /// <summary>
        /// Ngày kết thúc hợp đồng
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// trạng thái
        /// </summary>
        public int? Status { get; set; }
    } 
}
