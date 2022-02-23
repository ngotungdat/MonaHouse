using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Request.Auth
{
    public class Role
    {
        /// <summary>
        /// Tên chức năng (menu)
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Quyền của chức năng
        /// </summary>
        public bool IsView { get; set; }
    }
}
