using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Interface.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Service.Services.DomainServices;
using Sample.Interface.Services;
using Sample.Interface.DbContext;

namespace Sample.Service.Services
{
    public class UserInRoomService : DomainService<UserInRoom, BaseSearch>, IUserInRoomService
    {
        protected IAppDbContext coreDbContext;
        public UserInRoomService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }
        protected override string GetStoreProcName()
        {
            return "UserInRoom_GetPagingData";
        }
    }
}
