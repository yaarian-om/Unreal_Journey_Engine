using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class Tour_PackageRepo : Repo, IRepo<Tour_Package, int, bool>
    {

        #region C R U D Operation

        #region Create
        public bool Create(Tour_Package obj)
        {
            try
            {
                if(obj != null)
                {
                    db.Tour_Packages.Add(obj);
                    return db.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                Print_in_Red("Error = "+ex.Message);
                return false;
            }
        }
        #endregion Create

        #region Delete
        public bool Delete(int id)
        {
            try
            {
                var data = db.Tour_Packages.Find(id);
                if(data != null)
                {
                    db.Tour_Packages.Remove(data);
                    return db.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return false;
            }
        }
        #endregion Delete

        #region Get All Tour_Packages
        public List<Tour_Package> Get()
        {
            try
            {
                var data = db.Tour_Packages.ToList();
                return (data.Count > 0) ? data : null;
            }catch(Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get All Tour_Packages

        #region Get Single Tour_Package
        public Tour_Package Get(int id)
        {
            try
            {
                var data = db.Tour_Packages.Find(id);
                return (data != null) ? data : null;
            }catch(Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get Single Tour_Package

        #region Update
        public bool Update(Tour_Package obj)
        {
            try
            {
                var data = db.Tour_Packages.Find(obj.Tour_ID);
                if (data != null)
                {
                    var currentTime = DateTime.Now;
                    if (data.Image == null || data.Image.Equals("") || data.Image.Equals("temp.svg"))
                    {
                        // This Means User Didn't Uploaded the image,
                        // Just Make the Previous One
                    }
                    else
                    {
                        data.Image = (currentTime.ToString()) + obj.Image;
                    }

                    data.Tour_ID = obj.Tour_ID;
                    data.Title = obj.Title;
                    data.Location = obj.Location;
                    data.Duration = obj.Duration;
                    data.Cost = obj.Cost;
                    data.Tour_Guide_ID = obj.Tour_Guide_ID;

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
