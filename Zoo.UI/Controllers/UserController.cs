using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Zoo.DAL;
using Zoo.DAL.Entities;

namespace Zoo.UI.Controllers
{
       [Authorize(Roles = "Администратор, Руководитель, Исполнитель")]
        public class UserController : Controller
        {
            private ZooDbContext db = new ZooDbContext();

            [HttpGet]
            public ActionResult Index()
            {
                var users = db.Users.Include(u => u.Role).ToList();
                return View(users);
            }
      
            [HttpGet]
            [Authorize(Roles = "Администратор")]
            public ActionResult Create()
            {
               
                SelectList roles = new SelectList(db.Roles, "Id", "Name");
                ViewBag.Roles = roles;
                return View();
            }

            [HttpPost]
            [Authorize(Roles = "Администратор")]
            public ActionResult Create(User user)
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
                SelectList roles = new SelectList(db.Roles, "Id", "Name");
                ViewBag.Roles = roles;
               return View(user);
            }

            [HttpGet]
            [Authorize(Roles = "Администратор")]
            public ActionResult Edit(int id)
            {
                User user = db.Users.Find(id);
                SelectList roles = new SelectList(db.Roles, "Id", "Name", user.RoleId);
                ViewBag.Roles = roles;
                return View(user);
            }

            [HttpPost]
            [Authorize(Roles = "Администратор")]
            public ActionResult Edit(User user)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                SelectList departments = new SelectList(db.Departments, "Id", "Name");
                ViewBag.Departments = departments;
                SelectList roles = new SelectList(db.Roles, "Id", "Name");
                ViewBag.Roles = roles;

                return View(user);
            }

            [Authorize(Roles = "Администратор")]
            public ActionResult Delete(int id)
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


        }
    }
