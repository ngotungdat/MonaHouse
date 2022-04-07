using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services
{
    public interface ILicenseSampleService : ICatalogueService<LicenseSample, Sample.Entities.DomainEntities.BaseSearch>
    {
    }
}
