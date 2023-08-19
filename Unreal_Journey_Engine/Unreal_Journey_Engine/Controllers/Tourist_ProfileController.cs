using BLL.DTOs;
using BLL.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Unreal_Journey_Engine.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/tourist")]
    public class Tourist_ProfileController : ApiController
    {

        #region Get All Tourist
        [HttpGet]
        [Route("all")]
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
        [Route("{id}")]
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
        [Route("create")]
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
        [Route("update")]
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
                            Message = "Account Updated"
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
        [Route("delete/{id}")]
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
        #endregion Delete


        #region Feature APIs


        // Feature 1 : Upload Profile Image
        #region Upload Image

        [HttpPost]
        [Route("image/upload")]
        public HttpResponseMessage Upload_Tourist_Profile_Image()
        {
            try
            {
                //System.Web.HttpContext.Current.Session["Tourist_ID"] = 1;
                // Using Session Here
                //var current_user_ID = (int)System.Web.HttpContext.Current.Session["Tourist_ID"];
                var current_user_ID = 1;
                if (current_user_ID > 0)
                {
                    var file = HttpContext.Current.Request.Files[0];

                    if (file == null || file.ContentLength == 0)
                    {
                        var responseMessage = new
                        {
                            Message = "No Image Uploaded"
                        };
                        return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
                    }
                    else
                    {
                        byte[] imageData;
                        using(var memoryStream = new MemoryStream())
                        {
                            file.InputStream.CopyTo(memoryStream);
                            imageData = memoryStream.ToArray();
                        }
                        var final_decision = Tourist_ProfileService.Upload_Image(imageData, file.FileName, current_user_ID);
                        if (final_decision)
                        {
                            var responseMessage = new
                            {
                                Message = "Image Updated Successsfully"
                            };
                            return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                        }
                        else
                        {
                            var responseMessage = new
                            {
                                Message = "Failed to update the Image"
                            };
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
                        }
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Please Login First"
                    };
                    return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
                }



            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #endregion Upload Image

        // Feature 2 : Get Profile Image
        #region Get Image
        [HttpGet]
        [Route("image/")]
        public HttpResponseMessage Get_Tourist_Profile_Image()
        {
            try
            {
                // Using Session Here
                //var current_user = (int)System.Web.HttpContext.Current.Session["Tourist_ID"];
                var current_user = 1;
                if (current_user > 0 )
                {

                    var image = Tourist_ProfileService.Get_Image(current_user);
                    
                    if (image != null)
                    {
                        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                        response.Content = new ByteArrayContent(image);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png"); // Set the appropriate content type
                        return response;
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Image Not Found"
                        };
                        return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Please Login First"
                    };
                    return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
                }

                
               
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion Get Image


        #endregion Feature APIs



        #region Text Color Configuration in CONSOLE
        public static void Print_in_Red(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void Print_in_Green(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }




        #endregion Text Color Configuration in CONSOLE


    }
}
