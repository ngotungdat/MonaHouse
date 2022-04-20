using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Models.Auth
{
    /// <summary>
    /// Chức năng người dùng
    /// </summary>
    public class PermitObjectModel : AppDomainModel
    {
        /// <summary>
        /// Tên controller
        /// </summary>
        public string ControllerNames { get; set; }

        #region Extension Properties

        /// <summary>
        /// Danh sách tên controller
        /// </summary>
        public IList<string> Controllers { get; set; }


        public void ToModel()
        {
            ControllerNames = string.Join(";", Controllers);
        }

        public void ToView()
        {
            if (!string.IsNullOrEmpty(ControllerNames))
            {
                Controllers = ControllerNames.Split(";");
            }
        }


        #endregion
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
