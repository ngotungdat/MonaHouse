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
    public class NotificationUserService : DomainService<NotificationUser, BaseSearch>, INotificationUserService
    {
        protected IAppDbContext coreDbContext;
        protected IUserService userService;
        public NotificationUserService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext, IUserService userService) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
            this.userService = userService;

        }

        protected override string GetStoreProcName()
        {
            return "Get_NotificationUser";
        }
        public async Task<bool> UpdateIsSeenByUser(Users item)
        {
            if (item != null)
            {
                // zo notificationUser loc ra list co user t
                List<NotificationUser> listNotificationUserGetByUser = new List<NotificationUser>();
                var listNotificationUser = await this.GetAllAsync();
                foreach (NotificationUser notificationUser in listNotificationUser)
                {
                    if (notificationUser.UsersId == item.Id)
                    {
                        notificationUser.isSeen = true;
                        listNotificationUserGetByUser.Add(notificationUser);
                    }
                }
                // update isSeen
                var result = await this.UpdateAsync(listNotificationUserGetByUser);
                if (result == true) return true;
                return false;
            }
            return false;
        }
    }
}
