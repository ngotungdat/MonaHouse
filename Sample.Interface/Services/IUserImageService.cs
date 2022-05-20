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
    public interface IUserImageService : IDomainService<UserImage, BaseSearch>
    {
        Task<List<UserImage>> GetUserImageByUserId(int userId);
        Task<bool> UpdateUserImages(UpdateUserImageRequest updateUserImageRequest);
    }
}
