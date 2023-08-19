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
    }
}