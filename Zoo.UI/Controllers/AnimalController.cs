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

namespace Zoo.UI.Controllers
{
    [Authorize(Roles = "Администратор, Руководитель, Исполнитель")]
    public class AnimalController : Controller
    {
        //
        // GET: /Animal/
        IRepository<Animal> animalRepo;
        public AnimalController()
        {
             this.animalRepo =  new ZooRepository<Animal>();
        }

        public ActionResult Index()
        {
            var anim = animalRepo.GetAll.Include(p => p.Gender).Include(p => p.Department).Include(p => p.User).Include(p => p.Feeding).Include(t=> t.Lifecycles).ToList();
            return View(anim);
        }

        //
        // GET: /Animal/Details/5

        public ActionResult Details(int id)
        {

            return View();
        }

        //
        // GET: /Animal/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Animal/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Animal/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Animal/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Animal/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Animal/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
