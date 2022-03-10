using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHouse.MVC.Models
{
    public class CoreModel
    {
        /// <summary>
        /// Token hiện tại
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Link api
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Object truyền vào thêm
        /// </summary>
        public object MyProperty { get; set; }
        /// <summary>
        /// Người dùng hiện tại
        /// </summary>
        public Users Users { get; set; }
    }
}
