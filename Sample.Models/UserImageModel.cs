using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class UserImageModel: AppDomainModel
    {
        public int? UserId { get; set; }
        /// <summary>
        /// link lưu hình ảnh
        /// </summary>
        public string Link { get; set; }
    }
}
