using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class PackageModel : AppDomainModel
    {
        /// <summary>
        /// Tên gói phần mềm
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Mô tả cho title
        /// </summary>
        public string TitleDescription { get; set; }
        /// <summary>
        /// Số phòng tối đa
        /// </summary>
        public int LimitedRoom { get; set; }
        /// <summary>
        /// Giá gói phần mềm
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Mô tả thêm cho gói
        /// </summary>
        public string Description { get; set; }
    }
}
