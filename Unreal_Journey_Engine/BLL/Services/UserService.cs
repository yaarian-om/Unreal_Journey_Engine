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
    public class UserService
    {

        #region C R U D Operation

        #region Get All Users

        public static List<UserDTO> Get()
        {
            var data = RepoAccessFactory.User_Repo_Access().Get();
            if (data.Count > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDTO>();
                });
                var mapper = new Mapper(config);
                var TourDTO = mapper.Map<List<UserDTO>>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Users

        #region Get Single User

        public static UserDTO Get(int id)
        {
            var data = RepoAccessFactory.User_Repo_Access().Get(id);
            if (data != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>();
                });
                var mapper = new Mapper(config);
                var TourDTO = mapper.Map<UserDTO>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single User

        #region Create User

        public static bool Create(UserDTO dto)
        {
            // Convert to User, from User_DTO
            if (dto != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserDTO, User>();
                });
                var mapper = new Mapper(config);
                var Tour_Data = mapper.Map<User>(dto);

                return RepoAccessFactory.User_Repo_Access().Create(Tour_Data);
            }
            else
            {
                return false;
            }

        }

        #endregion Create User

        #region Delete a User

        public static bool Delete(int id)
        {
            if (id > 0)
            {
                return RepoAccessFactory.User_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a User

        #region Update User

        public static bool Update(UserDTO dto)
        {
            if (dto != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserDTO, User>();
                });
                var mapper = new Mapper(config);
                var Tour_Data = mapper.Map<User>(dto);
                return RepoAccessFactory.User_Repo_Access().Update(Tour_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update User

        #endregion C R U D Operation


        // Feature Api Needed


    }
}
