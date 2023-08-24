using BLL.DTOs;
using BLL.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using Unreal_Journey_Engine.AuthFilters;

namespace Unreal_Journey_Engine.Controllers
{
    public class User_Info_Provider
    {
        public static int Get_User_ID(string authorizationHeader)
        {
            int current_user = -1;
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                current_user = AuthService.IsTokenValid(authorizationHeader);
            }
            return current_user;
        }

        public static string Get_User_Role(string authorizationHeader)
        {
            string current_user_type = null;
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                int user_ID = AuthService.IsTokenValid(authorizationHeader);
                var current_user = UserService.Get(user_ID);
                current_user_type = current_user.Role;
            }
            return current_user_type;
        }

        public static int Get_Tourist_ID(string authorizationHeader)
        {
            int current_tourist_ID= 0;
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                int user_ID = AuthService.IsTokenValid(authorizationHeader);
                var current_user = Tourist_ProfileService.Get(user_ID);
                current_tourist_ID = current_user.Tourist_ID;
            }
            return current_tourist_ID;
        }

        public static int Get_Admin_ID(string authorizationHeader)
        {
            int current_admin_ID = 0;
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                int user_ID = AuthService.IsTokenValid(authorizationHeader);
                var current_user = Admin_ProfileService.Get(user_ID);
                current_admin_ID = current_user.Admin_ID;
            }
            return current_admin_ID;
        }

        public static int Get_tour_guide_ID(string authorizationHeader)
        {
            throw new NotImplementedException();
            int current_admin_ID = 0;
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                int user_ID = AuthService.IsTokenValid(authorizationHeader);
                //var current_user = TourG.Get(user_ID);
                //current_admin_ID = current_user.Admin_ID;
            }
            return current_admin_ID;
        }
    }
}