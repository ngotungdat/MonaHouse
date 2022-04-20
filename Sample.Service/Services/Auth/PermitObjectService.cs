using AutoMapper;
using Sample.Service.Services.DomainServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Entities.DomainEntities;
using Sample.Interface.UnitOfWork;
using Sample.Interface.Services.Auth;
using Sample.Entities.Auth;

namespace Sample.Service.Services.Auth
{
    public class PermitObjectService : DomainService<PermitObjects, BaseSearch>, IPermitObjectService
    {
        public PermitObjectService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        protected override string GetStoreProcName()
        {
            return "PermitObject_GetPagingData";
        }
    }
}
