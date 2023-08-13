using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class Admin_ProfileRepo : Repo, IRepo<Admin_Profile, int, bool>
    {

        #region C R U D Operations

        #region Create
        public bool Create(Admin_Profile obj)
        {
            try
            {
                db.Admin_Profiles.Add(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Print_in_Red(ex.Message);
                return false;
            }
        }
        #endregion Create

        #region Delete
        public bool Delete(int id)
        {
            // Toruist Delete Means Delete the User First,
            // As I have not Created the User Repo, so am Deleting Tourist Data
            try
            {
                var admin = db.Admin_Profiles.Find(id);
                db.Admin_Profiles.Remove(admin);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Print_in_Red(ex.Message);
                return false;
            }
        }
        #endregion Delete

        #region Get All Data
        public List<Admin_Profile> Get()
        {
            try
            {
                var data = db.Admin_Profiles.ToList();
                //This is Called Tarnary If-Else. Don't use if you can't explain
                return (data.Count > 0) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red(ex.Message);
                return null;
            }
        }
        #endregion Get All Data

        #region Get Single Data
        public Admin_Profile Get(int id)
        {
            try
            {
                var data = db.Admin_Profiles.Find(id);
                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red(ex.Message);
                return null;
            }
        }
        #endregion Get Single Data

        #region Update 
        public bool Update(Admin_Profile obj)
        {
            try
            {
                var data = db.Admin_Profiles.Find(obj.Admin_ID);
                if (data != null)
                {


                    data.Admin_ID = obj.Admin_ID;
                    data.Name = obj.Name;
                    data.Phone = obj.Phone;
                    data.Image = obj.Image;

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

        #endregion C R U D Operations

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
