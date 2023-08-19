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
    public class FeedbackService
    {

        #region C R U D Operation

        #region Get All Feedbacks

        public static List<FeedbackDTO> Get()
        {
            var data = RepoAccessFactory.Feedback_Repo_Access().Get();
            if (data.Count > 0)
            {
                var mapper = MapperService<Feedback, FeedbackDTO>.GetMapper();
                var TourDTO = mapper.Map<List<FeedbackDTO>>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get All Feedbacks

        #region Get Single Feedback

        public static FeedbackDTO Get(int id)
        {
            var data = RepoAccessFactory.Feedback_Repo_Access().Get(id);
            if (data != null)
            {
                var mapper = MapperService<Feedback, FeedbackDTO>.GetMapper();
                var TourDTO = mapper.Map<FeedbackDTO>(data);
                return TourDTO;
            }
            else
            {
                return null;
            }
        }

        #endregion Get Single Feedback

        #region Create Feedback

        public static bool Create(FeedbackDTO dto)
        {
            // Convert to Feedback, from Feedback_DTO
            if (dto != null)
            {
                var mapper = MapperService<FeedbackDTO, Feedback>.GetMapper();
                var Tour_Data = mapper.Map<Feedback>(dto);

                return RepoAccessFactory.Feedback_Repo_Access().Create(Tour_Data);
            }
            else
            {
                return false;
            }

        }

        #endregion Create Feedback

        #region Delete a Feedback

        public static bool Delete(int id)
        {
            if (id > 0)
            {
                return RepoAccessFactory.Feedback_Repo_Access().Delete(id);
            }
            else
            {
                return false;
            }
        }

        #endregion Delete a Feedback

        #region Update Feedback

        public static bool Update(FeedbackDTO dto)
        {
            if (dto != null)
            {
                var mapper = MapperService<FeedbackDTO, Feedback>.GetMapper();
                var Tour_Data = mapper.Map<Feedback>(dto);
                return RepoAccessFactory.Feedback_Repo_Access().Update(Tour_Data);
            }
            else
            {
                return false;
            }
        }

        #endregion Update Feedback

        #endregion C R U D Operation


        // Feature Api Needed



    }
}
