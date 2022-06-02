using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sample.Entities;
using Sample.Entities.Search;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class PackageOfUserService : DomainService<PackageOfUser, PackageOfUserSearch>, IPackageOfUserService
    {
        protected IAppDbContext coreDbContext;
        protected IPackageService packageService;
        public PackageOfUserService(IAppUnitOfWork unitOfWork
            , IMapper mapper
            , IAppDbContext coreDbContext
            , IPackageService packageService
            ) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
            this.packageService = packageService;
        }

        public async Task<bool> AppceptPackage(PackageOfUser item)
        {
            // khi duyet hop dong
            var result = false;
            if (item.Status == 1)
            {

                //danh sach pacckOfUser ma user dang su dung hien tai
                List<PackageOfUser> PackageOfUsers = (List<PackageOfUser>)await this.GetAsync(p => p.UserId == item.UserId && p.Id != item.Id && p.Status == 1);
                
                // nếu người dùng đang kí gói dịc vụ mới khi vẫn còn gói dịch vụ cũ
                if (PackageOfUsers.Count>0) {
                    // TimeSpan DaysLeft = PackageOfUsers[0].ExpireDate.Value.Subtract(DateTime.Now);
                    // số tiền/ 1 ngày theo gói đang sử dụng
                    double MoneyPerDay = (double)(PackageOfUsers[0].PackagePrice / (double)(PackageOfUsers[0].UsedDate));
                    // số ngày còn lại
                    int DaysLeft = PackageOfUsers[0].ExpireDate.Value.Subtract(DateTime.Now).Days;
                    // số tiền dư của user
                    double MoneyOfUser =(double)(DaysLeft) * MoneyPerDay;

                    // lay package user ma admin duyet
                    PackageOfUser PackageOfUser = this.GetById(item.Id);
                    Package package = packageService.GetById(PackageOfUser.PackageId);
                    // vì user dư tiền nên tính thêm vào gói đăng kí mới
                    double MoneyPerDay_new_POU = (double)(package.Price / (double)(package.UserdTime));
                    // tính ra số ngày được thêm vào gói mới
                    int DayAddPOU = (int)(MoneyOfUser / MoneyPerDay_new_POU);
                    // Tiền dư của User
                    double moneyOfUserBack = MoneyOfUser % MoneyPerDay_new_POU;

                    item.TenantId = PackageOfUser.TenantId;
                    item.AcceptDate = DateTime.Now;
                    item.ExpireDate = DateTime.Now.AddDays(package.UserdTime+DayAddPOU);
                    result = await this.UpdateAsync(item);

                    // udpate lai gói cũ
                    PackageOfUsers[0].Status = 2; // User đã sử dụng gói mới nên đóng gói cũ
                    result = await this.UpdateAsync(PackageOfUsers[0]);
                }

                else 
                {
                    // lay package user ma admin duyet
                    PackageOfUser PackageOfUser = this.GetById(item.Id);
                    Package package = packageService.GetById(PackageOfUser.PackageId);
                    item.TenantId = PackageOfUser.TenantId;
                    item.AcceptDate = DateTime.Now;
                    item.ExpireDate = DateTime.Now.AddDays(package.UserdTime);
                    result = await this.UpdateAsync(item);
                }
            }
            else {
                // lay package user ma admin duyet
                PackageOfUser PackageOfUser = this.GetById(item.Id);
                Package package = packageService.GetById(PackageOfUser.PackageId);
                item.TenantId = PackageOfUser.TenantId;
                result = await this.UpdateAsync(item);
            }
            return result;
        }
        protected override string GetStoreProcName()
        {
            return "Get_PackageOfUser";
        }
        public override Task<bool> CreateAsync(PackageOfUser item)
        {
            Package package = packageService.GetById(item.PackageId);
            if (package == null)
            {
                throw new Exception("Lỗi trong quá trình xử lý");
            }
            item.PackageDescription = package.Description;
            item.PackageMoreInfo = package.TitleDescription;
            item.PackageName = package.Title;
            item.PackagePrice = package.Price;
            item.RoomLimited = package.LimitedRoom;
            item.UsedDate = package.UserdTime;

            return base.CreateAsync(item);
        }

        public async Task<bool> ExtendPackage(PackageOfUser item)
        {
            // khi gia han hop dong
            var result = false;
            if (item.Status == 1)
            {
                // duyệt lệnh gia hạn
                // lay package user ma admin duyet
                PackageOfUser PackageOfUser = this.GetById(item.Id);
                item.TenantId = PackageOfUser.TenantId;
                item.AcceptDate = DateTime.Now;
                item.Status = 4;  // 4: gói đã được gia hạn

                // lay package user ma user dang su dung
                List<PackageOfUser> PackageOfUsers = (List<PackageOfUser>)await this.GetAsync(p => p.PackageId == item.PackageId && p.UserId == item.UserId && p.Status == 1);
                if (PackageOfUsers.Count > 0)
                {
                    Package package = packageService.GetById(PackageOfUsers[0].PackageId);
                    PackageOfUsers[0].TenantId = PackageOfUsers[0].TenantId;
                    PackageOfUsers[0].ExpireDate = PackageOfUsers[0].ExpireDate.Value.AddDays(package.UserdTime);
                    result = await this.UpdateAsync(PackageOfUsers[0]);
                    if (result) {
                        result = await this.UpdateAsync(item);
                    }
                }
            }
            else
            {
                // hủy gia hạn gói
                // lay package user ma admin duyet
                PackageOfUser PackageOfUser = this.GetById(item.Id);
                // Package package = packageService.GetById(PackageOfUser.PackageId);
                item.TenantId = PackageOfUser.TenantId;
                result = await this.UpdateAsync(item);
            }
            return result;
        }

        public Task<List<ReportPackageOfUser>> ReportPackageOfUser(int year, int packageId)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("Year", year));
            sqlParameters.Add(new SqlParameter("PackageId", packageId));

            SqlParameter[] parameters = sqlParameters.ToArray();
            return Task.Run(() =>
            {
                List<ReportPackageOfUser> pagedList = new List<ReportPackageOfUser>();
                DataTable dataTable = new DataTable();
                SqlConnection connection = null;
                SqlCommand command = null;
                try
                {
                    connection = (SqlConnection)coreDbContext.Database.GetDbConnection();
                    command = connection.CreateCommand();
                    connection.Open();
                    command.CommandText = "GET_ReportPackageOfUser";
                    command.Parameters.AddRange(parameters);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataTable);
                    pagedList = MappingDataTable.ConvertToList<ReportPackageOfUser>(dataTable);

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
        }
    }
}
