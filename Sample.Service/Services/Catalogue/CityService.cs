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
    public class CityService : CatalogueService<Cities, BaseSearch>, ICityService
    {
        public CityService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
