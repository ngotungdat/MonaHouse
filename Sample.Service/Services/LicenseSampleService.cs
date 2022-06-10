using AutoMapper;
using Microsoft.Data.SqlClient;
using Sample.Entities;
using Sample.Entities.Search;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class LicenseSampleService : DomainService<LicenseSample, LicenseSampleSearch>, ILicenseSampleService
    {
        public LicenseSampleService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public override async Task<PagedList<LicenseSample>> GetPagedListData(LicenseSampleSearch baseSearch)
        {
            PagedList<LicenseSample> pagedList = new PagedList<LicenseSample>();
            SqlParameter[] parameters = GetSqlParameters(baseSearch);
            pagedList = await this.unitOfWork.Repository<LicenseSample>().ExcuteQueryPagingAsync(this.GetStoreProcName(), parameters);
            pagedList.PageIndex = baseSearch.PageIndex;
            pagedList.PageSize = baseSearch.PageSize;
            return pagedList;
        }
        protected override string GetStoreProcName()
        {
            return "LicenseSample_GetPagingData";
        }
    }
}
