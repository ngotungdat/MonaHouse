using AutoMapper;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.Services.DomainServices;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class RoomImageService : DomainService<RoomImage, BaseSearch>, IRoomImageService
    {
        protected IAppDbContext coreDbContext;
        public RoomImageService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }

        public async Task<List<RoomImage>> GetImageRoomByRoomId(int roomId)
        {
            List<RoomImage> LsRoomImage = (List<RoomImage>) await this.GetAsync(d => d.RoomId == roomId);
            return LsRoomImage;
        }

        protected override string GetStoreProcName()
        {
            return "Get_RoomImage";
        }
        
    }
}
