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
    public class Promotion_TableService
    {
        #region C R U D Operation

        #region Get All Tourist Profiles

        public static List<Promotion_TableDTO> Get()
        {
            var data = RepoAccessFactory.Promotion_Table_Repo_Access().Get();
            if (data.Count > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Promotion_Table, Promotion_TableDTO>();
                });
                var mapper = new Mapper(config);
                var TouristDTO = mapper.Map<List<Promotion_TableDTO>>(data);
                return TouristDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Tourist Profiles

        #region Get Single Tourist Profile

        public static Promotion_TableDTO Get(int id)
        {
            var data = RepoAccessFactory.Promotion_Table_Repo_Access().Get(id);
            if (data != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Promotion_Table, Promotion_TableDTO>();
                });
                var mapper = new Mapper(config);
                var TouristDTO = mapper.Map<Promotion_TableDTO>(data);
                return TouristDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single Tourist Profile

        #region Create Tourist Profile

        public static bool Create(Promotion_TableDTO dto)
        {
            // Convert to Promotion_Table, from Promotion_Table_DTO
            if (dto != null)
            {
                 
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Promotion_TableDTO, Promotion_Table>();
                });
                var mapper = new Mapper(config);
                var Tourist_Data = mapper.Map<Promotion_Table>(dto);

                return RepoAccessFactory.Promotion_Table_Repo_Access().Create(Tourist_Data);
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
            if (id > 0)
            {
                return RepoAccessFactory.Promotion_Table_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a Tourist Profile

        #region Update Tourist Profile

        public static bool Update(Promotion_TableDTO dto)
        {
            if (dto != null)
            {

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Promotion_TableDTO, Promotion_Table>();
                });
                var mapper = new Mapper(config);
                var Tourist_Data = mapper.Map<Promotion_Table>(dto);
                return RepoAccessFactory.Promotion_Table_Repo_Access().Update(Tourist_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update Tourist Profile

        #endregion C R U D Operation
    }
}
