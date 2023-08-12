using DAL.Database.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
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

        public static IRepo<Tour_Package, int, bool> Tour_Package_Repo_Access()
        {
            return new Tour_PackageRepo();
        }

    }
}
