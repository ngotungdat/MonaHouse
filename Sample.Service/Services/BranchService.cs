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
        public override async Task<bool> CreateAsync(Branch item)
        {
            using (var dbContextTransaction = coreDbContext.Database.BeginTransaction())
            {
                try
                {
                    if (item != null)
                    {
                        await this.unitOfWork.Repository<Branch>().CreateAsync(item);
                        await this.unitOfWork.SaveAsync();
                        foreach(BranchImage jtem in item.BranchImages)
                        {
                            jtem.BranchId = item.Id;
                            jtem.CreatedBy = item.CreatedBy;
                            await this.unitOfWork.Repository<BranchImage>().CreateAsync(jtem);
                        }
                        await unitOfWork.SaveAsync();
                        await dbContextTransaction.CommitAsync();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }

        public override async Task<bool> UpdateAsync(Branch item)
        {
            using (var dbContextTransaction = coreDbContext.Database.BeginTransaction())
            {
                try
                {
                    var existItem = await this.Queryable.Where(e => e.Id == item.Id).FirstOrDefaultAsync();
                    if (existItem != null)
                    {
                        this.unitOfWork.Repository<Branch>().Update(item);
                        await this.unitOfWork.SaveAsync();
                        //foreach (BranchImage jtem in item.BranchImages)
                        //{
                        //    jtem.BranchId = item.Id;
                        //    jtem.CreatedBy = item.CreatedBy;
                        //    await this.unitOfWork.Repository<BranchImage>().CreateAsync(jtem);
                        //}
                        //await unitOfWork.SaveAsync();
                        await dbContextTransaction.CommitAsync();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
