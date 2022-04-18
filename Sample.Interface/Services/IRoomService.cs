﻿using Sample.Entities;
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
    public interface IRoomService : IDomainService<Room, BaseSearch>
    {
        Task<bool> AddNewRoomWithImage(RoomWithImgRequest itemModel);
    }
}
