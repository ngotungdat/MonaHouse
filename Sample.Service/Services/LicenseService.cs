using AutoMapper;
using Microsoft.Data.SqlClient;
using Sample.Entities;
using Sample.Entities.DomainEntities;
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
    public class LicenseService : CatalogueService<License, LicenseSearch>, ILicenseService
    {
        public LicenseService(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public override async Task<PagedList<License>> GetPagedListData(LicenseSearch baseSearch)
        {
            PagedList<License> pagedList = new PagedList<License>();
            SqlParameter[] parameters = GetSqlParameters(baseSearch);
            pagedList = await this.unitOfWork.Repository<License>().ExcuteQueryPagingAsync(this.GetStoreProcName(), parameters);
            pagedList.PageIndex = baseSearch.PageIndex;
            pagedList.PageSize = baseSearch.PageSize;
            return pagedList;
        }
        protected override string GetStoreProcName()
        {
            return "License_GetPagingData";
        }
    }
}
