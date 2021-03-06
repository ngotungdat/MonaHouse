using Sample.Entities;
using Sample.Entities.Search;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services
{
    public interface INotificationUserService : IDomainService<NotificationUser, BaseSearch>
    {
        Task<bool> UpdateIsSeenByUser(Users item);
    }
}
