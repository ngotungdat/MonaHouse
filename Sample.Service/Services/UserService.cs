using Sample.Entities;
using Sample.Extensions;
using Sample.Interface.DbContext;
using Sample.Interface.Services;
using Sample.Interface.UnitOfWork;
using Sample.Utilities;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Sample.Service.Services.DomainServices;
using Sample.Entities.Search;
using Sample.Entities.Auth;
using System.Reflection;

namespace Sample.Service.Services
{
    public class UserService : DomainService<Users, UserSearch>, IUserService
    {
        protected IAppDbContext coreDbContext;
        protected IDebtCollectionService DebtCollectionService;
        public UserService(IAppUnitOfWork unitOfWork
            , IMapper mapper
            , IAppDbContext coreDbContext
            , IDebtCollectionService debtCollection
            ) : base(unitOfWork, mapper)
        {
            this.DebtCollectionService = debtCollection;
        }

        protected override string GetStoreProcName()
        {
            return "User_GetPagingData";
        }

        /// <summary>
        /// Kiểm tra user đã tồn tại chưa?
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<string> GetExistItemMessage(Users item)
        {
            List<string> messages = new List<string>();
            string result = string.Empty;
            bool isExistEmail = !string.IsNullOrEmpty(item.Email) && await Queryable.AnyAsync(x => !x.Deleted && x.Id != item.Id && x.Email == item.Email);
            bool isExistPhone = !string.IsNullOrEmpty(item.Phone) && await Queryable.AnyAsync(x => !x.Deleted && x.Id != item.Id && x.Phone == item.Phone);
            bool isExistUserName = !string.IsNullOrEmpty(item.UserName)
                && await Queryable.AnyAsync(x => !x.Deleted && x.Id != item.Id
                && (x.UserName == item.UserName
                || x.Email == item.UserName
                || x.Phone == item.UserName
                ));
            bool isPhone = ValidateUserName.IsPhoneNumber(item.UserName);
            bool isEmail = ValidateUserName.IsEmail(item.UserName);


            if (isExistEmail)
                messages.Add("Email đã tồn tại!");
            if (isExistPhone)
                messages.Add("Số điện thoại đã tồn tại!");
            if (isExistUserName)
            {
                if (isPhone)
                    messages.Add("Số điện thoại đã tồn tại!");
                else if (isEmail)
                    messages.Add("Email đã tồn tại!");
                else
                    messages.Add("User name đã tồn tại!");
            }
            if (messages.Any())
                result = string.Join(" ", messages);
            return result;
        }

