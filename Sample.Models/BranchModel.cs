using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class BranchModel: AppDomainModel
    {
        public string Name { get; set; }
        /// <summary>
        /// Loại
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Mô tả thêm
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Hình ảnh đại diện cho nhà
        /// </summary>
        public string LinkImage { get; set; }
        /// <summary>
        /// Danh sách hình ảnh nối chuỗi từ store
        /// </summary>
        public string LinkImages { get; set; }

        public int RoomCount { get; set; }

        public int RoomBlank { get; set; }

        public int UserCount { get; set; }
    }
}
