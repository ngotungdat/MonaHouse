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

namespace Sample.Service.Services
{
    public class NotificationService : DomainService<Notification, NotificationSearch>, INotificationService
    {
        protected IAppDbContext coreDbContext;
        protected INotificationUserService notificationUserService;
        protected IUserService userService;
        public NotificationService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext, INotificationUserService notificationUserService, IUserService userService) : base(unitOfWork, mapper)
        {
            this.userService = userService;
            this.coreDbContext = coreDbContext;
            this.notificationUserService = notificationUserService;
        }

        protected override string GetStoreProcName()
        {
            return "Get_Notification";
        }
        public async Task<bool> CreatNotificationAndNotificationUser(Notification item)
        {
            // create notification in DB
             await this.CreateAsync(item);

            // create notificationUser in DB

            if(item!= null)
            {
                string[] ListRoleId = item.RoleId.Split(",");
                // Check role
                int CountRole = ListRoleId.Count();

                
                var ListUser = await userService.GetAllAsync();
                // list user co roll trong notification moi tao
                List<Users> ListUserByRole = new List<Users>();

                // neu co' 1 role
                if (CountRole ==1) {
                    foreach (var user in ListUser)
                    {
                        if (user.RoleNumber == Int32.Parse(ListRoleId[0])) {
                            ListUserByRole.Add(user);
                        }
                    }
                }
                else if ( CountRole == 2 ) {
                    foreach (var user in ListUser)
                    {
                        if (user.RoleNumber == Int32.Parse(ListRoleId[0]) || user.RoleNumber == Int32.Parse(ListRoleId[1]))
                        {
                            ListUserByRole.Add(user);
                        }
                    }
                }
                else if (CountRole == 3) {
                    foreach (var user in ListUser)
                    {
                        if (user.RoleNumber == Int32.Parse(ListRoleId[0]) || user.RoleNumber == Int32.Parse(ListRoleId[1])|| user.RoleNumber == Int32.Parse(ListRoleId[2]))
                        {
                            ListUserByRole.Add(user);
                        }
                    }
                }
                else if (CountRole == 4) {
                    foreach (var user in ListUser)
                    {
                        if (user.RoleNumber == Int32.Parse(ListRoleId[0]) || user.RoleNumber == Int32.Parse(ListRoleId[1])||user.RoleNumber == Int32.Parse(ListRoleId[2]) || user.RoleNumber == Int32.Parse(ListRoleId[3]))
                        {
                            ListUserByRole.Add(user);
                        }
                    }
                }
                // create notificationUser in DB
                List<NotificationUser> ListNewNotificationuser = new List<NotificationUser>();
                foreach(var user in ListUserByRole)
                {
                    NotificationUser notificationUser = new NotificationUser();
                    notificationUser.Id = 0;
                    notificationUser.UsersId = user.Id;
                    notificationUser.Title = item.Title;
                    notificationUser.Content = item.Content;
                    notificationUser.NotificationId = item.Id;
                    notificationUser.TenantId = item.TenantId;
                    notificationUser.Created = item.Created;
                    notificationUser.CreatedBy = item.CreatedBy;
                    notificationUser.Updated = item.Updated;
                    notificationUser.UpdatedBy = item.UpdatedBy;
                    notificationUser.Deleted = item.Deleted;
                    notificationUser.Active = item.Active;
                    notificationUser.isSeen = false;

                    ListNewNotificationuser.Add(notificationUser);
                }
                await notificationUserService.CreateAsync(ListNewNotificationuser);
                return true;
            }
            return false;
        }
    }
}
