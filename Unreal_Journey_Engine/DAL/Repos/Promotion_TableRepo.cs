using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class Promotion_TableRepo : Repo, IRepo<Promotion_Table, int, bool>
    {
        #region C R U D Operation

        #region Create
        public bool Create(Promotion_Table obj)
        {
            try
            {
                if (obj != null)
                {
                    db.Promotion_Tables.Add(obj);
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
                var data = db.Promotion_Tables.Find(id);
                if (data != null)
                {
                    db.Promotion_Tables.Remove(data);
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

        #region Get All Promotion_Tables
        public List<Promotion_Table> Get()
        {
            try
            {
                var data = db.Promotion_Tables.ToList();
                return (data.Count > 0) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get All Promotion_Tables

        #region Get Single Promotion_Table
        public Promotion_Table Get(int id)
        {
            try
            {
                var data = db.Promotion_Tables.Find(id);
                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get Single Promotion_Table

        #region Update
        public bool Update(Promotion_Table obj)
        {
            try
            {
                var data = db.Promotion_Tables.Find(obj.Promotion_ID);
                if (data != null)
                {
                    data.Title = obj.Title;
                    data.Discription = obj.Discription;
                    data.Amount = obj.Amount;
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
