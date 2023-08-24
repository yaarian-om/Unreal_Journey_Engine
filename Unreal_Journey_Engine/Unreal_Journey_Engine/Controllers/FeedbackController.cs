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
    [RoutePrefix("api/feedback")]
    public class FeedbackController : ApiController
    {


        #region Get All Feedbacks
        [HttpGet]
        [Route("all")]
        [Logged]
        public HttpResponseMessage Get_All_Feedbacks()
        {

            var authorizationHeader = Request.Headers.Authorization?.ToString();
            var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
            if (current_user_Type == "Admin")
            {
                var data = FeedbackService.Get();
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
            else
            {
                var responseMessage = new
                {
                    Message = "You are not allowrd to access all the feedbacks"
                };
                return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
            }

            

        }
        #endregion Get All Feedbacks

        #region Get Single Feedback
        [HttpGet]
        [Route("{id}")]
        [Logged]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = FeedbackService.Get(id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Feedback Not Found"
                    };
                    return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Get Single Feedback

        #region Post / Create
        [HttpPost]
        [Route("create")]
        [Logged]
        public HttpResponseMessage Create_Feedback(FeedbackDTO dto)
        {
            try
            {

                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Tourist" || current_user_Type == "Tour_Guide")
                {
                    if (dto != null)
                    {
                        var decision = FeedbackService.Create(dto);
                        if (decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Feedback Posted"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to Create Feedback"
                            };
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                        }

                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Provide Feedback Data to Create Feedback"
                        };
                        return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to send any feedbacks. Only Tourists and Tour Guides can"
                    };
                    return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
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
        public HttpResponseMessage Update_Feedback_Info(FeedbackDTO dto)
        {
            try
            {

                if (dto != null)
                {
                    var authorizationHeader = Request.Headers.Authorization?.ToString();
                    var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                    if (current_user_Type == "Admin")
                    {
                        var decision = FeedbackService.Update(dto);
                        if (decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Feedback Updated"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to Update Feedback"
                            };
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                        }
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "You are not allowed to Update Feedbacks. Only admin can"
                        };
                        return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
                    }
                    

                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Provide Feedback Data to Create Feedback"
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
        public HttpResponseMessage Delete_Feedback_Info(int id)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if(current_user_Type == "Admin")
                {
                    var data = FeedbackService.Delete(id);
                    if (data)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Feedback Not Found"
                        };
                        return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to delete feedbacks. Only admin can."
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
