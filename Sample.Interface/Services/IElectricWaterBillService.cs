using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Interface.Services.DomainServices;
using Sample.Request;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services
{
    public interface IElectricWaterBillService : IDomainService<ElectricWaterBill, ElectricityWaterBillSearch>
    {
        Task<IList<ElectricWaterBill>> GetElectricWaterBillWhenCheckOutWithMonth(GetElectricWaterBillWithMonthRequest request );
        Task<List<ElectricWaterForYearAndBranchId>> GetElectricWaterBillWitthBranchAndYear(int branchId, int year, int userId);
    }
}
