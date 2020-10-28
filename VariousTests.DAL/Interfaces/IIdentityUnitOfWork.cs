using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.DAL.Interfaces;
using VariousTests.DAL.Identity;

namespace VariousTests.DAL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        AppUserManager UserManager { get; }
        AppRoleManager RoleManager { get; }
        IProfileManager ProfileManager { get; }
        Task SaveAsync();
    }
}
