using Sample.Entities;
using Sample.Entities.Auth;
using Sample.Entities.DomainEntities;
using Sample.Interface.Services.DomainServices;
using Sample.Models;
using Sample.Request.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services.Auth
{
    public interface IPermitObjectPermissionService : IDomainService<PermitObjectPermissions, BaseSearch>
    {
        public Task<bool> AddNewPermitObjectPermissions(PermitObjectPermissionAddNewRequest permitObjectPermissionRequest);
        public Task<IList<PermitObjectPermissions>> GetPermitObjectPermissionsByPermitObjectId(int item);
        public Task<bool> UpdatePermitObjectPermissions(PermitObjectPermissionAddNewRequest permitObjectPermissionAddNewRequest);
        public Task<bool> DeletePermitObjectPermissionsById(int id);

    }
}
