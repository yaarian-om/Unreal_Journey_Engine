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
    [RoutePrefix("api/tour/review")]
    public class Tour_ReviewController : ApiController
    {

        #region Get All Tour Reviews
        [HttpGet]
        [Route("all")]
        [Logged]
        public HttpResponseMessage Get_All_Tour_Reviews()
        {

            var data = Tour_ReviewService.Get();
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
        #endregion Get All Tour Reviews

        #region Get Single Tour Review
        [HttpGet]
        [Route("{id}")]
        [Logged]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = Tour_ReviewService.Get(id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Review Not Found"
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

        #region Post / Create
        [HttpPost]
        [Route("create")]
        [Logged]
        public HttpResponseMessage Create_Tour_Review(Tour_ReviewDTO dto)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Tourist")
                {
                    if (dto != null)
                    {
                        var decision = Tour_ReviewService.Create(dto);
                        if (decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Review Posted"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to Post Review"
                            };
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                        }

                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Provide Tour Review Data to Post a Review"
                        };
                        return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to give any review.  Only tourist can."
                    };
                    return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Post / Create

        #region Update
        [HttpPut]
        [Route("update")]
        [Logged]
        public HttpResponseMessage Update_Tour_Info(Tour_ReviewDTO dto)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Tourist" || current_user_Type == "Admin")
                {
                    if (dto != null)
                    {
                        var decision = Tour_ReviewService.Update(dto);
                        if (decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Review Updated"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to Update Review"
                            };
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                        }

                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Provide Tour Data to Update Review"
                        };
                        return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allow to update tour review.  Only tourist and admin can."
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
        public HttpResponseMessage Delete_Tour_Review(int id)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Tourist" || current_user_Type == "Admin")
                {
                    var data = Tour_ReviewService.Delete(id);
                    if (data)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Review Not Found"
                        };
                        return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to delete review. Only tourist and admin can."
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
