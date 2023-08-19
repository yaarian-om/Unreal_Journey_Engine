using AutoMapper;
using BLL.DTOs;
using DAL.Database.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Tour_PackageService
    {
        #region C R U D Operation

        #region Get All Tour Packages

        public static List<Tour_PackageDTO> Get()
        {
            var data = RepoAccessFactory.Tour_Package_Repo_Access().Get();
            if (data.Count > 0)
            {
                var mapper = MapperService<Tour_Package, Tour_PackageDTO>.GetMapper();
                var TourDTO = mapper.Map<List<Tour_PackageDTO>>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Tour Packages

        #region Get Single Tour Package

        public static Tour_PackageDTO Get(int id)
        {
            var data = RepoAccessFactory.Tour_Package_Repo_Access().Get(id);
            if (data != null)
            {
                var mapper = MapperService<Tour_Package, Tour_PackageDTO>.GetMapper();
                var TourDTO = mapper.Map<Tour_PackageDTO>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single Tour Package

        #region Create Tour Package

        public static bool Create(Tour_PackageDTO dto)
        {
            // Convert to Tour_Package, from Tour_Package_DTO
            if (dto != null)
            {
                // There will be no option of image upload While signing up
                // So, we will set a Default image until he/she changes DP
                // This Default image will be set also in the frontend,
                // if he/she do not wanted to update image, then this will come
                dto.Image = "temp_tour.svg";
                var mapper = MapperService<Tour_PackageDTO, Tour_Package>.GetMapper();
                var Tour_Data = mapper.Map<Tour_Package>(dto);

                return RepoAccessFactory.Tour_Package_Repo_Access().Create(Tour_Data);
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
                return RepoAccessFactory.Tour_Package_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a Tour Package

        #region Update Tour Package

        public static bool Update(Tour_PackageDTO dto)
        {
            if (dto != null)
            {
                // Compare To Previous Image_Name
                //var old_data = RepoAccessFactory.Tour_Package_Repo_Access().Get(dto.Tour_ID);
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

                var mapper = MapperService<Tour_PackageDTO, Tour_Package>.GetMapper();
                var Tour_Data = mapper.Map<Tour_Package>(dto);
                return RepoAccessFactory.Tour_Package_Repo_Access().Update(Tour_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update Tour Package

        #endregion C R U D Operation


        // Feature Api Needed
        // Update Image()
    }
}
