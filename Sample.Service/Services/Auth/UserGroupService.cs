using AutoMapper;
using Sample.Service.Services.DomainServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Interface.UnitOfWork;
using Sample.Interface.Services.Auth;
using Sample.Entities.Auth;
using Sample.Entities.Search;

namespace Sample.Service.Services.Auth
{
    public class UserGroupService : CatalogueService<UserGroups, UserInGroupSearch>, IUserGroupService
    {
        IPermitObjectService permitObjectService;
        public UserGroupService(IAppUnitOfWork unitOfWork
            , IMapper mapper
            , IPermitObjectService PermitObjectService
            ) : base(unitOfWork, mapper)
        {
            permitObjectService = PermitObjectService;
        }

        /// <summary>
        /// Tạo nhóm người dùng
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<bool> CreateAsync(UserGroups item)
        {
            bool result = false;
            if (item != null)
            {
                // Tạo mới nhóm người dùng
                await this.unitOfWork.Repository<UserGroups>().CreateAsync(item);
                await this.unitOfWork.SaveAsync();

                // Lưu thông tin user thuộc nhóm người dùng
                if (item.UserIds != null && item.UserIds.Any())
                {
                    foreach (var userId in item.UserIds)
                    {
                        UserInGroups userInGroup = new UserInGroups()
                        {
                            CreatedBy = item.CreatedBy,
                            //UserId = userId,
                            Created = DateTime.Now,
                            UserGroupId = item.Id,
                            Active = true,
                            Deleted = false,
                            Id = 0
                        };
                        await this.unitOfWork.Repository<UserInGroups>().CreateAsync(userInGroup);
                    }
                }
                // Lưu thông tin chức năng + quyền tương ứng
                if (item.PermitObjectPermissions != null && item.PermitObjectPermissions.Any())
                {
                    foreach (var permitObjectPermission in item.PermitObjectPermissions)
                    {
                        permitObjectPermission.Created = DateTime.Now;
                        permitObjectPermission.UserGroupId = item.Id;
                        permitObjectPermission.Active = true;
                        permitObjectPermission.Id = 0;
                        await this.unitOfWork.Repository<PermitObjectPermissions>().CreateAsync(permitObjectPermission);
                    }
                }
                await this.unitOfWork.SaveAsync();


                result = true;
            }

            return result;
        }

