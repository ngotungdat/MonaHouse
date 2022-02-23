using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Entities.Catalogue
{
    /// <summary>
    /// Phường
    /// </summary>
    public class Wards : AppDomainCatalogue
    {
        /// <summary>
        /// Mã quận
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Mã thành phố
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Tên thành phố
        /// </summary>
        [StringLength(1000)]
        public string CityName { get; set; }

        /// <summary>
        /// Tên quận trực thuộc
        /// </summary>
        [StringLength(1000)]
        public string DistrictName { get; set; }
    }
}
