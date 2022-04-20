using AutoMapper;
using Sample.Entities;
using Sample.Entities.DomainEntities;
using Sample.Entities.Search;
using Sample.Extensions;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.Services.DomainServices;
using Sample.Interface.UnitOfWork;
using Sample.Request;
using Sample.Service.Services.DomainServices;
using Sample.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class RoomService : DomainService<Room, RoomSearch>, IRoomService
    {
        protected IAppDbContext coreDbContext;
        protected IUserService userService;
        protected IRoomImageService roomImageService;
        public RoomService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext
            ,IUserService UserService
            ,IRoomImageService RoomImageService) : base(unitOfWork, mapper)
        {
            this.userService = UserService;
            this.roomImageService = RoomImageService;
            this.coreDbContext = coreDbContext;
        }

        public async Task<bool> AddNewRoomWithImage(RoomWithImgRequest itemModel)
        {
            bool returnResult = false;
            var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);
            // phòng mới
            Room room = new Room();
            room.Id = itemModel.Id;
            room.Name = itemModel.Name;
            room.Price = itemModel.Price;
            room.Status = itemModel.Status;
            room.TenantId = user.TenantId;
            room.CreatedBy = user.UserName;
            room.Created = DateTime.Now;
            room.Acreage = itemModel.Acreage;
            room.BedAmount = itemModel.BedAmount;
            room.BranchFloor = itemModel.BranchFloor;
            room.BranchId = itemModel.BranchId;
            room.Deposit = itemModel.Deposit;
            room.Deleted = itemModel.Deleted;
            room.Active = itemModel.Active;
            room.RoomTypeId = itemModel.RoomTypeId;
            room.FloorId = itemModel.FloorId;
            //
            //var result = await this.CreateAsync(room);
            await this.unitOfWork.Repository<Room>().CreateAsync(room);
            await this.unitOfWork.SaveAsync();
            if (room!=null)
            {
                returnResult = true;
                // hình ảnh phòng mới
                var ListLinkImage = itemModel.Images.Split(";");
                List<RoomImage> LsRoomImage = new List<RoomImage>();
                if (ListLinkImage.Length > 0)
                {
                    foreach (var d in ListLinkImage)
                    {
                        RoomImage roomImage = new RoomImage();
                        roomImage.RoomId = room.Id;
                        roomImage.Link = d;
                        LsRoomImage.Add(roomImage);
                    }
                }
                var rs = await roomImageService.CreateAsync(LsRoomImage);
                if (rs)
                {
                    returnResult = true;
                }
                else
                {
                    returnResult = false;
                }
            }
            else
            {
                returnResult = false;
            }

            
            return returnResult;
        }

        protected override string GetStoreProcName()
        {
            return "Get_Room";
        }
       
    }
}
