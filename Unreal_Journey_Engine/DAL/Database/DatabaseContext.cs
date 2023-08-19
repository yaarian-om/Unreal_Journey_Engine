using DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tourist_Profile> Tourist_Profiles { get; set; }
        public DbSet<Tour_Package> Tour_Packages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Tour_Review> Tour_Reviews { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Admin_Profile> Admin_Profiles { get; set; }
        public DbSet<Promotion_Table> Promotion_Tables { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Pin_Code> Pin_Codes { get; set; }
        
    }
}
