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
using Microsoft.AspNet.Identity;
using VariousTests.WEB.Filters;

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

        [AlreadyAuthorize]
        public ActionResult Login()
        {
            return PartialView();
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

        [AlreadyAuthorize]
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
                    var code = await AccountService.GetCodeEmail(userDto);
                    var callback = Url.Action("ConfirmEmail", "Account", new { userId = AccountService.GetUserId(userDto), code = code }, protocol: Request.Url.Scheme);
                    await AccountService.GetCallback(userDto, callback);

                    return View("DisplayEmail");
                }
                else
                {
                    ModelState.AddModelError(details.Property, details.Message);
                }
            }
            return View(model);
        }

        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            IdentityResult result = await AccountService.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}