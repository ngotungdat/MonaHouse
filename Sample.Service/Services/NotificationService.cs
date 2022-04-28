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
        protected IRoomService roomService;
        protected IUserInRoomService userInRoomService;
        protected IBranchService branchService;
        public NotificationService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext
            , INotificationUserService notificationUserService
            , IUserService userService
            , IRoomService roomService
            , IBranchService branchService
            , IUserInRoomService userInRoomService
            ) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
            this.userService = userService;
            this.roomService = roomService;
            this.branchService = branchService;
            this.userInRoomService = userInRoomService;
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
                if (item.RoleId!=null&&item.BranchId==null&&item.RoomId==null) {
                    #region Notification by role
                    string[] ListRoleId = item.RoleId.Split(",");
                    // Check role
                    int CountRole = ListRoleId.Count();
                    var users = await userService.GetAsync(p => p.TenantId == item.TenantId);
                    // list user co roll trong notification moi tao
                    List<Users> ListUserByRole = new List<Users>();

                    // neu co' 1 role
                    if (CountRole == 1)
                    {
                        foreach (var user in users)
                        {
                            if (user.RoleNumber == Int32.Parse(ListRoleId[0]))
                            {
                                ListUserByRole.Add(user);
                            }
                        }
                    }
                    else if (CountRole == 2)
                    {
                        foreach (var user in users)
                        {
                            if (user.RoleNumber == Int32.Parse(ListRoleId[0]) || user.RoleNumber == Int32.Parse(ListRoleId[1]))
                            {
                                ListUserByRole.Add(user);
                            }
                        }
                    }
                    else if (CountRole == 3)
                    {
                        foreach (var user in users)
                        {
                            if (user.RoleNumber == Int32.Parse(ListRoleId[0]) || user.RoleNumber == Int32.Parse(ListRoleId[1]) || user.RoleNumber == Int32.Parse(ListRoleId[2]))
                            {
                                ListUserByRole.Add(user);
                            }
                        }
                    }
                    else if (CountRole == 4)
                    {
                        foreach (var user in users)
                        {
                            if (user.RoleNumber == Int32.Parse(ListRoleId[0]) || user.RoleNumber == Int32.Parse(ListRoleId[1]) || user.RoleNumber == Int32.Parse(ListRoleId[2]) || user.RoleNumber == Int32.Parse(ListRoleId[3]))
                            {
                                ListUserByRole.Add(user);
                            }
                        }
                    }
                    // create notificationUser in DB
                    List<NotificationUser> ListNewNotificationuser = new List<NotificationUser>();
                    foreach (var user in ListUserByRole)
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
                    #endregion
                }
                if (item.RoleId == null && item.BranchId != null && item.RoomId == null)
                {
                    #region Notification by Branch
                    string[] Branchs = item.BranchId.Split(",");
                    // danh sach branchs
                    // tim tat ca room trong branchs
                    List<Room> roomsInBranch = new List<Room>();
                    foreach(string branchId in Branchs)
                    {
                        var rooms = await roomService.GetAsync(p => p.BranchId == Int32.Parse(branchId));
                        foreach(Room room in rooms)
                        {
                            roomsInBranch.Add(room);
                        }
                    }
                    // sau khi lấy tất cả các room
                    // lấy tất cả user trong room
                    List<Users> usersInBranchs = new List<Users>();
                    foreach (Room room in roomsInBranch) {
                        var usersInRoom = await userInRoomService.GetAsync(p=>p.RoomId==room.Id);
                        foreach(var userInRoom in usersInRoom)
                        {
                            var UsersInBranch = await userService.GetAsync(p => p.Id == userInRoom.UsersId);
                            foreach(var user in UsersInBranch)
                            {
                                usersInBranchs.Add(user);
                            }
                        }
                    }
                    // create notificationUser in DB
                    List<NotificationUser> ListNewNotificationuser = new List<NotificationUser>();
                    foreach (var user in usersInBranchs)
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
                    #endregion
                }
                if (item.RoleId == null && item.BranchId == null && item.RoomId != null)
                {
                    #region Notification by room
                    string[] roomIds = item.RoomId.Split(",");
                    // lay danh sach room
                    List<Users> usersInRooms = new List<Users>();
                    foreach (var roomId in roomIds) {
                        var usersInRoom = await userInRoomService.GetAsync(p => p.RoomId ==Int32.Parse(roomId));
                        foreach (var userInRoom in usersInRoom)
                        {
                            var UsersInBranch = await userService.GetAsync(p => p.Id == userInRoom.UsersId);
                            foreach (var user in UsersInBranch)
                            {
                                usersInRooms.Add(user);
                            }
                        }
                    }
                    // create notificationUser in DB
                    List<NotificationUser> ListNewNotificationuser = new List<NotificationUser>();
                    foreach (var user in usersInRooms)
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
                    #endregion
                }
                return false;
            }
            return false;
        }
    }
}
