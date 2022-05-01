using AutoMapper;
using Sample.Entities;
using Sample.Entities.DomainEntities;
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
    public class RoomImageService : DomainService<RoomImage, BaseSearch>, IRoomImageService
    {
        protected IAppDbContext coreDbContext;
        public RoomImageService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }

        public async Task<List<RoomImage>> GetImageRoomByRoomId(int roomId)
        {
            List<RoomImage> LsRoomImage = (List<RoomImage>) await this.GetAsync(d => d.RoomId == roomId&& d.Deleted==false);
            return LsRoomImage;
        }

        public async Task<bool> UpdateImagesRoom(UpdateRoomImageRequest updateRoomImageRequest)
        {
            bool ReturnResult = false;
            if (updateRoomImageRequest != null)
            {
                var RoomID = updateRoomImageRequest.RoomId;
                var ImagesLinks = updateRoomImageRequest.StringAddImageLinks.Split(";");
                var ImageIdDeletes = updateRoomImageRequest.StringdeleteImageId.Split(";");
                // thêm ảnh
                if (ImagesLinks!= null&& ImagesLinks[0]!="")
                {
                List<RoomImage> RoomImages = new List<RoomImage>();
                    foreach(var imageLink in ImagesLinks)
                    {
                        RoomImage roomImage = new RoomImage();
                        roomImage.Id = 0;
                        roomImage.Link = imageLink;
                        roomImage.RoomId = RoomID;
                        roomImage.Active = true;
                        roomImage.Deleted = false;
                        // thêm vào danh sách thêm mới
                        RoomImages.Add(roomImage);
                    }
                    ReturnResult= await this.CreateAsync(RoomImages);
                }
                else
                {
                    ReturnResult = true;
                }
                if (ImageIdDeletes != null && ImageIdDeletes[0] != "")
                {
                    foreach (var imageId in ImageIdDeletes)
                    {
                        // xóa ảnh theo danh sách ImageId
                        ReturnResult = await this.DeleteAsync(Int32.Parse(imageId));
                    }
                }
                else
                {
                    ReturnResult = true;
                }
            }
            return ReturnResult;
        }

        protected override string GetStoreProcName()
        {
            return "Get_RoomImage";
        }
        
    }
}