        /// <summary>
        /// Lưu thông tin người dùng
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<bool> CreateAsync(Users item)
        {
            bool result = false;
            if (item != null)
            {
                if (item != null)
                {
                    // Tạo mới nhóm người dùng
                    await this.unitOfWork.Repository<Users>().CreateAsync(item);
                    await this.unitOfWork.SaveAsync();
                    if (item.RoleNumber == 3)
                    {
                        item.TenantId = item.Id;
                        this.unitOfWork.Repository<Users>().Update(item);
                        await this.unitOfWork.SaveAsync();
                    }
                    //Lưu thông tin user thuộc nhóm người dùng
                    if (item.UserGroupId != 0)
                    {
                        UserInGroups userInGroup = new UserInGroups()
                        {
                            UserId = item.Id,
                            UserGroupId = item.UserGroupId,

                            TenantId=item.TenantId,
                            Id = 0
                        };
                        await this.unitOfWork.Repository<UserInGroups>().CreateAsync(userInGroup);
                    }

                    await this.unitOfWork.SaveAsync();
                    //await this.coreDbContext.SaveChangesAsync();

                    //this.coreDbContext.Entry<Users>(item).State = EntityState.Detached;

                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<bool> UpdateAsync(Users item)
        {
            bool result = false;
            var existItem = await this.Queryable.Where(e => e.Id == item.Id).FirstOrDefaultAsync();
            int oldTenant = existItem.TenantId;
            int oldRoleNumber = existItem.RoleNumber;
            if (existItem != null)
            {
                //kiểm tra nếu Properties nào null thì lấy lại data cũ.
                try
                {
                    foreach (PropertyInfo item_old in existItem.GetType().GetProperties())
                    {
                        foreach (PropertyInfo item_new in item.GetType().GetProperties())
                        {
                            if (item_old.Name == item_new.Name)
                            {
                                var value_old = item_old.GetValue(existItem);
                                var value_new = item_new.GetValue(item);
                                if (value_old != value_new)
                                {
                                    item_old.SetValue(existItem, value_new ?? value_old);
                                }
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                //
                if (existItem.TenantId == 0)
                {
                    existItem.TenantId = oldTenant;
                }
                if (existItem.RoleNumber == 0)
                {
                    existItem.RoleNumber = oldRoleNumber;
                }
                //if (!item.IsResetPassword)
                //    item.Password = existItem.Password;
                var currentCreated = existItem.Created;
                var currentCreatedByInfo = existItem.CreatedBy;
                //existItem = mapper.Map<Users>(item);
                existItem.Created = currentCreated;
                existItem.CreatedBy = currentCreatedByInfo;
                //existItem.Password = SecurityUtilities.HashSHA1(item.Password);

                this.unitOfWork.Repository<Users>().Update(existItem);
                await this.unitOfWork.SaveAsync();

                // Cập nhật thông tin user ở nhóm
                //if (item.UserGroupId != 0)
                //{
                //    var existUserInGroup = await this.unitOfWork.Repository<UserInGroups>().GetQueryable()
                //        .Where(e => e.UserGroupId == item.UserGroupId).FirstOrDefaultAsync();
                //    if (existUserInGroup != null)
                //    {
                //        existUserInGroup.UserGroupId = item.UserGroupId;
                //        //existUserInGroup.UserId = item.Id;
                //        existUserInGroup.Updated = DateTime.Now;
                //        this.unitOfWork.Repository<UserInGroups>().Update(existUserInGroup);
                //    }
                //    else
                //    {
                //        UserInGroups userInGroup = new UserInGroups()
                //        {
                //            Created = DateTime.Now,
                //            CreatedBy = item.CreatedBy,
                //            //UserId = item.Id,
                //            UserGroupId = item.UserGroupId,
                //            Active = true,
                //            Deleted = false,
                //        };

                //        userInGroup.Created = DateTime.Now;
                //        //userInGroup.UserId = item.Id;
                //        userInGroup.Id = 0;
                //        await this.unitOfWork.Repository<UserInGroups>().CreateAsync(userInGroup);
                //    }

                //    // Kiểm tra những item không có trong role chọn => Xóa đi
                //    var existGroupOlds = await this.unitOfWork.Repository<UserInGroups>().GetQueryable().Where(e => e.UserGroupId != item.UserGroupId ).ToListAsync();
                //    if (existGroupOlds != null)
                //    {
                //        foreach (var existGroupOld in existGroupOlds)
                //        {
                //            this.unitOfWork.Repository<UserInGroups>().Delete(existGroupOld);
                //        }
                //    }
                //}
                //else
                //{
                //    var userInGroups = await this.unitOfWork.Repository<UserInGroups>().GetQueryable().ToListAsync();
                //    if (userInGroups != null && userInGroups.Any())
                //    {
                //        foreach (var userInGroup in userInGroups)
                //        {
                //            this.unitOfWork.Repository<UserInGroups>().Delete(userInGroup);
                //        }
                //    }
                //}

                await this.unitOfWork.SaveAsync();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Cập nhật password mới cho user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserPassword(int userId, string newPassword)
        {
            bool result = false;

            var existUserInfo = await this.unitOfWork.Repository<Users>().GetQueryable().Where(e => e.Id == userId).FirstOrDefaultAsync();
            if (existUserInfo != null)
            {
                existUserInfo.Password = newPassword;
                existUserInfo.Updated = DateTime.Now;
                Expression<Func<Users, object>>[] includeProperties = new Expression<Func<Users, object>>[]
                {
                    e => e.Password,
                    e => e.Updated
                };
                await this.unitOfWork.Repository<Users>().UpdateFieldsSaveAsync(existUserInfo, includeProperties);
                await this.unitOfWork.SaveAsync();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Kiểm tra user đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Verify(string userName, string password)
        {
            var user = await Queryable
                .Where(e => !e.Deleted
                && (e.UserName == userName
                || e.Phone == userName
                || e.Email == userName
                )
                )
                .FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Status == (int)CoreContants.StatusUser.Locked)
                {
                    throw new Exception("Tài khoản đã bị khóa");
                }
                if (user.Status == (int)CoreContants.StatusUser.NotActive)
                {
                    throw new Exception("Tài khoản chưa được kích hoạt");
                }
                //if (!user.IsAdmin && !user.IsCheckOTP)
                //{
                //    throw new Exception("Người dùng chưa xác thực OTP");
                //}
                if (user.Password == SecurityUtilities.HashSHA1(password))
                {
                    return true;
                }
                else
                    return false;

            }
            else
                return false;
        }

        /// <summary>
        /// Kiểm tra pass word cũ đã giống chưa
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> CheckCurrentUserPassword(int userId, string password, string newPasssword)
        {
            string message = string.Empty;
            List<string> messages = new List<string>();
            bool isCurrentPassword = await this.Queryable.AnyAsync(x => x.Id == userId && x.Password == SecurityUtilities.HashSHA1(password));
            bool isDuplicateNewPassword = await this.Queryable.AnyAsync(x => x.Id == userId && x.Password == SecurityUtilities.HashSHA1(newPasssword));
            if (!isCurrentPassword)
                messages.Add("Mật khẩu cũ không chính xác");
            else if (isDuplicateNewPassword)
                messages.Add("Mật khẩu mới không được trùng mật khẩu cũ"); 
            if (messages.Any())
                message = string.Join("; ", messages);
            return message;
        }

        /// <summary>
        /// Kiểm tra quyền của user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="controller"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<bool> HasPermission(int userId, string controller, IList<string> permissions)
        {
            bool hasPermit = false;

            var userInfo = await unitOfWork.Repository<Users>().GetQueryable().Where(e => e.Id == userId).FirstOrDefaultAsync();
            if (userInfo != null && userInfo.Status == (int)CoreContants.StatusUser.Locked)
                throw new AppException("Tài khoản đã bị khóa!");

            // Lấy ra những nhóm user thuộc
            var userGroupIds = await unitOfWork.Repository<UserInGroups>().GetQueryable()
                .Select(e => e.UserGroupId).ToListAsync();

            var permissionIds = new List<int>();
            var permitObjectIds = new List<int>();

            if (userGroupIds != null && userGroupIds.Any())
            {
                var permitObjectChecks = await unitOfWork.Repository<PermitObjects>().GetQueryable().Where(e => !e.Deleted
                && !string.IsNullOrEmpty(e.ControllerNames)
                && e.ControllerNames.Contains(controller)
                ).ToListAsync();
                permitObjectChecks = permitObjectChecks.Where(e => e.ControllerNames.Split(";", StringSplitOptions.None).Contains(controller)).ToList();
                if (permitObjectChecks != null && permitObjectChecks.Any())
                {
                    var permitObjectCheckIds = permitObjectChecks.Select(e => e.Id).ToList();
                    // Lấy ra những quyền user có trong chức năng cần kiểm tra
                    var permitObjectPermissions = await unitOfWork.Repository<PermitObjectPermissions>().GetQueryable()
                    .Where(e => e.UserGroupId.HasValue
                    && userGroupIds.Contains(e.UserGroupId.Value)
                    && permitObjectCheckIds.Contains(e.PermitObjectId)
                    )
                    .ToListAsync();
                    if (permitObjectPermissions != null && permitObjectPermissions.Any())
                    {
                        permitObjectIds = permitObjectPermissions.Select(e => e.PermitObjectId).Distinct().ToList();

                        foreach (var permitObjectId in permitObjectIds)
                        {
                            // Lấy danh mục mã quyền user cần kiểm tra
                            permissionIds = permitObjectPermissions.Where(e => e.PermitObjectId == permitObjectId).Select(e => e.PermissionId).ToList();
                            var permissionCodes = await unitOfWork.Repository<Permissions>().GetQueryable().Where(e => permissionIds.Contains(e.Id))
                                .Select(e => e.Code)
                                .ToListAsync();

                            // Lấy danh chức năng cần kiểm tra
                            var permitObjectControllers = await unitOfWork.Repository<PermitObjects>().GetQueryable().Where(e => permitObjectIds.Contains(e.Id))
                                .Select(e => e.ControllerNames.Split(";", StringSplitOptions.None))
                                .ToListAsync();

                            // Kiểm tra user có quyền trong chức năng không
                            if (permissionCodes != null && permissionCodes.Any() && permitObjectControllers != null && permitObjectControllers.Any())
                            {
                                hasPermit = permitObjectControllers.Any(x => x.Contains(controller)) && permissions.Any(x => permissionCodes.Contains(x));
                            }
                        }
                    }
                }

            }
            return hasPermit;
        }

        /// <summary>
        /// Kiểm tra user có trong nhóm chỉ định không
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userGroupCode"></param>
        /// <returns></returns>
        public async Task<bool> IsInUserGroup(int userId, string userGroupCode)
        {
            bool result = false;
            var userGroupInfo = await this.unitOfWork.Repository<UserGroups>().GetQueryable()
                .Where(e => !e.Deleted && e.Active && e.Code == userGroupCode).FirstOrDefaultAsync();
            if (userGroupInfo != null)
            {
                result = await this.unitOfWork.Repository<UserInGroups>().GetQueryable()
                    .AnyAsync(e => !e.Deleted && e.Active && e.UserGroupId == userGroupInfo.Id);
            }
            else throw new AppException("Không tìm thấy thông tin nhóm người dùng");


            return result;
        }

        /// <summary>
        /// Cập nhật thông tin user token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="isLogin"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserToken(int userId, string token, bool isLogin = false)
        {
            bool result = false;

            var userInfo = await this.unitOfWork.Repository<Users>().GetQueryable().Where(e => e.Id == userId).FirstOrDefaultAsync();
            //this.medicalDbContext.Entry<Users>(userInfo).State = EntityState.Detached;
            if (userInfo != null)
            {
                if (isLogin)
                {
                    userInfo.Token = token;
                    userInfo.ExpiredDate = DateTime.Now.AddDays(1);
                    //userInfo.ExpiredDate = DateTime.Now.AddMinutes(1);
                }
                else
                {
                    userInfo.Token = string.Empty;
                    userInfo.ExpiredDate = null;
                }
                Expression<Func<Users, object>>[] includeProperties = new Expression<Func<Users, object>>[]
                {
                    e => e.Token,
                    e => e.ExpiredDate
                };
                this.unitOfWork.Repository<Users>().UpdateFieldsSave(userInfo, includeProperties);
                await this.unitOfWork.SaveAsync();
                result = true;
            }

            return result;
        }

        public async Task<Users> GetByToken(string token)
        {
            return await this.unitOfWork.Repository<Users>().GetQueryable().Where(e => e.Token == token).FirstOrDefaultAsync();
        }

        /// <summary>
        /// thu nợ cảu người dùng
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> TakeDebt(int UserId, double debt, string note) {
            var user = this.GetById(LoginContext.Instance.CurrentUser.UserId);
            // tạo mới hóa đơn thu nợ
            Users users = this.GetById(UserId);
            DebtCollection debtCollection = new DebtCollection() ;
            debtCollection.Id = 0;
            debtCollection.UserId = UserId;
            debtCollection.ConllecttionDate = DateTime.Now;
            debtCollection.DebtConllection = debt;
            debtCollection.DebtRemaining = users.DebtMoney - debt;
            debtCollection.Active = true;
            debtCollection.note = note;
            debtCollection.TenantId = user.TenantId;
            debtCollection.CreatedBy = user.UserName;

            var rs = await DebtCollectionService.CreateAsync(debtCollection);
            if (rs== true)
            {
                users.DebtMoney = users.DebtMoney - debt;
            }
            else { throw new Exception("Lỗi trong quá trình xử lý"); }
            return await this.UpdateAsync(users);
        }
    }
}
