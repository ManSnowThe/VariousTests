using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.BLL.Infrastructure;
using VariousTests.BLL.DTO;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace VariousTests.BLL.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<Details> Register(UserDTO userDto);
        Task<ClaimsIdentity> Login(UserDTO userDto);
        Task<string> GetCodeEmail(UserDTO userDto);
        Task GetCallback(UserDTO userDto, string callback);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);
        string GetUserId(UserDTO userDto);
    }
}
