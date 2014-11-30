using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
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
            var animals = animalRepo.GetAll
                .Include(p => p.Gender)
                .Include(p => p.Department)
                .Include(p => p.User)
                .Include(p => p.Feeding)
                .Include(t => t.Lifecycles)
                .ToList();
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
                    .Where(u => (((DateTime.Parse(date) - u.Lifecycles.EnteredOrBorn).Days % u.NumberFeeding) == 0));
                return PartialView(animals);
            }
            return null;
        }


        //
        // GET: /Animal/Details/5

        public ActionResult Details(int id)
        {
            var anim = animalRepo.GetOne(id);
            if (anim == null)
            {
                return View("Error");
            }
            return PartialView("_Details",anim);
        }

        // GET: /Animal/Create
        public ActionResult Create()
        {
            //ViewBag.Id_Login = new SelectList(db.Logins, "Id", "Login1", catalogfilm.Id_Login);
            return PartialView("_Create");
        }

        //
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
            return PartialView("_Create");
        }
       
        
        // GET: /Animal/Edit/5

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            var anim = animalRepo.GetOne(id);
            if (ModelState.IsValid)
            {
                animalRepo.Update(anim);
                return RedirectToAction("Index","Animal");
            }
            return PartialView("_Edit");
        }

    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {

                 animalRepo.Update(animalRepo.GetOne(id));
                return PartialView("GetAnimals");
            }
            return null;
        }
        public ActionResult Delete(int id=0)
        {
          var anim= animalRepo.GetOne(id);
          if (anim == null)
          {
              return PartialView("Error");
          }
            return  PartialView("_Delete");
        }
        // POST: /Animal/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
          animalRepo.Delete(id);
          return PartialView("_GetAnimals");
        }
    }
}
