using AutoMapper;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class LicenseSampleService : CatalogueService<LicenseSample, Entities.DomainEntities.BaseSearch>, ILicenseSampleService
    {
        public LicenseSampleService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        protected override string GetStoreProcName()
        {
            return "LicenseSample_GetPagingData";
        }
    }
}
