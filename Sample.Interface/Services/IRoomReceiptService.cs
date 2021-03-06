using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Interface.Services.DomainServices;
using Sample.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services
{
    public interface IRoomReceiptService : IDomainService<RoomReceipt, RoomReceiptSearch>
    {
        Task<List<RoomReceiptsForYearAndBranchId>> GetRoomReceiptsWitthBranchAndYear(int branchId, int year, int userId);
    }
}
