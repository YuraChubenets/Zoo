using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using Zoo.DAL;
using Zoo.BLL.Entities;
using Zoo.DAL.Abstract;

namespace Zoo.WebUI.Controllers
{
       [Authorize(Roles = "Администратор, Руководитель, Исполнитель")]
        public class UserController : Controller
        {
           IRepository<User> userRepo;
           IRepository<Role> roleRepo;
           IRepository<Animal> animRepo;

           public UserController(IRepository<User> u)
           {
               userRepo = u;
           }
           public UserController(IRepository<Role> r)
           {
               roleRepo = r;
           }  
           public UserController(IRepository<Animal> a)
           {
               animRepo = a;
           } 
           
           public UserController()
            {
                this.userRepo = new ZooRepository<User>();
                this.roleRepo = new ZooRepository<Role>();
                this.animRepo = new ZooRepository<Animal>();
            }

            [HttpGet]
            public ActionResult Index()
            {
                var users = userRepo.GetAll.Include(u => u.Role).ToList();
                return View(users);
            }
      
            [HttpGet]
            [Authorize(Roles = "Администратор")]
            public ActionResult Create()
            {
                SelectList roles = new SelectList(roleRepo.GetAll, "Id", "Name");
                ViewBag.Roles = roles;
                return View();
            }

            [HttpPost]
            [Authorize(Roles = "Администратор")]
            public ActionResult Create(User user)
            {
                if (ModelState.IsValid)
                {
                   userRepo.Create(user);
                    return RedirectToAction("Index");
                }

                SelectList roles = new SelectList(roleRepo.GetAll, "Id", "Name");
                ViewBag.Roles = roles;
                return View(user);
            }

            [HttpGet]
            [Authorize(Roles = "Администратор")]
            public ActionResult Edit(int id)
            {
                var user = userRepo.GetOne(id);
                SelectList roles = new SelectList(roleRepo.GetAll, "Id", "Name", user.RoleId);
                ViewBag.Roles = roles;
                return View(user);
            }

            [HttpPost]
            [Authorize(Roles = "Администратор")]
            public ActionResult Edit(User user)
            {
                if (ModelState.IsValid)
                {
                    userRepo.Update(user);
                    return RedirectToAction("Index");
                }

 
                SelectList roles = new SelectList(roleRepo.GetAll, "Id", "Name");
                ViewBag.Roles = roles;
                return View(user);
            }

            [Authorize(Roles = "Администратор")]
            public ActionResult Delete(int id)
            {
                userRepo.Delete(id);
                return RedirectToAction("Index");
            }
        }
    }
