using AutoMapper;
using Sample.Entities;
using Sample.Entities.Search;
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
    public class FloorService : DomainService<Floor, FloorSearch>, IFloorService
    {
        public FloorService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        protected override string GetStoreProcName()
        {
            return "Floor_GetPagingData";
        }
    }
}
