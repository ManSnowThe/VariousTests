using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VariousTests.BLL.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using VariousTests.BLL.DTO;
using VariousTests.WEB.Models;
using VariousTests.BLL.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;

namespace VariousTests.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService AccountService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IAccountService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await AccountService.Login(userDto);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RegisterViewModel, UserDTO>()
                .ForMember("Role", opt => opt.MapFrom(src => "user"))).CreateMapper();
                UserDTO userDto = mapper.Map<RegisterViewModel, UserDTO>(model);

                Details details = await AccountService.Register(userDto);
                if (details.Succeeded)
                {
                    // Изменить
                    return View("SuccessRegister");
                }
                else
                {
                    ModelState.AddModelError(details.Property, details.Message);
                }
            }
            return View(model);
        }

        // Удалить
        //private async Task SetInitialDataAsync()
        //{
        //    await AccountService.SetInitialData(new UserDTO
        //    {
        //        Email = "admin@mail.com",
        //        UserName = "admin",
        //        Password = "Pass123",
        //        Name = "Админ",
        //        Role = "admin"
        //    }, new List<string> { "user", "admin" });
        //}
    }
}