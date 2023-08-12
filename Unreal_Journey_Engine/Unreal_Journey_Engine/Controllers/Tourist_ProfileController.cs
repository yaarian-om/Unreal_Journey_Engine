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
    public class Tourist_ProfileController : ApiController
    {

        #region Get All Tourist
        [HttpGet]
        [Route("api/tourist/all")]
        public HttpResponseMessage Get_All_Tourist()
        {

            var data = Tourist_ProfileService.Get();
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
        #endregion Get All Tourist

        #region Get Single Tourist
        [HttpGet]
        [Route("api/tourist/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = Tourist_ProfileService.Get(id);
                if(data != null)
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
        #endregion Get Single Tourist

        #region Signup / Create
        [HttpPost]
        [Route("api/tourist/create")]
        public HttpResponseMessage Create_Tourist_Profile(Tourist_ProfileDTO dto)
        {
            try
            {
                
                if (dto != null)
                {
                    var decision = Tourist_ProfileService.Create(dto);
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
        [Route("api/tourist/update")]
        public HttpResponseMessage Update_Tourist_Profile(Tourist_ProfileDTO dto)
        {
            try
            {

                if (dto != null)
                {
                    var decision = Tourist_ProfileService.Update(dto);
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
        [HttpGet]
        [Route("api/tourist/delete/{id}")]
        public HttpResponseMessage Delete_Tourist_Profile(int id)
        {
            try
            {
                var data = Tourist_ProfileService.Delete(id);
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
        #endregion Get Single Tourist

        // Feature Api Needed



    }
}
