using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Tourist_ProfileService
    {
        #region C R U D Operation

        #region Get All Tourist Profiles

        public static List<Tourist_ProfileDTO> Get()
        {
            var data = RepoAccessFactory.Tourist_Profile_Repo_Access().Get();
            if (data.Count > 0)
            {
                
                var mapper = MapperService<Tourist_Profile, Tourist_ProfileDTO>.GetMapper();
                var TouristDTO = mapper.Map<List<Tourist_ProfileDTO>>(data);
                return TouristDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Tourist Profiles

        #region Get Single Tourist Profile
        
        public static Tourist_ProfileDTO Get(int id)
        {
            var data = RepoAccessFactory.Tourist_Profile_Repo_Access().Get(id);
            if(data != null)
            {
                var mapper = MapperService<Tourist_Profile, Tourist_ProfileDTO>.GetMapper();
                var TouristDTO = mapper.Map<Tourist_ProfileDTO>(data);
                return TouristDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single Tourist Profile

        #region Create Tourist Profile
        
        public static bool Create(Tourist_ProfileDTO dto)
        {
            // Convert to Tourist_Profile, from Tourist_Profile_DTO
            if(dto != null)
            {
                // There will be no option of image upload While signing up
                // So, we will set a Default image until he/she changes DP
                // This Default image will be set also in the frontend,
                // if he/she do not wanted to update image, then this will come
                dto.Image = "temp.svg";
                var mapper = MapperService<Tourist_ProfileDTO, Tourist_Profile>.GetMapper();
                var Tourist_Data = mapper.Map<Tourist_Profile>(dto);

                return RepoAccessFactory.Tourist_Profile_Repo_Access().Create(Tourist_Data);
            }
            else
            {
                return false;
            }

        }

        #endregion Create Tourist Profile

        #region Delete a Tourist Profile
        
        public static bool Delete(int id)
        {
            if(id > 0 )
            {
                return RepoAccessFactory.Tourist_Profile_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a Tourist Profile

        #region Update Tourist Profile
        
        public static bool Update(Tourist_ProfileDTO dto)
        {
            if(dto != null)
            {
                // Compare To Previous Image_Name
                //var old_data = RepoAccessFactory.Tour_Package_Repo_Access().Get(dto.Tourist_ID);
                var currentTime = DateTime.Now;
                if (dto.Image == null || dto.Image.Equals("") || dto.Image.Equals("temp.svg"))
                {
                    // This Means User Didn't Uploaded the image,
                    // Just Make the Previous One
                }
                else
                {
                    // Making the Image_Name Unique
                    dto.Image = (currentTime.ToString()) + dto.Image;
                }


                var mapper = MapperService<Tourist_ProfileDTO, Tourist_Profile>.GetMapper();
                var Tourist_Data = mapper.Map<Tourist_Profile>(dto);
                return RepoAccessFactory.Tourist_Profile_Repo_Access().Update(Tourist_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update Tourist Profile

        #endregion C R U D Operation


        // Feature Api Needed

        #region Feature APIs

        #region Upload_Image

        public static bool Upload_Image(byte[] image, string imageName, int Tourist_ID)
        {
            try
            {
                var data = RepoAccessFactory.Tourist_Profile_Repo_Access().Get(Tourist_ID);
                if (data != null)
                {
                    // Using DateTime Stamp to avoid Same name or Identical Names
                    var currentTime = DateTime.Now;
                    var final_imageName = (currentTime.ToString()) + imageName;
                    data.Image = final_imageName;
                    // Sending New Image name to the Server
                    var update_decision = RepoAccessFactory.Tourist_Profile_Repo_Access().Update(data);
                    if (update_decision)
                    {
                        // Send the Image and Image Name to the Repo
                        var upload_decision = RepoAccessFactory.Tourist_Profile_Image_Repo_Access().Upload_Image(image, final_imageName);
                        if (upload_decision)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                Print_in_Red("Tourist_ProfileService Error = " + ex.Message);
                return false;
            }
        }

        #endregion Upload_Image

        #region Get Image
        public static byte[] Get_Image(int Tourist_ID)
        {
            var data = RepoAccessFactory.Tourist_Profile_Repo_Access().Get(Tourist_ID);
            var imageName = data.Image.ToString();
            if (data != null)
            {
                return RepoAccessFactory.Tourist_Profile_Image_Repo_Access().Get_Image(imageName);
            }
            else
            {
                return null;
            }

            // Else Null
        }
        #endregion Get Image

        #region Get Tourist By User_ID

        public static Tourist_ProfileDTO Get_by_User_ID(int id)
        {
            var touristProfile = (from profile in RepoAccessFactory.Tourist_Profile_Repo_Access().Get()
                                  where profile.User_ID == id
                                  select profile).SingleOrDefault();

            var mapper = MapperService<Tourist_Profile, Tourist_ProfileDTO>.GetMapper();
            var TouristDTO = mapper.Map<Tourist_ProfileDTO>(touristProfile);

            return (touristProfile != null)? TouristDTO : null;
        }


        #endregion Get Tourist By User_ID

        #region Forget Password

        public static bool Send_Pin(string Email)
        {
            var user_info = (from t in RepoAccessFactory.User_Repo_Access().Get()
                             where t.Email.Equals(Email)
                             select t).SingleOrDefault();
            if (user_info != null)
            {
                // Generate and Send Pin
                int randomPin = new Random().Next(100000, 1000000);
                var create_pin = new Pin_Code() {
                    Pin_Code_ID = 0,
                    Pin = randomPin,
                    User_ID = user_info.User_ID
                };

                // Send Mail Code here
                var decision = EmailService.SendEmail(user_info.Email, "Password Reset Code", 
                    $"Dear Tourist,\r\n" +
                    $"You have requested to reset your password for your account. To proceed with the password reset, please use the verification code provided below:\r\n\r\n" +
                    $"Verification Code: {randomPin}\r\n\r\n" +
                    $"Please enter this code on the password reset page to verify your identity. This code will expire after a certain duration for security purposes.\r\n\r\n" +
                    $"If you did not request a password reset or if you have any concerns regarding your account security, please contact our support team immediately.\r\n\r\nBest regards,\r\n" +
                    $"Unreal Journey Team");
                if (decision)
                {
                    return RepoAccessFactory.Pin_Code_Repo_Access().Create(create_pin);
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }

        public static bool Verify_Pin(string s_pin)
        {
            int pin = int.Parse(s_pin);
            var decision = (from t in RepoAccessFactory.Pin_Code_Repo_Access().Get()
                            where t.Pin.Equals(pin)
                            //&& t.User_ID.Equals(pin)
                            select t).SingleOrDefault();
            var Update_pin = new Pin_Code()
            {
                Pin_Code_ID = -1,
                Pin = pin,
                User_ID = -1
            };
            return RepoAccessFactory.Pin_Code_Repo_Access().Update(Update_pin);
        }

        public static bool Update_Password(string Email, string New_Password)
        {
            var user_info = (from t in RepoAccessFactory.User_Repo_Access().Get()
                             where t.Email.Equals(Email)
                             select t).SingleOrDefault();
            if(user_info != null)
            {
                user_info.Password = New_Password;
                return RepoAccessFactory.User_Repo_Access().Update(user_info);
            }
            else
            {
                return false;
            }
            
        }

        #endregion Forget Password

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
