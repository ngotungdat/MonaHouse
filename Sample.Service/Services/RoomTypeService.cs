using AutoMapper;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class RoomTypeService : DomainService<RoomType, BaseSearch>, IRoomTypeService
    {
        protected IAppDbContext coreDbContext;

        public RoomTypeService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }
        protected override string GetStoreProcName()
        {
            return "Get_RoomTypes";
        }
    }
}
