using AutoMapper;
using Sample.Entities;
using Sample.Entities.Search;
using Sample.Extensions;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Request;
using Sample.Service.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service.Services
{
    public class RoomUtilitiesService : DomainService<RoomUtilities, RoomUtilitiSearch>, IRoomUtilitiService
    {
        protected IAppDbContext coreDbContext;
        protected IUserService userService;
        public RoomUtilitiesService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext, IUserService _userService) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
            userService = _userService;

        }

        public async Task<bool> UpdateUtilitiesRoom(UpdateUtilitiesRoomRequest itemModel)
        {
            bool ReturnResult = false;
            if (itemModel != null)
            {
                var LsRoomUtilities = itemModel.ListUtilitiesAndPrice.Split(";");
                var LsAllRoomUtilities = await this.GetAsync(p => p.RoomId == itemModel.RoomId);
                IList<RoomUtilities> LsRoomUtilitiesHasFilter = new List<RoomUtilities>();
                if (LsRoomUtilities.Length > 0&& LsRoomUtilities[0]!="") {
                    foreach (var p in LsRoomUtilities)
                    {
                        bool tmp = false;
                        foreach (var d in LsAllRoomUtilities)
                        {
                            if (d.UtilitiesId == Int32.Parse(p.Split(':')[0]) && d.Price == double.Parse(p.Split(':')[1]))
                            {
                                tmp = true;
                                break;
                            }
                            else
                            {
                                tmp = false;
                            }
                        }
                        if (tmp == false)
                        {
                            var user = userService.GetById(LoginContext.Instance.CurrentUser.UserId);

                            RoomUtilities roomUtilities = new RoomUtilities();
                            roomUtilities.Id = 0;
                            roomUtilities.UtilitiesId = Int32.Parse(p.Split(':')[0]); ;
                            roomUtilities.Price = double.Parse(p.Split(':')[1]); ;
                            roomUtilities.RoomId = itemModel.RoomId;
                            roomUtilities.Active = true;
                            roomUtilities.Deleted = false;
                            roomUtilities.TenantId = user.Id;
                            roomUtilities.CreatedBy = user.UserName;
                            var result = await this.CreateAsync(roomUtilities);
                            if (!result)
                            {
                                throw new Exception("Lỗi trong quá trình xử lý");
                            }
                            ReturnResult = true;
                        }
                    }
                }
                // delete dk: so sanh tu db vs rq
                foreach (var p in LsAllRoomUtilities)
                {
                    bool tmp = false;
                    if(LsRoomUtilities.Length > 0 && LsRoomUtilities[0] != "")
                    {
                        foreach (var d in LsRoomUtilities)
                        {
                            if (p.UtilitiesId == Int32.Parse(d.Split(':')[0]) && p.Price == double.Parse(d.Split(':')[1]))
                            {
                                tmp = true;
                                break;
                            }
                            else
                            {
                                tmp = false;
                            }
                        }
                    }
                    if (tmp == false)
                    {
                        var result = await this.DeleteUtilitiesRoomById(p.Id);
                        if (!result)
                        {
                            throw new Exception("Lỗi trong quá trình xử lý");
                        }
                        ReturnResult = true;

                    }
                }
                // form khong them sua xoa' 
                ReturnResult = true;
                return ReturnResult;
            }
            return ReturnResult;
        }
        public async Task<bool> DeleteUtilitiesRoomById(int id)
        {
            var exists = Queryable
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
            if (exists != null)
            {
                exists.Deleted = true;
                unitOfWork.Repository<RoomUtilities>().Delete(exists);
                await unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                throw new Exception(id + " not exists");
            }
        }

        protected override string GetStoreProcName()
        {
            return "RoomUtilities_GetPagingData";
        }
    }
}
