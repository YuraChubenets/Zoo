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
        IRepository<Gender> genderRepo;
        IRepository<Department> depatrtRepo;
        IRepository<User> userRepo;
        IRepository<Feeding> feedingRepo;
        IRepository<Lifecycle> lifecycleRepo;



        public AnimalController(  IRepository<Animal> a)
        {
            animalRepo = a;
        }
        public AnimalController()
        {
            this.depatrtRepo = new ZooRepository<Department>(); 
            this.animalRepo =  new ZooRepository<Animal>();
            this.genderRepo = new ZooRepository<Gender>();
            this.userRepo = new ZooRepository<User>();
            this.feedingRepo = new ZooRepository<Feeding>();
            this.lifecycleRepo = new ZooRepository<Lifecycle>();
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
            ViewBag.Genders = new SelectList(genderRepo.GetAll, "Id", "Name");
            ViewBag.Departments = new SelectList(depatrtRepo.GetAll, "Id", "Name");
            ViewBag.Users = new SelectList(userRepo.GetAll, "Id", "Name");
            ViewBag.Feedings = new SelectList(feedingRepo.GetAll, "Id", "NameFeeding");
            ViewBag.Lifecycles = new SelectList(lifecycleRepo.GetAll, "Id", "EnteredOrBorn",);

           // ViewBag.Id_Login = new SelectList(db.Logins, "Id", "Login1", catalogfilm.Id_Login);

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
