using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.DAL.Interfaces;
using VariousTests.DAL.EF;
using VariousTests.DAL.Entities;
using VariousTests.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace VariousTests.DAL.Repositories
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private ApplicationContext db;
        public AppUserManager UserManager { get; }
        public AppRoleManager RoleManager { get; }
        public IProfileManager ProfileManager { get; }

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            UserManager = new AppUserManager(new UserStore<AppUser>(db));
            RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));
            ProfileManager = new ProfileManager(db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    ProfileManager.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
