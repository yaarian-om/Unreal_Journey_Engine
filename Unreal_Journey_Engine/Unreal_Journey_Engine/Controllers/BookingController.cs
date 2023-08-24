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
    [RoutePrefix("api/booking")]
    public class BookingController : ApiController
    {


        #region Get All Bookings
        [HttpGet]
        [Route("all")]
        [Logged]
        public HttpResponseMessage Get_All_Bookings()
        {
            var authorizationHeader = Request.Headers.Authorization?.ToString();
            var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
            if (current_user_Type == "Admin")
            {

            }
            else
            {
                var responseMessage = new
                {
                    Message = "You are not allowed to see all bookings. Only admin can"
                };
                return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
            }
            var data = BookingService.Get();
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
        #endregion Get All Bookings

        #region Get Single Booking
        [HttpGet]
        [Route("{id}")]
        [Logged]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = BookingService.Get(id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Booking Not Found"
                    };
                    return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Get Single Booking

        #region  Create
        [HttpPost]
        [Route("create")]
        [Logged]
        public HttpResponseMessage Create_Booking(BookingDTO dto)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Tourist" || current_user_Type == "Admin")
                {
                    if (dto != null)
                    {
                        var decision = BookingService.Create(dto);
                        if (decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Booking Created"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to Create Booking"
                            };
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                        }

                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Provide Booking Data to Create Booking"
                        };
                        return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to Create bookings. Only tourist and admin can"
                    };
                    return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion  Create

        #region Update
        [HttpPut]
        [Route("update")]
        [Logged]
        public HttpResponseMessage Update_Booking_Info(BookingDTO dto)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Tourist" || current_user_Type == "Admin")
                {
                    if (dto != null)
                    {
                        var decision = BookingService.Update(dto);
                        if (decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Booking Updated"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to Update Booking"
                            };
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, responseMessage);
                        }

                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Provide Booking Data to Create Booking"
                        };
                        return Request.CreateResponse(HttpStatusCode.PreconditionFailed, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to Update bookings. Only tourist and admin can"
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
        public HttpResponseMessage Delete_Booking_Info(int id)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Tourist" || current_user_Type == "Admin")
                {
                    var data = BookingService.Delete(id);
                    if (data)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Booking Not Found"
                        };
                        return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to delete bookings. Only tourist and admin can"
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
