using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Interface.Services.DomainServices;
using Sample.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services
{
    public interface IRoomImageService : IDomainService<RoomImage, BaseSearch>
    {
        Task<List<RoomImage>> GetImageRoomByRoomId(int roomId);
        Task<bool> UpdateImagesRoom(UpdateRoomImageRequest updateRoomImageRequest);
    }
}
