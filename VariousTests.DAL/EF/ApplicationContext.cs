using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using VariousTests.DAL.Entities;
using System.Data.Entity.Infrastructure;

namespace VariousTests.DAL.EF
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(string connection) : base(connection)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<VarTest> Tests { get; set; }
        public DbSet<VarTopic> Topics { get; set; }
        public DbSet<VarQuestion> Questions { get; set; }
        public DbSet<VarAnswer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().HasMany(t => t.Tests)
                .WithMany(u => u.UserProfiles)
                .Map(m => m.MapLeftKey("UserId")
                .MapRightKey("TestId")
                .ToTable("UsersCompletedTest"));

            base.OnModelCreating(modelBuilder);
        }

        public class MigrationsContextFactory : IDbContextFactory<ApplicationContext>
        {
            public ApplicationContext Create()
            {
                return new ApplicationContext("AppConnection");
            }
        }
    }
}
