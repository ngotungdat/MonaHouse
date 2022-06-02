using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Extensions;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Request;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class ElectricWaterBillService : DomainService<ElectricWaterBill, ElectricityWaterBillSearch>, IElectricWaterBillService
    {
        protected readonly IAppDbContext Context;

        public ElectricWaterBillService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext Context) : base(unitOfWork, mapper)
        {
            this.Context = Context;
        }
        protected override string GetStoreProcName()
        {
            return "ElectricWaterBill_GetPagingData";
        }
        public async Task<IList<ElectricWaterBill>> GetElectricWaterBillWhenCheckOutWithMonth(GetElectricWaterBillWithMonthRequest request) {
            IList<ElectricWaterBill> result = await this.GetAsync(p => p.RoomId == request.RoomId && p.WriteDate.Value.Month == request.Month && p.WriteDate.Value.Year== request.Year);
            return result;
        }

        public Task<List<ElectricWaterForYearAndBranchId>> GetElectricWaterBillWitthBranchAndYear(int branchId, int year, int userId)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("BranchId", branchId));
            sqlParameters.Add(new SqlParameter("Year", year));
            sqlParameters.Add(new SqlParameter("TenantId", userId));

            SqlParameter[] parameters = sqlParameters.ToArray();
            return Task.Run(() =>
            {
                List<ElectricWaterForYearAndBranchId> pagedList = new List<ElectricWaterForYearAndBranchId>();
                DataTable dataTable = new DataTable();
                SqlConnection connection = null;
                SqlCommand command = null;
                try
                {
                    connection = (SqlConnection)Context.Database.GetDbConnection();
                    command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = "Get_ElectricWaterWithYearAndBranchId";
                    command.Parameters.AddRange(parameters);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);
                    pagedList = MappingDataTable.ConvertToList<ElectricWaterForYearAndBranchId>(dataTable);
                   
                    return pagedList;
                }
                finally
                {
                    if (connection != null && connection.State == System.Data.ConnectionState.Open)
                        connection.Close();

                    if (command != null)
                        command.Dispose();
                }
            });
            //List<ElectricWaterForYearAndBranchId> rs = (List<ElectricWaterForYearAndBranchId>)await this.unitOfWork.Repository<ElectricWaterForYearAndBranchId>().ExcuteStoreAsync("Get_ElectricWaterWithYearAndBranchId", parameters);
            
           // return rs;
           
        }
    }
}
