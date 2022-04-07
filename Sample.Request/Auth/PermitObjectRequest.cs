using Sample.Request.DomainRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Request.Auth
{
    public class PermitObjectRequest : AppDomainCatalogueRequest
    {
        /// <summary>
        /// Tên controller
        /// </summary>
        /// 
        public string ControllerNames { get; set; }

        #region Extension Properties

        /// <summary>
        /// Danh sách tên controller
        /// </summary>
        /*public string Controllers { get; set; }*/


        /*public void ToModel()
        {
            ControllerNames = string.Join(";", Controllers);
        }

        public void ToView()
        {
            if (!string.IsNullOrEmpty(ControllerNames))
            {
                Controllers = ControllerNames.Split(";");
            }
        }*/


        #endregion
    }
}
