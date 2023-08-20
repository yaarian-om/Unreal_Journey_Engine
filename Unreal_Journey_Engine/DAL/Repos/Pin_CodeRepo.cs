using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class Pin_CodeRepo : Repo, IRepo<Pin_Code, int, bool>
    {
        #region Create
        public bool Create(Pin_Code obj)
        {
            try
            {
                if (obj != null)
                {
                    var old_data = db.Pin_Codes.FirstOrDefault(u => u.User_ID == obj.User_ID);
                    if(old_data != null)
                    {
                        old_data.Pin = obj.Pin;
                    }
                    else
                    {
                        db.Pin_Codes.Add(obj);
                    }
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
                var data = db.Pin_Codes.Find(id);
                if (data != null)
                {
                    db.Pin_Codes.Remove(data);
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

        #region Get All Pin_Codes
        public List<Pin_Code> Get()
        {
            try
            {
                var data = db.Pin_Codes.ToList();
                return (data.Count > 0) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get All Pin_Codes

        #region Get Single Pin_Code
        public Pin_Code Get(int id)
        {
            try
            {
                var data = db.Pin_Codes.Find(id);
                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get Single Pin_Code

        #region Update
        public bool Update(Pin_Code old_pin_info)
        {
            try
            {
                var data = db.Pin_Codes.FirstOrDefault(u => u.Pin == old_pin_info.Pin);
                if (data != null)
                {
                    data.Pin = null;
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
