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
    public class Tour_ReviewService
    {


        #region C R U D Operation

        #region Get All Tour Reviews

        public static List<Tour_ReviewDTO> Get()
        {
            var data = RepoAccessFactory.Tour_Review_Repo_Access().Get();
            if (data.Count > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tour_Review, Tour_ReviewDTO>();
                });
                var mapper = new Mapper(config);
                var TourDTO = mapper.Map<List<Tour_ReviewDTO>>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Tour Reviews

        #region Get Single Tour Review

        public static Tour_ReviewDTO Get(int id)
        {
            var data = RepoAccessFactory.Tour_Review_Repo_Access().Get(id);
            if (data != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Tour_Review, Tour_ReviewDTO>();
                });
                var mapper = new Mapper(config);
                var TourDTO = mapper.Map<Tour_ReviewDTO>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single Tour Review

        #region Create Tour Review

        public static bool Create(Tour_ReviewDTO dto)
        {
            // Convert to Tour_Review, from Tour_Review_DTO
            if (dto != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tour_ReviewDTO, Tour_Review>();
                });
                var mapper = new Mapper(config);
                var Tour_Data = mapper.Map<Tour_Review>(dto);

                return RepoAccessFactory.Tour_Review_Repo_Access().Create(Tour_Data);
            }
            else
            {
                return false;
            }

        }

        #endregion Create Tour Review

        #region Delete a Tour Review

        public static bool Delete(int id)
        {
            if (id > 0)
            {
                return RepoAccessFactory.Tour_Review_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a Tour Review

        #region Update Tour Review

        public static bool Update(Tour_ReviewDTO dto)
        {
            if (dto != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tour_ReviewDTO, Tour_Review>();
                });
                var mapper = new Mapper(config);
                var Tour_Data = mapper.Map<Tour_Review>(dto);
                return RepoAccessFactory.Tour_Review_Repo_Access().Update(Tour_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update Tour Review

        #endregion C R U D Operation


        // Feature Api Needed


    }
}
