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
    public class UserImageService : DomainService<UserImage, BaseSearch>, IUserImageService
    {
        protected IAppDbContext coreDbContext;
        public UserImageService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }

       
        public async Task<List<UserImage>> GetUserImageByUserId(int userId)
        {
            List<UserImage> userImages = (List<UserImage>) await this.GetAsync(d => d.UserId == userId&& d.Deleted==false);
            return userImages;
        }

        public async Task<bool> UpdateUserImages(UpdateUserImageRequest updateUserImageRequest)
        {
            bool ReturnResult = false;
            if (updateUserImageRequest != null)
            {
                var UserID = updateUserImageRequest.UserId;
                var ImagesLinks = updateUserImageRequest.StringAddImageLinks.Split(";");
                var ImageIdDeletes = updateUserImageRequest.StringdeleteImageId.Split(";");
                // thêm ảnh
                if (ImagesLinks!= null&& ImagesLinks[0]!="")
                {
                List<UserImage> RoomImages = new List<UserImage>();
                    foreach(var imageLink in ImagesLinks)
                    {
                        UserImage roomImage = new UserImage();
                        roomImage.Id = 0;
                        roomImage.Link = imageLink;
                        roomImage.UserId = UserID;
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
