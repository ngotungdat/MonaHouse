using AutoMapper;
using Sample.Entities;
using Sample.Entities.Search;
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
    public class BranchImageService : DomainService<BranchImage, BranchImageSearch>, IBranchImageService
    {
        protected IAppDbContext coreDbContext;
        public BranchImageService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }
        protected override string GetStoreProcName()
        {
            return "BranchImage_GetPagingData";
        }
        
    }
}
