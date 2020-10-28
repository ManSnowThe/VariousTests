using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using VariousTests.BLL.Infrastructure;
using VariousTests.BLL.DTO;
using VariousTests.BLL.Interfaces;
using VariousTests.DAL.Interfaces;
using VariousTests.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace VariousTests.BLL.Services
{
    public class AccountService : IAccountService
    {
        IIdentityUnitOfWork Database { get; set; }

        public AccountService(IIdentityUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<Details> Register(UserDTO userDto)
        {
            var user = await Database.UserManager.FindByEmailAsync(userDto.Email);

            if (user == null)
            {
                user = new AppUser { Email = userDto.Email, UserName = userDto.UserName };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Count() > 0)
                {
                    return new Details(false, result.Errors.FirstOrDefault(), "");
                }

                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);

                UserProfile userProfile = new UserProfile { Id = user.Id, UserName = userDto.UserName };
                Database.ProfileManager.Create(userProfile);
                await Database.SaveAsync();

                return new Details(true, "Регистрация прошла успешно", "");
            }
            else
            {
                return new Details(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Login(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            AppUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
