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
using System.Threading.Tasks;
using Sample.Entities.Search;

namespace Sample.Service.Services
{
    public class UserInRoomService : DomainService<UserInRoom, UserInRoomSearch>, IUserInRoomService
    {
        protected IAppDbContext coreDbContext;
        public UserInRoomService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }

        public async Task<List<UserInRoom>> GetByUserInRoomByRoomId(int id)
        {
            var result=(List<UserInRoom>) await this.GetAsync(d => d.RoomId == id);
            return result;
        }

        protected override string GetStoreProcName()
        {
            return "UserInRoom_GetPagingData";
        }
    }
}
