using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using Unreal_Journey_Engine.AuthFilters;

namespace Unreal_Journey_Engine.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {

        #region Get All Users
        [HttpGet]
        [Route("all")]
        [Logged]
        public HttpResponseMessage Get_All_Users()
        {

            var data = UserService.Get();
            if (data.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                var responseMessage = new
                {
                    Message = "No data available"
                };
                return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
            }

        }
        #endregion Get All Users

        #region Get Single User
        [HttpGet]
        [Route("{id}")]
        [Logged]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = UserService.Get(id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Account Not Found"
                    };
                    return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Get Single User

        #region Signup / Create
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create_User(UserDTO dto)
        {
            try
            {

                if (dto != null)
                {
                    var decision = UserService.Create(dto);
                    if (decision)
                    {
                        var responseMessage = new
                        {
                            Message = "Account Created"
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Failed to Create Account"
                        };
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                    }

                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Provide User Data to Create Account"
                    };
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Signup / Create

        #region Update
        [HttpPut]
        [Route("update")]
        [Logged]
        public HttpResponseMessage Update_User_Info(UserDTO dto)
        {
            try
            {

                if (dto != null)
                {
                    var decision = UserService.Update(dto);
                    if (decision)
                    {
                        var responseMessage = new
                        {
                            Message = "Account Update"
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Failed to Update Account"
                        };
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                    }

                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Provide User Data to Create Account"
                    };
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Update

        #region Delete
        [HttpDelete]
        [Route("delete/{id}")]
        [Logged]
        public HttpResponseMessage Delete_User_Info(int id)
        {
            try
            {
                var data = UserService.Delete(id);
                if (data)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Account Not Found"
                    };
                    return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Delete




        #region Login

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(LoginDTO login)
        {
            try
            {
                var token = AuthService.Login(login.Email, login.Password);
                if (token != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, token);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Msg = "Username or password invalid" });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        #endregion Login

        #region Logout

        [HttpGet]
        [Route("logout")]
        [Logged]
        public HttpResponseMessage Logout(LoginDTO login)
        {
            try
            {
                var current_user = 0;
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                current_user = User_Info_Provider.Get_User_ID(authorizationHeader);

                var token = AuthService.Logout(authorizationHeader);
                if (token != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, token);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Logout Failed" });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        #endregion Logout

        #region View Own Profile

        [HttpPost]
        [Route("profile")]
        [Logged]
        public HttpResponseMessage ViewProfile()
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_ID = User_Info_Provider.Get_User_ID(authorizationHeader);
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);

                if (current_user_Type == "Tourist")
                {
                    var tourist_info = Tourist_ProfileService.Get(current_user_ID);
                    return Request.CreateResponse(HttpStatusCode.OK, tourist_info);
                }
                else if(current_user_Type == "Admin")
                {
                    var admin_info = Admin_ProfileService.Get(current_user_ID);
                    return Request.CreateResponse(HttpStatusCode.OK, admin_info);
                }
                else if (current_user_Type == "Tour_Guide")
                {
                    var tour_guide_info = Admin_ProfileService.Get(current_user_ID);
                    return Request.CreateResponse(HttpStatusCode.OK, tour_guide_info);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Profile Not Found !" });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion View Own Profile





    }
}
