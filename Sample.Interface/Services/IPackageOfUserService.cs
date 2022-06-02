using Sample.Entities;
using Sample.Entities.Search;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services
{
    public interface IPackageOfUserService : IDomainService<PackageOfUser, PackageOfUserSearch>
    {
        Task<bool> AppceptPackage(PackageOfUser item);
        Task<bool> ExtendPackage(PackageOfUser item);
        Task<List<ReportPackageOfUser>> ReportPackageOfUser(int year, int packageId);
    }
}
