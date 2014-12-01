using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Zoo.BLL.Entities;
using Zoo.DAL.Abstract;
using Zoo.WebUI.Models;

namespace Zoo.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        IRepository<User> userRepo;
        public AccountController()
        {
            this.userRepo = new ZooRepository<User>();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Animal");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;
            try
            {
                var user = userRepo.GetAll.Where(u => u.Login == login && u.Password == password).FirstOrDefault();

                if (user != null)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
                ModelState.AddModelError("", "Поризошла ошибка-" + ex);
            }
            return isValid;
        }

    }

}