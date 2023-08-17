using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Unreal_Journey_Engine.Controllers
{
    public class Tour_ReviewController : ApiController
    {

        #region Get All Tour Reviews
        [HttpGet]
        [Route("api/tour/review/all")]
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

        #region Get Single Tour
        [HttpGet]
        [Route("api/tour/review/{id}")]
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
        [Route("api/tour/review/create")]
        public HttpResponseMessage Create_Tour_Review(Tour_ReviewDTO dto)
        {
            try
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
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Post / Create

        #region Update
        [HttpPut]
        [Route("api/tour/review/update")]
        public HttpResponseMessage Update_Tour_Info(Tour_ReviewDTO dto)
        {
            try
            {

                if (dto != null)
                {
                    var decision = Tour_ReviewService.Update(dto);
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
                        Message = "Provide Tour Data to Create Account"
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
        [Route("api/tour/review/delete/{id}")]
        public HttpResponseMessage Delete_Tour_Review(int id)
        {
            try
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
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Delete


    }
}
