using Sample.Entities.DomainEntities;
using Sample.Interface.Services.Catalogue;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Entities.Catalogue;

namespace Sample.Service.Services.Catalogue
{
    public class CountryService : CatalogueService<Countries, BaseSearch>, ICountryService
    {
        public CountryService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
