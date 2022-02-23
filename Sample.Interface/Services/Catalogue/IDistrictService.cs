using Sample.Entities.Catalogue;
using Sample.Entities.DomainEntities;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Interface.Services.Catalogue
{
    public interface IDistrictService : ICatalogueService<Districts, BaseSearch>
    {
    }
}
