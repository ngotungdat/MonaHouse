//using Sample.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Sample.Entities
{
    public class Users : DomainEntities.AppDomain
    {

        /// <summary>
        /// UserName
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        /// <summary>
        /// Mật khẩu người dùng
        /// </summary>
        [StringLength(4000)]
        public string Password { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        [StringLength(200)]
        public string FullName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [StringLength(12)]
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [StringLength(1000)]
        public string Address { get; set; }

        /// <summary>
        /// Trạng thái (1: Đã kích hoạt, 2: Chưa kích hoạt, 3: Đang bị khóa)
        /// </summary>
        public int? Status { get; set; }
        public int RoleNumber { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int GenderNumber { get; set; }
        public string CitizenIdentification { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Phải là admin không
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// Token đăng nhập
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Thời gian hết hạn token
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// ID nhóm người dùng được chọn
        /// </summary>
        [NotMapped]
        public int UserGroupId { get; set; }
        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Giới tính
        /// 0 => Nữ
        /// 1 => Nam
        /// </summary>
        //public bool? Gender { get; set; }

        /// <summary>
        /// Cờ kiểm tra OTP của user
        /// </summary>
        //[DefaultValue(false)]
        //public bool IsCheckOTP { get; set; }

        ///// <summary>
        ///// Cờ check login = facebook
        ///// </summary>
        //[DefaultValue(false)]
        //public bool IsLoginFaceBook { get; set; }

        ///// <summary>
        ///// Cờ check login = google
        ///// </summary>
        //[DefaultValue(false)]
        //public bool IsLoginGoogle { get; set; }

        ///ghi chú
    }
}
