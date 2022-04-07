using Sample.Entities.DomainEntities;
using Sample.Interface.UnitOfWork;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sample.Service.Services.DomainServices;
using Sample.Interface.Services.Auth;
using Sample.Entities.Auth;
using System.Threading.Tasks;
using Sample.Models;
using Sample.Request.Auth;
using System.Data.Entity;
using System.Linq;

namespace Sample.Service.Services.Auth
{
    public class PermitObjectPermissionService : DomainService<PermitObjectPermissions, BaseSearch>, IPermitObjectPermissionService
    {
        IPermitObjectService permitObjectService;
        IPermissionService permissionService;
        IUserGroupService userGroupService;
        public PermitObjectPermissionService(IAppUnitOfWork unitOfWork, IMapper mapper, IPermissionService PermissionService, IPermitObjectService PermitObjectService, IUserGroupService UserGroupService) : base(unitOfWork, mapper)
        {
            permissionService = PermissionService;
            permitObjectService = PermitObjectService;
            userGroupService = UserGroupService;
        }

        public async Task<bool> AddNewPermitObjectPermissions(PermitObjectPermissionAddNewRequest permitObjectPermissionRequest)
        {
            if (permitObjectPermissionRequest != null)
            {
                var listPermissionId = permitObjectPermissionRequest.PermissionId.Split(";");
                List<PermitObjectPermissions> ListpermitObjectPermissionRequests = new List<PermitObjectPermissions>();
                foreach(var d in listPermissionId)
                {
                    PermitObjectPermissions permitObjectPermission = new PermitObjectPermissions();
                    permitObjectPermission.Id = permitObjectPermissionRequest.Id;
                    permitObjectPermission.Deleted = permitObjectPermissionRequest.Deleted;
                    permitObjectPermission.Active = permitObjectPermissionRequest.Active;
                    permitObjectPermission.PermissionId = Int32.Parse(d);
                    permitObjectPermission.UserGroupId = Int32.Parse( permitObjectPermissionRequest.UserGroupId);
                    permitObjectPermission.PermitObjectId = Int32.Parse(permitObjectPermissionRequest.PermitObjectId);
                    ListpermitObjectPermissionRequests.Add(permitObjectPermission);
                }
                if (ListpermitObjectPermissionRequests!= null) {
                    var rs= await this.CreateAsync(ListpermitObjectPermissionRequests);
                    if (rs)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }


        public async Task<IList<PermitObjectPermissions>> GetPermitObjectPermissionsByPermitObjectId(int item)
        {
            if (item != null)
            {
                IList<PermitObjectPermissions> permitObjectPermissions = new List<PermitObjectPermissions>();
                IList<PermitObjectPermissions> permitObjectPermissionsRS = new List<PermitObjectPermissions>();
                permitObjectPermissions = await this.GetAllAsync();
                foreach (var d in permitObjectPermissions) {
                    if(d.PermitObjectId == item)
                    {
                        permitObjectPermissionsRS.Add(d);
                    }
                }
                if (permitObjectPermissionsRS!= null)
                {
                    return permitObjectPermissionsRS;
                }
                throw new Exception("Lỗi trong quá trình xử lý");
            }
            throw new Exception("Lỗi trong quá trình xử lý");
        }

        public async Task<bool> UpdatePermitObjectPermissions(PermitObjectPermissionAddNewRequest permitObjectPermissionAddNewRequest)
        {
            bool ReturnResult = false;
            if (permitObjectPermissionAddNewRequest != null)
            {
                var LsPermissionId= permitObjectPermissionAddNewRequest.PermissionId.Split(";");
                int UserGroupId =  Int32.Parse(permitObjectPermissionAddNewRequest.UserGroupId);
                int PermitObjectId = Int32.Parse(permitObjectPermissionAddNewRequest.PermitObjectId);

                var LsAllPermitObjectPermission = await this.GetAllAsync();
                IList<PermitObjectPermissions> LsPermitObjectPermissionHasFilter = new List<PermitObjectPermissions>();
                foreach (var d in LsAllPermitObjectPermission) {
                    if(d.UserGroupId== UserGroupId && d.PermitObjectId== PermitObjectId)
                    {
                        LsPermitObjectPermissionHasFilter.Add(d);
                    }
                }
                // addnew dk: khong tim thay permission trong db
                foreach (var z in LsPermissionId)
                {
                    bool tmp = false;
                    {
                    foreach(var d in LsPermitObjectPermissionHasFilter)
                        if (d.PermissionId == Int32.Parse(z))
                        {
                            tmp = true;
                            break;
                        }
                        else
                        {
                            tmp = false;
                        }
                    }
                    if (tmp == false) {
                        PermitObjectPermissions permitObjectPermissions = new PermitObjectPermissions();
                        permitObjectPermissions.Id = 0;
                        permitObjectPermissions.PermissionId = Int32.Parse(z);
                        permitObjectPermissions.PermitObjectId = PermitObjectId;
                        permitObjectPermissions.UserGroupId = UserGroupId;
                        permitObjectPermissions.Active = true;
                        permitObjectPermissions.Deleted = false;
                        
                        var result= await this.CreateAsync(permitObjectPermissions);
                        if (!result)
                        {
                            throw new Exception("Lỗi trong quá trình xử lý");
                        }
                        ReturnResult = true;
                    }
                }

                // delete dk: so sanh tu db vs rq
                foreach(var d in LsPermitObjectPermissionHasFilter)
                {
                    bool tmp = false;
                    foreach (var z in LsPermissionId) {
                        if(d.PermissionId== Int32.Parse(z))
                        {
                            tmp = true;
                            break;
                        }
                        else
                        {
                            tmp = false;
                        }
                    }
                    if(tmp == false)
                    {
                        var result = await this.DeletePermitObjectPermissionsById(d.Id);
                        if (!result)
                        {
                            throw new Exception("Lỗi trong quá trình xử lý");
                        }
                        ReturnResult = true;

                    }
                }
                return ReturnResult;
            }
            return ReturnResult;


        }

        protected override string GetStoreProcName()
        {
            return "PermitObjectPermissions_GetPagingData";
        }
        
        public async Task<bool> DeletePermitObjectPermissionsById(int id)
        {
            var exists = Queryable
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
            if (exists != null)
            {
                exists.Deleted = true;
                unitOfWork.Repository<PermitObjectPermissions>().Delete(exists);
                await unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                throw new Exception(id + " not exists");
            }
        }
    }
}
