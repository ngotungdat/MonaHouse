﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sample.Request.Auth;

namespace Sample.Extensions
{
    public sealed class LoginContext
    {
        private static LoginContext instance = null;

        private LoginContext()
        {
        }

        public static LoginContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginContext();
                }
                return instance;
            }
        }

        public UserLoginModel CurrentUser
        {
            get
            {
                var user = (UserLoginModel)Sample.Extensions.HttpContext.Current.Items["User"];
                if (user != null)
                    return user;
                return null;
            }
        }

        public void Clear()
        {
            instance = null;
        }

        public UserLoginModel GetCurrentUser(IHttpContextAccessor httpContext)
        {
            if (httpContext != null && httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var claim = httpContext.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.UserData);
                if (claim != null)
                    return JsonConvert.DeserializeObject<UserLoginModel>(claim.Value);
            }
            return null;
        }
    }

    public class UserLoginModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        //public bool IsCheckOTP { get; set; }
        public IList<Role> Roles { get; set; }
        //public bool IsConfirmOTP { get; set; }
    }
}
