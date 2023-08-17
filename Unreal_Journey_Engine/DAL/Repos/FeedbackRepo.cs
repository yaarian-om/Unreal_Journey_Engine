using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class FeedbackRepo : Repo, IRepo<Feedback, int, bool>
    {

        #region C R U D Operation

        #region Create
        public bool Create(Feedback obj)
        {
            try
            {
                if (obj != null)
                {
                    db.Feedbacks.Add(obj);
                    return db.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return false;
            }
        }
        #endregion Create

        #region Delete
        public bool Delete(int id)
        {
            try
            {
                var data = db.Feedbacks.Find(id);
                if (data != null)
                {
                    db.Feedbacks.Remove(data);
                    return db.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return false;
            }
        }
        #endregion Delete

        #region Get All Feedbacks
        public List<Feedback> Get()
        {
            try
            {
                var data = db.Feedbacks.ToList();
                return (data.Count > 0) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get All Feedbacks

        #region Get Single Feedback
        public Feedback Get(int id)
        {
            try
            {
                var data = db.Feedbacks.Find(id);
                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get Single Feedback

        #region Update
        public bool Update(Feedback obj)
        {
            try
            {
                var data = db.Feedbacks.Find(obj.Feedback_ID);
                if (data != null)
                {

                    data.Feedback_ID = obj.Feedback_ID;
                    data.Subject = obj.Subject;
                    data.Description = obj.Description;
                    data.Status = obj.Status;
                    data.User_ID= obj.User_ID;

                    return db.SaveChanges() > 0;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Print_in_Red(ex.Message);
                return false;
            }


        }
        #endregion Update

        #endregion C R U D Operation


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
