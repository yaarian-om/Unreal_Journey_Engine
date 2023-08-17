using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class Tour_ReviewRepo : Repo, IRepo<Tour_Review, int, bool>
    {

        #region C R U D Operation

        #region Create
        public bool Create(Tour_Review obj)
        {
            try
            {
                if (obj != null)
                {
                    db.Tour_Reviews.Add(obj);
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
                var data = db.Tour_Reviews.Find(id);
                if (data != null)
                {
                    db.Tour_Reviews.Remove(data);
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

        #region Get All Tour_Reviews
        public List<Tour_Review> Get()
        {
            try
            {
                var data = db.Tour_Reviews.ToList();
                return (data.Count > 0) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get All Tour_Reviews

        #region Get Single Tour_Review
        public Tour_Review Get(int id)
        {
            try
            {
                var data = db.Tour_Reviews.Find(id);
                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get Single Tour_Review

        #region Update
        public bool Update(Tour_Review obj)
        {
            try
            {
                var data = db.Tour_Reviews.Find(obj.Review_ID);
                if (data != null)
                {

                    data.Review_ID = obj.Review_ID;
                    data.Comment = obj.Comment;
                    data.Tour_ID = obj.Tour_ID;
                    data.Tourist_ID = obj.Tourist_ID;

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
