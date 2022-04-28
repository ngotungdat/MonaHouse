using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sample.Entities
{
    public class Branch : DomainEntities.AppDomain
    {
        /// <summary>
        /// Tên dãy nhà
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Loại
        /// </summary>
        [Required]
        public int? Type { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Hình thức cho thuê
        /// </summary>
        [Required]
        public int? RentalForm { get; set; }
        /// <summary>
        /// Tỉnh
        /// </summary>
        public int? AreaId { get; set; }
        /// <summary>
        /// Huyện
        /// </summary>
        public int? District { get; set; }
        /// <summary>
        /// Xã
        /// </summary>
        public int? Ward { get; set; }
        /// <summary>
        /// Tiền diện trên 1kwh
        /// </summary>
        public double? ElectricFee { get; set; }
        /// <summary>
        /// Tiền nước
        /// </summary>
        public double? WaterFee { get; set; }
        /// <summary>
        /// 1 - Phí trên 1 người, 2 - Phí trên 1 khối
        /// </summary>
        public int? WaterFeeType { get; set; }
        /// <summary>
        /// Ngày ghi điện nước
        /// </summary>
        public DateTime? RecordDate { get; set; }
        /// <summary>
        /// Ngày chốt hóa đơn
        /// </summary>
        public DateTime? DateClosingFee { get; set; }
        /// <summary>
        /// true - Tính phí giữ xe
        /// </summary>
        public bool? isParkingFee { get; set; }
        /// <summary>
        /// Mô tả thêm
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Nội quy phòng trọ
        /// </summary>
        public string Rules { get; set; }
        /// <summary>
        /// Tồng số phòng
        /// </summary>
        public int? RoomAmount { get; set; }
        /// <summary>
        /// Tổng số phòng trống
        /// </summary>
        public int? EmptyRoomAmount { get; set; }
        /// <summary>
        /// Tổng số giường
        /// </summary>
        public int? BedAmount { get; set; }
        /// <summary>
        /// Tổng số giường trống
        /// </summary>
        public int? EmptyBedAmount { get; set; }
        /// <summary>
        /// Danh sách hình ảnh
        /// </summary>
        [NotMapped]
        public IList<BranchImage> BranchImages { get; set; }
        /// <summary>
        /// Hình ảnh đại diện cho nhà
        /// </summary>
        [NotMapped]
        public string LinkImage { get; set; }
        /// <summary>
        /// Danh sách hình ảnh nối chuỗi từ store
        /// </summary>
        [NotMapped]
        public string LinkImages { get; set; }

        [NotMapped]
        public int RoomCount { get; set; }

        [NotMapped]
        public int RoomBlank { get; set; }

        [NotMapped]
        public int UserCount { get; set; }

    }
}
