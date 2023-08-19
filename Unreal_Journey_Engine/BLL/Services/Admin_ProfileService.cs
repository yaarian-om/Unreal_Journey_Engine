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
    public class Admin_ProfileService
    {
        #region C R U D Operation

        #region Get All Tour Packages

        public static List<Admin_ProfileDTO> Get()
        {
            var data = RepoAccessFactory.Admin_Profile_Repo_Access().Get();
            if (data.Count > 0)
            {
                var mapper = MapperService<Admin_Profile, Admin_ProfileDTO>.GetMapper();
                var TourDTO = mapper.Map<List<Admin_ProfileDTO>>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Tour Packages

        #region Get Single Tour Package

        public static Admin_ProfileDTO Get(int id)
        {
            var data = RepoAccessFactory.Admin_Profile_Repo_Access().Get(id);
            if (data != null)
            {
                var mapper = MapperService<Admin_Profile, Admin_ProfileDTO>.GetMapper();
                var TourDTO = mapper.Map<Admin_ProfileDTO>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single Tour Packages

        #region Create Tour Package

        public static bool Create(Admin_ProfileDTO dto)
        {
            // Convert to Admin_Profile, from Admin_Profile_DTO
            if (dto != null)
            {
                // There will be no option of image upload While signing up
                // So, we will set a Default image until he/she changes DP
                // This Default image will be set also in the frontend,
                // if he/she do not wanted to update image, then this will come
                dto.Image = "temp_tour.svg";
                var mapper = MapperService<Admin_ProfileDTO, Admin_Profile>.GetMapper();
                var Tour_Data = mapper.Map<Admin_Profile>(dto);

                return RepoAccessFactory.Admin_Profile_Repo_Access().Create(Tour_Data);
            }
            else
            {
                return false;
            }

        }

        #endregion Create Tour Package

        #region Delete a Tour Package

        public static bool Delete(int id)
        {
            if (id > 0)
            {
                return RepoAccessFactory.Admin_Profile_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a Tour Package

        #region Update Tour Package

        public static bool Update(Admin_ProfileDTO dto)
        {
            if (dto != null)
            {
                // Compare To Previous Image_Name
                //var old_data = RepoAccessFactory.Admin_Profile_Repo_Access().Get(dto.Tour_ID);
                var currentTime = DateTime.Now;
                if (dto.Image == null || dto.Image.Equals("") || dto.Image.Equals("temp_tour.svg"))
                {
                    // This Means User Didn't Uploaded the image,
                    // Just Make the Previous One
                }
                else
                {
                    // Making the Image_Name Unique
                    dto.Image = (currentTime.ToString()) + dto.Image;
                }
                var mapper = MapperService<Admin_ProfileDTO, Admin_Profile>.GetMapper();
                var Tour_Data = mapper.Map<Admin_Profile>(dto);
                return RepoAccessFactory.Admin_Profile_Repo_Access().Update(Tour_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update Tour Package

        #endregion C R U D Operation

    }
}
