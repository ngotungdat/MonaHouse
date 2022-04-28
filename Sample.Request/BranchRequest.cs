using Sample.Entities;
using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class BranchRequest : AppDomainRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập tên nhà!")]
        public string Name { get; set; }
        /// <summary>
        /// Loại
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Mô tả thêm, quy định
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Danh sách hình ảnh
        /// </summary>
        public IList<BranchImageRequest> BranchImages { get; set; }

        public int RoomCount { get; set; }

        public int RoomBlank { get; set; }

        public int UserCount { get; set; }
    }
}
