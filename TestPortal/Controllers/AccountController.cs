using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestPortal.BL;
using TestPortal.Models;

namespace TestPortal.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_userService.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
            }
            else
            {
                ModelState.AddModelError("", "Invalid user name or password.");
                return View(model);
            }

            return RedirectToAction("Index", "Stories");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}