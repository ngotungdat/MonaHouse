using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Models.Catalogue
{
    public class WardModel : AppDomainCatalogueModel
    {
        /// <summary>
        /// Quận
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Mã thành phố
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Tên thành phố
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Tên quận trực thuộc
        /// </summary>
        public string DistrictName { get; set; }
    }
}
