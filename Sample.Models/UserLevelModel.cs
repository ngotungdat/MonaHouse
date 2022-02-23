using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class UserLevelModel : AppDomainModel
    {
        /// <summary>
        /// Cấp người dùng
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Chiết khấu phí mua hàng (%)	
        /// </summary>
        public double? FeeBuyPro { get; set; }

        /// <summary>
        /// Chiết khấu phí vận chuyển TQ - VN (%)
        /// </summary>
        public double? FeeWeight { get; set; }

        /// <summary>
        /// Đặt cọc tối thiểu (%)
        /// </summary>
        public double? LessDeposit { get; set; }

        /// <summary>
        /// Tiền từ (VNĐ)
        /// </summary>
        public double? Money { get; set; }

        /// <summary>
        /// Tiền đến (VNĐ)
        /// </summary>
        public double? MoneyTo { get; set; }
    }
}
