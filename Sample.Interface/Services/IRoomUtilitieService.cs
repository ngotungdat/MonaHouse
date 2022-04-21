using Sample.Entities;
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
    public interface IRoomUtilitiService : IDomainService<RoomUtilities, RoomUtilitiSearch>
    {
        Task<bool> UpdateUtilitiesRoom(UpdateUtilitiesRoomRequest itemModel);
        Task<bool> DeleteUtilitiesRoomById(int id);
    }
}
