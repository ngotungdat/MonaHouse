//using Sample.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Sample.Entities
{

    //public class Users : DomainEntities.AppDomain
    //{
    //    /// <summary>
    //    /// UserName
    //    /// </summary>
    //    [Required]
    //    [StringLength(50)]
    //    public string UserName { get; set; }

    //    /// <summary>
    //    /// Họ và tên
    //    /// </summary>
    //    [StringLength(200)]
    //    public string FullName { get; set; }

    //    /// <summary>
    //    /// Số điện thoại
    //    /// </summary>
    //    [StringLength(20)]
    //    public string Phone { get; set; }

    //    /// <summary>
    //    /// Email
    //    /// </summary>
    //    [StringLength(50)]
    //    public string Email { get; set; }

    //    /// <summary>
    //    /// Địa chỉ
    //    /// </summary>
    //    [StringLength(1000)]
    //    public string Address { get; set; }

    //    /// <summary>
    //    /// Trạng thái (1: Đã kích hoạt, 2: Chưa kích hoạt, 3: Đang bị khóa)
    //    /// </summary>
    //    public int? Status { get; set; }

    //    /// <summary>
    //    /// Ngày sinh
    //    /// </summary>
    //    public DateTime? Birthday { get; set; }

    //    /// <summary>
    //    /// Phải là admin không
    //    /// </summary>
    //    public bool IsAdmin { get; set; }

    //    /// <summary>
    //    /// Mật khẩu người dùng
    //    /// </summary>
    //    [StringLength(4000)]
    //    public string Password { get; set; }

    //    /// <summary>
    //    /// Token đăng nhập
    //    /// </summary>
    //    public string Token { get; set; }

    //    /// <summary>
    //    /// Thời gian hết hạn token
    //    /// </summary>
    //    public DateTime? ExpiredDate { get; set; }

    //    /// <summary>
    //    /// Giới tính
    //    /// 0 => Nữ
    //    /// 1 => Nam
    //    /// </summary>
    //    public bool? Gender { get; set; }

    //    /// <summary>
    //    /// Cờ kiểm tra OTP của user
    //    /// </summary>
    //    [DefaultValue(false)]
    //    public bool IsCheckOTP { get; set; }

    //    /// <summary>
    //    /// Cờ check login = facebook
    //    /// </summary>
    //    [DefaultValue(false)]
    //    public bool IsLoginFaceBook { get; set; }

    //    /// <summary>
    //    /// Cờ check login = google
    //    /// </summary>
    //    [DefaultValue(false)]
    //    public bool IsLoginGoogle { get; set; }

    //    #region Extension Properties

    //    /// <summary>
    //    /// Cờ xét reset mật khẩu
    //    /// </summary>
    //    [NotMapped]
    //    public bool IsResetPassword { get; set; }

    //    /// <summary>
    //    /// ID nhóm người dùng được chọn
    //    /// </summary>
    //    [NotMapped]
    //    public int UserGroupId { get; set; }

    //    ///// <summary>
    //    ///// Những nhóm người dùng thuộc
    //    ///// </summary>
    //    //[NotMapped]
    //    //public IList<UserInGroups> UserInGroups { get; set; }
    //    #endregion

    //    /// <summary>
    //    /// Cấp người dùng
    //    /// </summary>
    //    public int? LevelId { get; set; }

    //    /// <summary>
    //    /// Số dư (VNĐ)
    //    /// </summary>
    //    public double? Wallet { get; set; }

    //    /// <summary>
    //    /// ID nhân viên kinh doanh
    //    /// </summary>
    //    public int? SaleId { get; set; }

    //    /// <summary>
    //    /// ID nhân viên đặt hàng
    //    /// </summary>
    //    public int? DatHangId { get; set; }

    //    public int? VIPLevel { get; set; }

    //    /// <summary>
    //    /// Số dư (Tệ)
    //    /// </summary>
    //    public double? WalletCNY { get; set; }

    //    /// <summary>
    //    /// Kho TQ
    //    /// </summary>
    //    public int? WarehouseFrom { get; set; }

    //    /// <summary>
    //    /// Kho VN
    //    /// </summary>
    //    public int? WarehouseTo { get; set; }

    //    /// <summary>
    //    /// Tỉ giá riêng (Tệ)
    //    /// </summary>
    //    public double? Currency { get; set; }

    //    /// <summary>
    //    /// Phí mua hàng riêng (%)
    //    /// </summary>
    //    public double? FeeBuyPro { get; set; }

    //    /// <summary>
    //    /// Phí cân nặng riêng (VNĐ/KG)
    //    /// </summary>
    //    public double? FeeTQVNPerWeight { get; set; }

    //    /// <summary>
    //    /// Phần trăm đặt cọc (%)
    //    /// </summary>
    //    public double? Deposit { get; set; }

    //    /// <summary>
    //    /// Phương thức vận chuyển
    //    /// </summary>
    //    public int? ShippingType { get; set; }

    //    /// <summary>
    //    /// Kho TQ
    //    /// </summary>
    //    public int? WareHouseTQ { get; set; }

    //    /// <summary>
    //    /// Kho VN
    //    /// </summary>
    //    public int? WareHouseVN { get; set; }

    //    public string FeeTQVNPerVolume { get; set; }

    //    public string TienTichLuy { get; set; }

    //    public DateTime? DateUpLevel { get; set; }
    //}
    public class Users : DomainEntities.AppDomain
    {
        /// <summary>
        /// ID chủ của User
        /// </summary>
        //public Guid ChildOfUserID { get; set; }
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
        /// RoleName
        /// </summary>
        [StringLength(50)]
        public string RoleName { get; set; }
        public int GenderNumber { get; set; }
        /// <summary>
        /// GenderName
        /// </summary>
        [StringLength(50)]
        public string GenderName { get; set; }
        public string CitizenIdentification { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Phải là admin không
        /// </summary>
        //public bool IsAdmin { get; set; }


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
    }
}
