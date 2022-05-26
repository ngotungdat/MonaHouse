using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sample.Utilities.CoreContants;

namespace Sample.Models
{
    public class PackageOfUserModel : AppDomainModel
    {
        /// <summary>
        /// ID user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// ID gói phần mềm
        /// </summary>
        public int PackageId { get; set; }

        /// <summary>
        /// Trạng thái gói đăng kí của người dùng
        /// </summary>
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

        public string UserName { get; set; }

        public int? UserStatus { get; set; }

        public string Avatar { get; set; }
        
        public string FullName { get; set; }
        
        public string Address { get; set; }
        
        public string Phone { get; set; }
        
        public string Email { get; set; }
        
        public double? DebtMoney { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string CitizenIdentification { get; set; }
        
        public int? GenderNumber { get; set; }
        
        public string TitleDescription { get; set; }
        
        public int UserdTime { get; set; }
        
        public double? Price { get; set; }
        
        public string Description { get; set; }
        
        public int? LimitedRoom { get; set; }
        
        public int PackageType { get; set; }
        
        public string Title { get; set; }
        
        public int TypeOfRental { get; set; }

    }
}
