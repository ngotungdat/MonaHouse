using Sample.Entities;
using Sample.Entities.Search;
using Sample.Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Interface.Services
{
    public interface IUserService : IDomainService<Users, UserSearch>
    {
        Task<bool> Verify(string userName, string password);

        Task<bool> HasPermission(int userId, string controller, IList<string> permissions);
        Task<string> CheckCurrentUserPassword(int userId, string password, string newPasssword);
        Task<bool> UpdateUserToken(int userId, string token, bool isLogin = false);
        Task<bool> UpdateUserPassword(int userId, string newPassword);

        Task<bool> IsInUserGroup(int userId, string userGroupCode);
        Task<Users> GetByToken(string token);
        Task<bool> TakeDebt(int UserId, double debt, string note);
    }
}
