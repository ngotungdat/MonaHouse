using Sample.Entities.Auth;
using Sample.Entities.DomainEntities;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services.Auth
{
    public interface IUserGroupService : IDomainService<UserGroups, BaseSearch>
    {
        public Task<UserGroups> GetUserGroupsByPermitObjectId(string Id);
    }
}
