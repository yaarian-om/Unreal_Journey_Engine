using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class UserRepo : Repo, IRepo<User, int, bool>
    {

        #region C R U D Operation

        #region Create
        public bool Create(User obj)
        {
            try
            {
                if (obj != null)
                {
                    db.Users.Add(obj);
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
                var data = db.Users.Find(id);
                if (data != null)
                {
                    db.Users.Remove(data);
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

        #region Get All Users
        public List<User> Get()
        {
            try
            {
                var data = db.Users.ToList();
                return (data.Count > 0) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get All Users

        #region Get Single User
        public User Get(int id)
        {
            try
            {
                var data = db.Users.Find(id);
                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get Single User

        #region Update
        public bool Update(User obj)
        {
            try
            {
                var data = db.Users.Find(obj.User_ID);
                if (data != null)
                {
                   

                    data.User_ID = obj.User_ID;
                    data.Email = obj.Email;
                    data.Password = obj.Password;
                    data.Role = obj.Role;
                    

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
