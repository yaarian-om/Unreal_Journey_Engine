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
    public class Promotion_TableController : ApiController
    {
        #region Get All Promotion
        [HttpGet]
        [Route("api/promotion/all")]
        public HttpResponseMessage Get_All_Promotion()
        {

            var data = Promotion_TableService.Get();
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
        #endregion Get All Promotion

        #region Get Single Promotion
        [HttpGet]
        [Route("api/promotion/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = Promotion_TableService.Get(id);
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
        #endregion Get Single Promotion

        #region Signup / Create
        [HttpPost]
        [Route("api/promotion/create")]
        public HttpResponseMessage Create_Promotion(Promotion_TableDTO dto)
        {
            try
            {

                if (dto != null)
                {
                    var decision = Promotion_TableService.Create(dto);
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
                        Message = "Provide Tourist Data to Create Account"
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
        [Route("api/promotion/update")]
        public HttpResponseMessage Update_Promotion(Promotion_TableDTO dto)
        {
            try
            {

                if (dto != null)
                {
                    var decision = Promotion_TableService.Update(dto);
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
                        Message = "Provide Tourist Data to Create Account"
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
        [Route("api/promotion/delete/{id}")]
        public HttpResponseMessage Delete_Promotion(int id)
        {
            try
            {
                var data = Promotion_TableService.Delete(id);
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

        // Feature Api Needed

    }
}
