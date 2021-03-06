using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class Room : DomainEntities.AppDomain
    {
        public string Name { get; set; }
        /// <summary>
        /// Dãy trọ
        /// </summary>
        public int? BranchId { get; set; }
        /// <summary>
        /// Tầng
        /// </summary>
        public int? FloorId { get; set; }
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
        /// <summary>
        /// Loại phòng
        /// </summary>
        public int? RoomTypeId { get; set; }
        /// <summary>
        /// Giá Điện
        /// </summary>
        public double? ElectricPrice { get; set; }
        /// <summary>
        /// Giá nước
        /// </summary>
        public double? WaterPrice { get; set; }
        /// <summary>
        /// người đại diện phòng
        /// </summary>
        public int? UserInRoomRepresentative { get; set; }
        /// <summary>
        /// Ngày dọn vào
        /// </summary>
        public DateTime? DateInToRoom { get; set; }
        /// <summary>
        /// Ngày dọn  ra
        /// </summary>
        public DateTime? DateGetOutRoom { get; set; }
        /// <summary>
        /// Gói Điện nước
        /// </summary>
        public int? ElectricWaterPackage { get; set; }

        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public string Phone { get; set; }
        [NotMapped]
        public string RoomTypeName { get; set; }
        [NotMapped]
        public string BranchName { get; set; }
        [NotMapped]
        public string FloorName { get; set; }
        [NotMapped]
        public double? DebtMoney { get; set; }
    }
}
