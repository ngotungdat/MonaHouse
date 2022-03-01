using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class PackageRequest: AppDomainRequest
    {
        /// <summary>
        /// Tên gói phần mềm
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập tên gói phần mềm!")]
        public string Title { get; set; }
        /// <summary>
        /// Mô tả cho title
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập mô tả cho tiêu đề!")]
        public string TitleDescription { get; set; }
        /// <summary>
        /// Số phòng tối đa
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập giới hạn số phòng!")]
        public int LimitedRoom { get; set; }
        /// <summary>
        /// Giá gói phần mềm
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập giá gói phần mềm!")]
        public double Price { get; set; }
        /// <summary>
        /// Mô tả thêm cho gói
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập mô tả thêm!")]
        public string Description { get; set; }
    }
}
