using Sample.Entities;
using Sample.Extensions;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Utilities;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Sample.Service.Services.DomainServices;
using Sample.Entities.Search;
using Sample.Entities.Auth;
using Sample.Entities.DomainEntities;

namespace Sample.Service.Services
{
    public class VehicleFeeConfigService : DomainService<VehicleFeeConfig, BaseSearch>, IVehicleFeeConfigService
    {
        protected IAppDbContext coreDbContext;
        public VehicleFeeConfigService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }

        protected override string GetStoreProcName()
        {
            return "Get_VehicleFeeConfig";
        }
    }
}
