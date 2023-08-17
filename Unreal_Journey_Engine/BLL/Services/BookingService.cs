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
    public class BookingService
    {

        #region C R U D Operation

        #region Get All Bookings

        public static List<BookingDTO> Get()
        {
            var data = RepoAccessFactory.Booking_Repo_Access().Get();
            if (data.Count > 0)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Booking, BookingDTO>();
                });
                var mapper = new Mapper(config);
                var TourDTO = mapper.Map<List<BookingDTO>>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Bookings

        #region Get Single Booking

        public static BookingDTO Get(int id)
        {
            var data = RepoAccessFactory.Booking_Repo_Access().Get(id);
            if (data != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Booking, BookingDTO>();
                });
                var mapper = new Mapper(config);
                var TourDTO = mapper.Map<BookingDTO>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single Booking

        #region Create Booking

        public static bool Create(BookingDTO dto)
        {
            // Convert to Booking, from Booking_DTO
            if (dto != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingDTO, Booking>();
                });
                var mapper = new Mapper(config);
                var Tour_Data = mapper.Map<Booking>(dto);

                return RepoAccessFactory.Booking_Repo_Access().Create(Tour_Data);
            }
            else
            {
                return false;
            }

        }

        #endregion Create Booking

        #region Delete a Booking

        public static bool Delete(int id)
        {
            if (id > 0)
            {
                return RepoAccessFactory.Booking_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a Booking

        #region Update Booking

        public static bool Update(BookingDTO dto)
        {
            if (dto != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingDTO, Booking>();
                });
                var mapper = new Mapper(config);
                var Tour_Data = mapper.Map<Booking>(dto);
                return RepoAccessFactory.Booking_Repo_Access().Update(Tour_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update Booking

        #endregion C R U D Operation


        // Feature Api Needed




    }
}
