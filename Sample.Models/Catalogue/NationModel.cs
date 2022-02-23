using Sample.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Models.Catalogue
{
    public class NationModel : AppDomainCatalogueModel
    {
        /// <summary>
        /// Mã quốc gia
        /// </summary>
        public int? CountryId { get; set; }
    }
}
