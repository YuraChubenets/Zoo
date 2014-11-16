using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zoo.DAL;
using Zoo.UI.Models;
using Zoo.DAL.Entities;
using System.Web.Security;

namespace Zoo.UI.Controllers
{
        [AllowAnonymous]
        public class AccountController : Controller
        {
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
                            return RedirectToAction("Index", "Request");
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

                using (ZooDbContext _db = new ZooDbContext())
                {
                    try
                    {
                        User user = (from u in _db.Users
                                     where u.Login == login && u.Password == password
                                     select u).FirstOrDefault();
                        
                        List<User> uu = _db.Users.ToList();

                        var dd = _db.Users.AsEnumerable().Select(h => h);

                        if (user != null)
                        {
                            isValid = true;
                        }
                    }
                    catch
                    {
                        isValid = false;
                    }
                }
                return isValid;
            }
        }

    }

