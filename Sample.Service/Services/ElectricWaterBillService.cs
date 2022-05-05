using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Request;
using Sample.Service.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class ElectricWaterBillService : DomainService<ElectricWaterBill, ElectricityWaterBillSearch>, IElectricWaterBillService
    {
        public ElectricWaterBillService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        protected override string GetStoreProcName()
        {
            return "ElectricWaterBill_GetPagingData";
        }
        public async Task<IList<ElectricWaterBill>> GetElectricWaterBillWhenCheckOutWithMonth(GetElectricWaterBillWithMonthRequest request) {
            IList<ElectricWaterBill> result = await this.GetAsync(p => p.RoomId == request.RoomId && p.WriteDate.Value.Month == request.Month && p.WriteDate.Value.Year== request.Year);
            return result;
        }
    }
}
