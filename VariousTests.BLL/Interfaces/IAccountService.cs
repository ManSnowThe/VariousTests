using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariousTests.BLL.Infrastructure;
using VariousTests.BLL.DTO;
using System.Security.Claims;

namespace VariousTests.BLL.Interfaces
{
    public interface IAccountService : IDisposable
    {
        Task<Details> Register(UserDTO userDto);
        Task<ClaimsIdentity> Login(UserDTO userDto);
        //Удалить
        //Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
