using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Sample.Entities
{
    public class UserInRoom : DomainEntities.AppDomain
    {
        public int? UsersId { get; set; }
        /// <summary>
        /// Quê quán
        /// </summary>
        public string Village { get; set; }
        /// <summary>
        /// Ảnh chứng minh thư
        /// </summary>
        public string CMT { get; set; }
        /// <summary>
        /// Dãy trọ
        /// </summary>
        public int? BranchId { get; set; }
        /// <summary>
        /// Phòng
        /// </summary>
        public int? RoomId { get; set; }
        /// <summary>
        /// Giường
        /// </summary>
        public int? Bed { get; set; }
        /// <summary>
        /// Ngày vào ở
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Tạm trú: true - đã đăng ký tạm trú
        /// </summary>
        public bool? TamTru { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public int? Status { get; set; }
    }
}
