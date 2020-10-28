using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using VariousTests.DAL.Entities;

namespace VariousTests.DAL.EF
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(string connection) : base(connection)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}
