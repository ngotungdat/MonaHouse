using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Entities
{
    public class PackageOfUser : DomainEntities.AppDomain
    {
        /// <summary>
        /// ID user
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// ID gói phần mềm
        /// </summary>

        [Required]
        public int PackageId { get; set; }

        /// <summary>
        /// Trạng thái gói đăng kí của người dùng
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// ngày admin duyệt gói
        /// </summary>
        public DateTime? AcceptDate { get; set; }

        /// <summary>
        /// ngày hết hạn gói theo ngày được duyệt
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// phương thức thanh toán
        /// </summary>
        public int? PaymentMethod { get; set; }

        /// <summary>
        /// ghi chú
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// số phòng giới hạn
        /// </summary>
        public int? RoomLimited { get; set; }

        /// <summary>
        /// tên gói
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// giá gói
        /// </summary>
        public double? PackagePrice { get; set; }

        /// <summary>
        /// Mô tả gói
        /// </summary>
        public string PackageDescription { get; set; }

        /// <summary>
        /// thông tin thêm
        /// </summary>
        public string PackageMoreInfo { get; set; }

        /// <summary>
        /// Ngày giới hạn
        /// </summary>
        public int? UsedDate { get; set; }

        // not mapping
        [NotMapped]
        public string Avatar { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public int? UserStatus { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        [NotMapped]
        public string Address { get; set; }
        [NotMapped]
        public string Phone { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public double? DebtMoney { get; set; }
        [NotMapped]
        public DateTime? DateOfBirth { get; set; }
        [NotMapped]
        public string CitizenIdentification { get; set; }
        [NotMapped]
        public int? GenderNumber { get; set; }
        [NotMapped]
        public string TitleDescription { get; set; }
        [NotMapped]
        public int UserdTime { get; set; }
        [NotMapped]
        public double? Price { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public int? LimitedRoom { get; set; }
        [NotMapped]
        public int PackageType { get; set; }
        [NotMapped]
        public string Title { get; set; }
        [NotMapped]
        public int TypeOfRental { get; set; }
    }
}
