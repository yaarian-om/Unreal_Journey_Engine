using BLL.DTOs;
using BLL.Services;
using Newtonsoft.Json.Linq;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;
using Unreal_Journey_Engine.AuthFilters;

namespace Unreal_Journey_Engine.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/tourist")]
    public class Tourist_ProfileController : ApiController
    {

        #region Get All Tourist
        [HttpGet]
        [Route("all")]
        [Logged]
        public HttpResponseMessage Get_All_Tourist()
        {
            var current_user_Type = "";
            var authorizationHeader = Request.Headers.Authorization?.ToString();
            current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
            if(current_user_Type == "Admin")
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
            else
            {
                var responseMessage = new
                {
                    Message = "You are not allowed to get all info of Tourists"
                };
                return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
            }

        }
        #endregion Get All Tourist

        #region Get Single Tourist
        [HttpGet]
        [Route("{id}")]
        [Logged]
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
        [Logged]
        public HttpResponseMessage Create_Tourist_Profile(Tourist_ProfileDTO dto)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Tourist" || current_user_Type == "Admin")
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
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to create tourist account. Only Tourist and admin can"
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
        public HttpResponseMessage Update_Tourist_Profile(Tourist_ProfileDTO dto)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Admin" || current_user_Type == "Tourist")
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
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to update tourist profile. Only Tourist and admin can"
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
        public HttpResponseMessage Delete_Tourist_Profile(int id)
        {
            try
            {
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                var current_user_Type = User_Info_Provider.Get_User_Role(authorizationHeader);
                if (current_user_Type == "Admin" || current_user_Type == "Tourist")
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
                else
                {
                    var responseMessage = new
                    {
                        Message = "You are not allowed to delete tourist profile. Only Tourist and admin can"
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


        #region Feature APIs


        // Feature 1 : Upload Profile Image
        #region Upload Image

        [HttpPost]
        [Route("image/upload")]
        [Logged]
        public HttpResponseMessage Upload_Tourist_Profile_Image()
        {
            try
            {
                //System.Web.HttpContext.Current.Session["Tourist_ID"] = 1;
                // Using Session Here
                //var current_user_ID = (int)System.Web.HttpContext.Current.Session["Tourist_ID"];
                var current_user_ID = 0;
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                current_user_ID = User_Info_Provider.Get_User_ID(authorizationHeader);
                var tourist_info = Tourist_ProfileService.Get_by_User_ID(current_user_ID);
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
                        var final_decision = Tourist_ProfileService.Upload_Image(imageData, file.FileName, tourist_info.Tourist_ID);
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
        [Route("image")]
        [Logged]
        public HttpResponseMessage Get_Tourist_Profile_Image()
        {
            try
            {

                //if (!string.IsNullOrEmpty(authorizationHeader))
                //{
                //    current_user = AuthService.IsTokenValid(authorizationHeader);
                //}

                var current_user = 0;
                var authorizationHeader = Request.Headers.Authorization?.ToString();
                current_user = User_Info_Provider.Get_User_ID(authorizationHeader);


                if (current_user > 0 )
                {
                    var current_tourist = Tourist_ProfileService.Get_by_User_ID(current_user);
                    var image = Tourist_ProfileService.Get_Image(current_tourist.Tourist_ID);
                    
                    if (image != null)
                    {
                        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                        var copy_Byte_image = image;
                        response.Content = new ByteArrayContent(image);
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue(GetImageContentType(copy_Byte_image)); // Set the appropriate content type
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

        #region Get Image Type
        private string GetImageContentType(byte[] image)
        {
            if (image.Length >= 2)
            {
                if (image[0] == 0xFF && image[1] == 0xD8) // JPEG magic number
                {
                    return "image/jpeg";
                }
                else if (image[0] == 0x89 && image[1] == 0x50 && image[2] == 0x4E && image[3] == 0x47) // PNG magic number
                {
                    return "image/png";
                }
                else if (image[0] == 0x47 && image[1] == 0x49 && image[2] == 0x46 && image[3] == 0x38) // GIF8 magic number
                {
                    return "image/gif";
                }
                else if (image[0] == 0x3C && image[1] == 0x73 && image[2] == 0x76 && image[3] == 0x67) // SVG magic number (XML declaration)
                {
                    return "image/svg+xml";
                }
            }

            return "application/octet-stream";
        }
        #endregion Get Image Type





        #endregion Get Image

        // Feature 3 : Forget Password OTP varification
        #region Forget Password

        [HttpPut]
        [Route("forget_password")]
        public HttpResponseMessage Get_Pin(Forget_PasswordDTO obj)
        {
            if(obj.Email !=  null)
            {
                var decision = Tourist_ProfileService.Send_Pin(obj.Email);
                if (decision)
                {
                    //HttpContext.Current.Session["Email"] = obj.Email;
                    // Email has to be sent from the Frontend through Frontend Session
                    var responseMessage = new
                    {
                        Message = "Pin has been sent to " + obj.Email
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Could Not Sent the Varification Pin to " + obj.Email
                    };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
                }
            }
            else
            {
                var responseMessage = new
                {
                    Message = "Please enter your login email"
                };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
            }
        }

        [HttpPut]
        [Route("verify_pin")]
        public HttpResponseMessage Verify_Pin(Forget_PasswordDTO obj)
        {
            if (obj.Pin != null)
            {
                var decision = Tourist_ProfileService.Verify_Pin(obj.Pin);
                if (decision)
                {
                    var responseMessage = new
                    {
                        Message = "Pin Verfication Successful. Right Pin Given"
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Failed to verify the pin. Pin is incorrect"
                    };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
                }
            }
            else
            {
                var responseMessage = new
                {
                    Message = "Please provide pin first"
                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
            }
        }

        [HttpPut]
        [Route("update_password")]
        public HttpResponseMessage Update_Password(Forget_PasswordDTO obj)
        {
            if(!string.IsNullOrEmpty(obj.Password))
            {
                var Email = obj.Email;
                if (Email != null)
                {
                    var decision = Tourist_ProfileService.Update_Password(Email, obj.Password);
                    if (decision)
                    {
                        var responseMessage = new
                        {
                            Message = "Password Updated Successfully"
                        };
                        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                    }
                    else
                    {
                        var responseMessage = new
                        {
                            Message = "Failed to update password"
                        };
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
                    }
                }
                else
                {
                    var responseMessage = new
                    {
                        Message = "Could not retrive your given mail for the forget password"
                    };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
                }
            }
            else
            {
                var responseMessage = new
                {
                    Message = "Please provide your new password"
                };
                return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
            }
        }

        #endregion Forget Password

        // Feature 4 : Get Weather of given Location
        #region Weather Data
        [HttpGet]
        [Route("weather/{location}")]
        [Logged]
        // Here Task is used for Asyncronous Ability / Programming
        public async Task<IHttpActionResult> Get_weather_Data(string location)
        {
            if(!string.IsNullOrEmpty(location))
            {
                string weatherData = await WeatherService.GetWeatherAsync(location);

                if (weatherData != null)
                {
                    var jsonObject = JObject.Parse(weatherData);

                    return Json(jsonObject); // This will return JSON data
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("Please provide your Location");
            }
        }
        #endregion Weather Data

        // Feature 5 : Get all Bookings by Logged in Tourist ID
        #region Get All Bookings by Logged in Tourist ID
        [HttpGet]
        [Route("bookings/all")]
        [Logged]
        public HttpResponseMessage Get_All_Booking_by_Tourist_ID()
        {
            var authorizationHeader = Request.Headers.Authorization?.ToString();
            var user_type = User_Info_Provider.Get_User_Role(authorizationHeader);
            if (user_type == "Tourist")
            {
                var current_tourist_ID = User_Info_Provider.Get_Tourist_ID(authorizationHeader);
                var data = Tourist_ProfileService.Get_All_Booking_by_Tourist_ID(current_tourist_ID);
                if (data != null && data.Count > 0)
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
                    Message = "You are not allowed. Only tourist can"
                };
                return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
            }
        }


        #endregion Get All Bookings by Logged in Tourist ID

        // Feature 5 : Get all Reviews by Logged in Tourist ID
        #region Get All Reviews by Logged in Tourist ID
        [HttpGet]
        [Route("reviews/all")]
        [Logged]
        public HttpResponseMessage Get_All_Reviews_by_Tourist_ID()
        {
            var authorizationHeader = Request.Headers.Authorization?.ToString();
            var user_type = User_Info_Provider.Get_User_Role(authorizationHeader);
            if (user_type == "Tourist")
            {
                var current_tourist_ID = User_Info_Provider.Get_Tourist_ID(authorizationHeader);
                var data = Tourist_ProfileService.Get_All_Reviews_by_Tourist_ID(current_tourist_ID);
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
                    Message = "You are not allowed. Only tourist can"
                };
                return Request.CreateResponse(HttpStatusCode.Forbidden, responseMessage);
            }
        }


        #endregion Get All Reviews by Logged in Tourist ID







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
