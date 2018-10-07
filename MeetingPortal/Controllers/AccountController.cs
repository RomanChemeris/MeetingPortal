using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MeetingPortal.App_Start;
using MeetingPortal.Core.ViewModels;
using MeetingPortal.DAL.Entities;
using MeetingPortal.DAL.Services;
using Microsoft.AspNet.Identity;

namespace MeetingPortal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager SignInManager;
        private ApplicationUserManager UserManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Неправильный логин или пароль");
                return View(model);
            }
            if (await UserManager.CheckPasswordAsync(user, model.Password))
            {
                await UserManager.ResetAccessFailedCountAsync(user.Id);
                await SignInManager.SignInAsync(user, true, true);
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "Неправильный логин или пароль");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}