        /// <summary>
        /// Cập nhật thông tin nhóm người dùng
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<bool> UpdateAsync(UserGroups item)
        {
            bool result = false;
            var existItem = await this.Queryable.Where(e => e.Id == item.Id).FirstOrDefaultAsync();
            if (existItem != null)
            {
                var currentCreatedDate = existItem.Created;
                var currentCreatedBy = existItem.CreatedBy;
                existItem = mapper.Map<UserGroups>(item);
                existItem.Created = currentCreatedDate;
                existItem.CreatedBy = currentCreatedBy;
                this.unitOfWork.Repository<UserGroups>().Update(existItem);
                await this.unitOfWork.SaveAsync();

                // Cập nhật thông tin user ở nhóm
                if (item.UserIds != null && item.UserIds.Any())
                {
                    foreach (var userId in item.UserIds)
                    {
                        var existUserInGroup = await this.unitOfWork.Repository<UserInGroups>().GetQueryable()
                            .Where(e => e.UserGroupId == existItem.Id).FirstOrDefaultAsync();
                        if (existUserInGroup != null)
                        {
                            //existUserInGroup.UserId = userId;
                            existUserInGroup.UserGroupId = item.Id;
                            existUserInGroup.Updated = DateTime.Now;
                            this.unitOfWork.Repository<UserInGroups>().Update(existUserInGroup);
                        }
                        else
                        {
                            UserInGroups userInGroup = new UserInGroups()
                            {
                                CreatedBy = item.CreatedBy,
                                //UserId = userId,
                                Created = DateTime.Now,
                                UserGroupId = existItem.Id,
                                Id = 0
                            };
                            await this.unitOfWork.Repository<UserInGroups>().CreateAsync(userInGroup);

                        }
                    }

                    // Kiểm tra những item không có trong role chọn => Xóa đi
                    var existGroupOlds = await this.unitOfWork.Repository<UserInGroups>().GetQueryable().Where(e => e.UserGroupId == existItem.Id).ToListAsync();
                    if (existGroupOlds != null)
                    {
                        foreach (var existGroupOld in existGroupOlds)
                        {
                            this.unitOfWork.Repository<UserInGroups>().Delete(existGroupOld);
                        }
                    }

                }
                else
                {
                    var existUserInGroups = await this.unitOfWork.Repository<UserInGroups>().GetQueryable()
                        .Where(e => !e.Deleted && e.UserGroupId == existItem.Id).ToListAsync();
                    if (existUserInGroups != null && existUserInGroups.Any())
                    {
                        foreach (var existUserInGroup in existUserInGroups)
                        {
                            this.unitOfWork.Repository<UserInGroups>().Delete(existUserInGroup);
                        }
                    }
                }

                // Cập nhật thông tin quyền với chứng năng tương ứng của nhóm
                if (item.PermitObjectPermissions != null && item.PermitObjectPermissions.Any())
                {
                    foreach (var permitObjectPermission in item.PermitObjectPermissions)
                    {
                        var existPermitObjectPermission = await this.unitOfWork.Repository<PermitObjectPermissions>().GetQueryable()
                            .Where(e => !e.Deleted && e.Active
                            && e.PermissionId == permitObjectPermission.PermissionId
                            && e.PermitObjectId == permitObjectPermission.PermitObjectId
                            && e.UserGroupId == existItem.Id
                            ).FirstOrDefaultAsync();
                        if (existPermitObjectPermission != null)
                        {
                            var currentCreated = existItem.Created;
                            var currentCreatedByInfo = existItem.CreatedBy;
                            existPermitObjectPermission = mapper.Map<PermitObjectPermissions>(permitObjectPermission);
                            existPermitObjectPermission.Created = currentCreated;
                            existPermitObjectPermission.CreatedBy = currentCreatedByInfo;
                            existPermitObjectPermission.UserGroupId = item.Id;
                            existPermitObjectPermission.Updated = DateTime.Now;
                            this.unitOfWork.Repository<PermitObjectPermissions>().Update(existPermitObjectPermission);
                        }
                        else
                        {
                            permitObjectPermission.Created = DateTime.Now;
                            permitObjectPermission.UserGroupId = item.Id;
                            permitObjectPermission.Active = true;
                            await this.unitOfWork.Repository<PermitObjectPermissions>().CreateAsync(permitObjectPermission);

                        }
                    }
                }
                await this.unitOfWork.SaveAsync();
                result = true;
            }

            return result;
        }

        public override async Task<string> GetExistItemMessage(UserGroups item)
        {
            string result = string.Empty;
            bool isExistCode = await Queryable.AnyAsync(x => !x.Deleted && x.Id != item.Id && x.Code == item.Code);
            if (item.Id > 0 && item.PermitObjectPermissions != null && item.PermitObjectPermissions.Any())
            {
                foreach (var permitObject in item.PermitObjectPermissions)
                {
                    if (permitObject.Deleted) continue;
                    bool isExistPermitObjectPermission = await this.unitOfWork.Repository<PermitObjectPermissions>().GetQueryable().AnyAsync(e => !e.Deleted && e.Active
                    && (permitObject.Id > 0 && e.Id != permitObject.Id)
                    && e.UserGroupId == item.Id
                    && e.PermissionId == permitObject.PermissionId
                    && e.PermitObjectId == permitObject.PermitObjectId
                    );
                    if (isExistPermitObjectPermission)
                        return "Quyền đã tồn tại với chức năng này!";
                }
            }

            if (isExistCode)
                return "Mã đã tồn tại!";
            return result;
        }

        public async Task<UserGroups> GetUserGroupsByPermitObjectId(string Id)
        {
            IList<UserGroups> ListUserGroups = await this.GetAllAsync();
            PermitObjects permitObjects = permitObjectService.GetById(Int32.Parse(Id));
            if(permitObjects != null)
            {
                foreach(var d in ListUserGroups)
                {
                    if (d.Code == permitObjects.Code)
                    {
                        return d;
                    }
                }
            }
            throw new Exception("Lỗi trong quá trình xử lý");
        }
    }
}
