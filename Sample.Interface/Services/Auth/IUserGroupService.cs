using Sample.Entities.Auth;
using Sample.Entities.Search;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services.Auth
{
    public interface IUserGroupService : ICatalogueService<UserGroups, UserInGroupSearch>
    {
        public Task<UserGroups> GetUserGroupsByPermitObjectId(string Id);
    }
}
