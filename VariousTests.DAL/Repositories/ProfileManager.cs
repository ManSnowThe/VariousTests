using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.DAL.Interfaces;
using VariousTests.DAL.EF;
using VariousTests.DAL.Entities;

namespace VariousTests.DAL.Repositories
{
    public class ProfileManager : IProfileManager
    {
        public ApplicationContext Database { get; set; }

        public ProfileManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(UserProfile item)
        {
            Database.UserProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
