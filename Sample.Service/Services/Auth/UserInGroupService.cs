using Sample.Interface.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Sample.Interface.Services.Auth;
using Sample.Service.Services.DomainServices;
using Sample.Entities.Auth;
using Sample.Entities.Search;

namespace Sample.Service.Services.Auth
{
    public class UserInGroupService : DomainService<UserInGroups, UserInGroupSearch>, IUserInGroupService
    {
        public UserInGroupService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override string GetStoreProcName()
        {
            return "UserInGroup_GetPagingData";
        }
    }
}
