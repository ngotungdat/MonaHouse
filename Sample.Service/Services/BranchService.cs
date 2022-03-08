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

                    }

                    //foreach (var data in listData)
                    //{
                    //    await this.CreateAsync(data);
                    //    foreach (var item in data.Orders)
                    //    {
                    //        item.MainOrderId = data.Id;
                    //    }
                    //    data.StaffIncomes.ForEach(e => e.MainOrderId = data.Id);

                    //    await unitOfWork.Repository<Order>().CreateAsync(data.Orders);

                    //    await unitOfWork.Repository<StaffIncome>().CreateAsync(data.StaffIncomes);
                    //    await unitOfWork.SaveAsync();

                    //    if (data.ShopTempId > 0)
                    //        //Xóa Shop temp và order temp
                    //        await orderShopTempService.DeleteAsync(data.ShopTempId);
                    //}

                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
            return true;
        }
    }
}
