using Sample.Entities;
using Sample.Entities.Auth;
using Sample.Entities.DomainEntities;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Interface.Services.Auth
{
    public interface IPermissionService : ICatalogueService<Permissions, BaseSearch>
    {
    }
}
