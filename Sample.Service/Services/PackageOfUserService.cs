using AutoMapper;
using Sample.Entities;
using Sample.Entities.Search;
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
    public class PackageOfUserService : DomainService<PackageOfUser, PackageOfUserSearch>, IPackageOfUserService
    {
        protected IAppDbContext coreDbContext;
        public PackageOfUserService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }
        protected override string GetStoreProcName()
        {
            return "Get_PackageOfUser";
        }
    }
}
