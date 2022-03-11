using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Request
{
    public class FloorRequest: AppDomainRequest
    {
        /// <summary>
        /// Tầng nhà/ dãy trọ
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập tên tầng!")]
        [StringLength(500, ErrorMessage = "Số kí tự của tên phải nhỏ hơn 500!")]
        public string Name { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }
    }
}
