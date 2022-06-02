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
    public interface IRoomService : IDomainService<Room, RoomSearch>
    {
        Task<bool> AddNewRoomWithImage(RoomWithImgRequest itemModel);
        Task<double> CheckOutRoomWithMonth(CheckOutRoomRequest request);
        Task<bool> GetOutRoom(int roomId, DateTime dateTime);
        Task<List<RoomReport>> GetReportRoom(int userId);
    }
}
