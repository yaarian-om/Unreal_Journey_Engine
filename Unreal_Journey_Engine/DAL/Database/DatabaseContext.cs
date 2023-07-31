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
    }
}
