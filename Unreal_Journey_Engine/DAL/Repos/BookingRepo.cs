using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class BookingRepo : Repo, IRepo<Booking, int, bool>
    {

        #region C R U D Operation

        #region Create
        public bool Create(Booking obj)
        {
            try
            {
                if (obj != null)
                {
                    db.Bookings.Add(obj);
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
                var data = db.Bookings.Find(id);
                if (data != null)
                {
                    db.Bookings.Remove(data);
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

        #region Get All Bookings
        public List<Booking> Get()
        {
            try
            {
                var data = db.Bookings.ToList();
                return (data.Count > 0) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get All Bookings

        #region Get Single Booking
        public Booking Get(int id)
        {
            try
            {
                var data = db.Bookings.Find(id);
                return (data != null) ? data : null;
            }
            catch (Exception ex)
            {
                Print_in_Red("Error = " + ex.Message);
                return null;
            }
        }
        #endregion Get Single Booking

        #region Update
        public bool Update(Booking obj)
        {
            try
            {
                var data = db.Bookings.Find(obj.Booking_ID);
                if (data != null)
                {
                    
                    data.Booking_ID = obj.Booking_ID;
                    data.Tour_Status = obj.Tour_Status;
                    data.Date = obj.Date;
                    data.Payment_Status = obj.Payment_Status;
                    data.Tourist_ID = obj.Tourist_ID;
                    data.Tour_ID = obj.Tour_ID;

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
