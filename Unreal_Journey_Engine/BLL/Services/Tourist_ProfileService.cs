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
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tourist_Profile, Tourist_ProfileDTO>();
                });
                var mapper = new Mapper(config);
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
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Tourist_Profile, Tourist_ProfileDTO>();
                });
                var mapper = new Mapper(config);
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
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tourist_ProfileDTO, Tourist_Profile>();
                });
                var mapper = new Mapper(config);
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


                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tourist_ProfileDTO, Tourist_Profile>();
                });
                var mapper = new Mapper(config);
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
        // Update Image()



    }
}
