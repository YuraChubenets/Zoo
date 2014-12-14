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
using Zoo.WebUI.helpers;
using System.Threading.Tasks;

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

        public ViewResult Index()
        {
            return View();
        }

        public ActionResult GetAnimals(string date, int? page)
        {
            ViewBag.Date = date;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            if (date != null)
            {
                var anim = animalRepo.GetAll
                   .Include(p => p.Gender)
                   .Include(p => p.Department)
                   .Include(p => p.User)
                   .Include(p => p.Feeding)
                   .Include(t => t.Lifecycles)
                   .AsEnumerable().Where(u => (((DateTime.Parse(date) - u.Lifecycles.EnteredOrBorn).Days % u.Feeding.Count) == 0)).ToList();

                return  View("_GetAnimals", anim.ToPagedList(pageNumber, pageSize));
            }

            var animals = animalRepo.GetAll
                    .Include(p => p.Gender)
                    .Include(p => p.Department)
                    .Include(p => p.User)
                    .Include(p => p.Feeding)
                    .Include(t => t.Lifecycles)
                    .AsEnumerable()
                    .Where(u => (((DateTime.UtcNow - u.Lifecycles.EnteredOrBorn).Days % u.Feeding.Count) == 0));

            return View("_GetAnimals", animals.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult AllAnimals()
        {
            return View();
        }
   
        public ActionResult ListAnimals(int? page, string text)
        {
           int pageSize = 5;
            int pageNumber = (page ?? 1);
            var animals = animalRepo.GetAll.ToList();

            if (text != null)
            {
                var anim = animals.Where(a => a.KindOfAnimal.ToUpper().Contains(text.ToUpper()) ||
                                              a.DiscriptionFeed.ToUpper().Contains(text.ToUpper()));
                return View(anim.ToPagedList(pageNumber, pageSize));

            }
            return View(animals.ToPagedList(pageNumber, pageSize));

        }

     
        // GET: /Animal/Details/5
        public async Task <ActionResult> Details(int id)
        {
            var anim = await animalRepo.GetOne(id);
            
             return PartialView("_Details", anim);
        }

        // GET: /Animal/Create
        public ActionResult Create()
        {
            ViewBag.Genders = new SelectList(genderRepo.GetAll, "Id", "Name");
            ViewBag.Departments = new SelectList(depatrtRepo.GetAll, "Id", "Name");
            ViewBag.Feedings = new SelectList(feedingRepo.GetAll, "Id", "NameFeeding");
          
            return PartialView("_Create");
        }

        // POST: /Animal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Animal anim)
        {
            var now = DateTime.UtcNow;
            var life = new Lifecycle() { EnteredOrBorn = now };
            await lifecycleRepo.Create(life);
            anim.LifecycleId = life.Id;
            anim.UserId = await userRepo.GetAll.Where(u => u.Login == User.Identity.Name).Select(a => a.Id).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                await animalRepo.Create(anim);
                return RedirectToAction("AllAnimals");
            }
            return View("AllAnimals");
        }
       
        
        // GET: /Animal/Edit/5
        public async Task <ActionResult> Edit(int id)
        {
            ViewBag.Genders = new SelectList(genderRepo.GetAll, "Id", "Name");
            ViewBag.Departments = new SelectList(depatrtRepo.GetAll, "Id", "Name");
            ViewBag.Feedings = new SelectList(feedingRepo.GetAll, "Id", "NameFeeding");
            ViewBag.Users = new SelectList(userRepo.GetAll, "Id", "Name");
   
            var anim = await animalRepo.GetOne(id);
            if (anim!=null)
            {
                return PartialView("_Edit",anim);
            }
            return View("AllAnimals");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Animal animals)
        {
            animalRepo.Update(animals, animals.Id );
            return RedirectToAction("AllAnimals", "Animal");
        }

        public async Task<ActionResult> Delete(int id)
        {
          var anim= await animalRepo.GetOne(id);
          if (anim != null)
          {
              return  PartialView("_Delete", anim);
          }
            return View("AllAnimals");
        }
        // POST: /Animal/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Delete(int id, Animal animal)
        {
          var anim = await animalRepo.GetOne(id);
          anim.Lifecycles.TransferredOrDied = DateTime.UtcNow;
          await  animalRepo.Delete(id);
          return RedirectToAction("AllAnimals", "Animal");
        }
    }
}
