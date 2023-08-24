using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Unreal_Journey_Engine.AuthFilters;

namespace Unreal_Journey_Engine.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/tour")]
    public class Tour_PackageController : ApiController
    {

        #region Get All Tours
        [HttpGet]
        [Route("all")]
        [Logged]
        public HttpResponseMessage Get_All_Tours()
        {

            var data = Tour_PackageService.Get();
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
        #endregion Get All Tours

        #region Get Single Tour
        [HttpGet]
        [Route("{id}")]
        [Logged]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = Tour_PackageService.Get(id);
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
        #endregion Get Single Tour

        #region  Create
        [HttpPost]
        [Route("create")]
        [Logged]
        public HttpResponseMessage Create_Tour_Package(Tour_PackageDTO dto)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Admin")
                {
                    if (dto != null)
                    {
                        var decision = Tour_PackageService.Create(dto);
                        if (decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Tour Package Created"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to Create Tour Package"
                            };
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                        }

                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Provide Tour Data to Create Package"
                        };
                        return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to create tour packages. Only admin can"
                    };
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Create

        #region Update
        [HttpPut]
        [Route("update")]
        [Logged]
        public HttpResponseMessage Update_Tour_Info(Tour_PackageDTO dto)
        {
            try
            {

                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Admin")
                {
                    if (dto != null)
                    {
                        var decision = Tour_PackageService.Update(dto);
                        if (decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Tour package Update"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to Update Tour package"
                            };
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                        }

                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Provide Tour Data to Create Account"
                        };
                        return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to update tour package. Only Admin can"
                    };
                    return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
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
        public HttpResponseMessage Delete_Tour_Info(int id)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Admin")
                {
                    var data = Tour_PackageService.Delete(id);
                    if (data)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Tour package Not Found"
                        };
                        return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to delete tour packagaes. Only admin can"
                    };
                    return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Delete
    }
}
