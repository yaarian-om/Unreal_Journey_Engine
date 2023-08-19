using DAL.Database.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RepoAccessFactory
    {
        public static IRepo<Tourist_Profile, int, bool> Tourist_Profile_Repo_Access()
        {
            return new Tourist_ProfileRepo();
        }

        public static I_image<Tourist_Profile, byte[],string, bool> Tourist_Profile_Image_Repo_Access()
        {
            return new Tourist_ProfileRepo();
        }

        public static IRepo<Tour_Package, int, bool> Tour_Package_Repo_Access()
        {
            return new Tour_PackageRepo();
        }

        public static IRepo<Admin_Profile, int, bool> Admin_Profile_Repo_Access()
        {
            return new Admin_ProfileRepo();
        }

        public static IRepo<Promotion_Table, int, bool> Promotion_Table_Repo_Access()
        {
            return new Promotion_TableRepo();
        }

        public static IRepo<User, int, bool> User_Repo_Access()
        {
            return new UserRepo();
        }

        public static IRepo<Tour_Review, int, bool> Tour_Review_Repo_Access()
        {
            return new Tour_ReviewRepo();
        }

        public static IRepo<Feedback, int, bool> Feedback_Repo_Access()
        {
            return new FeedbackRepo();
        }

        public static IRepo<Booking, int, bool> Booking_Repo_Access()
        {
            return new BookingRepo();
        }

        public static IRepo<User, int, bool> UserDataAccess()
        {
            return new UserRepo();
        }
        public static IRepo<Token, int, Token> TokenDataAccess()
        {
            return new TokenRepo();
        }
        public static IAuth AuthDataAccess()
        {
            return new UserRepo();
        }

    }
}
