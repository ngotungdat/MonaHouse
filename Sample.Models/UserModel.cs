//using Sample.Models.Auth;
using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Sample.Utilities.CoreContants;

namespace Sample.Models
{

    public class UserModel : AppDomainModel
    {
        public string UserName { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Tên trạng thái
        /// </summary>
        public string StatusName
        {
            get
            {
                switch (Status)
                {
                    case (int)StatusUser.Active:
                        return "Đã kích hoạt";
                    case (int)StatusUser.NotActive:
                        return "Chưa kích hoạt";
                    case (int)StatusUser.Locked:
                        return "Đang bị khóa";
                    default:
                        return string.Empty;
                }
            }
        }
        public int GenderNumber { get; set; }
        public string GenderName
        {
            get
            {
                switch (GenderNumber)
                {
                    case (int)Gender.Nam:
                        return "Nam";
                    case (int)Gender.Nu:
                        return "Nữ";
                    default:
                        return "Không xác định";
                }
            }
        }
        public int RoleNumber { get; set; }
        public string RoleName
        {
            get
            {
                switch (RoleNumber)
                {
                    case (int)RoleUser.Admin:
                        return "Admin";
                    case (int)RoleUser.CSKH:
                        return "Chăm sóc khách hàng";
                    case (int)RoleUser.ChuTro:
                        return "Chủ nhà";
                    case (int)RoleUser.NhanVien:
                        return "Nhân viên";
                    case (int)RoleUser.ThueTro:
                        return "Khách thuê";
                    default:
                        return "Chưa xác định";
                }
            }
        }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Phải là admin không
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Mật khẩu người dùng
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Cờ kiểm tra OTP của user
        /// </summary>
        //public bool IsCheckOTP { get; set; }
    }

}
