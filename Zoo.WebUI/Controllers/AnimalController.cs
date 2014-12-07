using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Zoo.BLL.Entities;
using Zoo.DAL.Abstract;
using Zoo.WebUI.Models;
using PagedList.Mvc;
using PagedList;

namespace Zoo.WebUI.Controllers
{
    [Authorize(Roles = "Администратор, Руководитель, Исполнитель")]
    public class AnimalController : Controller
    {
        // GET: /Animal/
        IRepository<Animal> animalRepo;
        public AnimalController(  IRepository<Animal> a)
        {
            animalRepo = a;
        }
        public AnimalController()
        {
             this.animalRepo =  new ZooRepository<Animal>();
        }

        public ActionResult Index()
        {
            var animals = animalRepo.GetAll
                    .Include(p => p.Gender)
                    .Include(p => p.Department)
                    .Include(p => p.User)
                    .Include(p => p.Feeding)
                    .Include(t => t.Lifecycles)
                    .AsEnumerable()
                    .Where(u => (((DateTime.UtcNow - u.Lifecycles.EnteredOrBorn).Days % u.Feeding.Count) == 0));
            return View(animals);
        }


        public ActionResult GetAnimals(string date)
        {
            if (date != null)
            {
                var animals = animalRepo.GetAll
                    .Include(p => p.Gender)
                    .Include(p => p.Department)
                    .Include(p => p.User)
                    .Include(p => p.Feeding)
                    .Include(t => t.Lifecycles)
                    .AsEnumerable()
                    .Where(u => (((DateTime.Parse(date) - u.Lifecycles.EnteredOrBorn).Days % u.Feeding.Count) == 0));
             
                return PartialView("_GetAnimals", animals);
            }
            return View("Index");
        }

        public ActionResult AllAnimals(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var animals = animalRepo.GetAll.ToList();

            return View(animals.ToPagedList(pageNumber, pageSize));

        }
     
        // GET: /Animal/Details/5

        public ActionResult Details(int id)
        {
            var anim = animalRepo.GetOne(id);
            if (anim != null)
            {
                return PartialView("_Details", anim);
            }
            return View("Index");
        }

        // GET: /Animal/Create
        public ActionResult Create()
        {
            //ViewBag.Id_Login = new SelectList(db.Logins, "Id", "Login1", catalogfilm.Id_Login);
            return PartialView("_Create");
        }

        // POST: /Animal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Animal anim)
        {
            if (ModelState.IsValid)
            {
                animalRepo.Create(anim);
                return RedirectToAction("Index");
            }
            //ViewBag.Id_Login = new SelectList(db.Logins, "Id", "Login1", catalogfilm.Id_Login);
            return View("Index");
        }
       
        
        // GET: /Animal/Edit/5

        public ActionResult Edit(int id)
        {
            var anim = animalRepo.GetOne(id);
            if (anim!=null)
            {
                return PartialView("_Edit",anim);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Animal anim)
        {
                 animalRepo.Update(anim);
                 return RedirectToAction("Index", "Animal");
        }

        public ActionResult Delete(int id)
        {
          var anim= animalRepo.GetOne(id);
          if (anim != null)
          {
              return  PartialView("_Delete", anim);
          }
            return View("Index");
        }
        // POST: /Animal/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
          animalRepo.Delete(id);
          return RedirectToAction("Index", "Animal");
        }
    }
}
