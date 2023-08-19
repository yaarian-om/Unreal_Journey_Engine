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
