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
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class BranchService : DomainService<Branch, BranchSearch>, IBranchService
    {
        protected IAppDbContext coreDbContext;
        public BranchService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }
        protected override string GetStoreProcName()
        {
            return "Branch_GetPagingData";
        }
        //public override async Task<bool> CreateAsync(Branch item)
        //{
        //    if (item != null)
        //    {
        //        await this.unitOfWork.Repository<Branch>().CreateAsync(item);
        //        await this.unitOfWork.SaveAsync();
        //        return true;
        //    }
        //    return false;
        //}
    }
}
