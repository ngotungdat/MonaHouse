using Sample.Entities.DomainEntities;
using Sample.Interface.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Service.Services.DomainServices;
using Sample.Interface.Services.Auth;
using Sample.Entities.Auth;

namespace Sample.Service.Services.Auth
{
    public class PermitObjectPermissionService : DomainService<PermitObjectPermissions, BaseSearch>, IPermitObjectPermissionService
    {
        public PermitObjectPermissionService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
