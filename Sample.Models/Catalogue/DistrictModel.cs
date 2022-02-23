using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Models.Catalogue
{
    public class DistrictModel : AppDomainCatalogueModel
    {
        /// <summary>
        /// Mã thành phố
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Tên thành phố
        /// </summary>
        public string CityName { get; set; }
    }
}
