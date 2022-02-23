using Sample.Entities.DomainEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sample.Entities.Catalogue
{
    /// <summary>
    /// Thành phố
    /// </summary>
    public class Cities : AppDomainCatalogue
    {
        /// <summary>
        /// Mã quốc gia
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Tên thành phố
        /// </summary>
        [StringLength(1000)]
        public string CountryName { get; set; }
    }
}
