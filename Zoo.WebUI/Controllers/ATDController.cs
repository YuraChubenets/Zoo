using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zoo.BLL.Entities;
using Zoo.DAL.Abstract;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;

namespace Zoo.WebUI.Controllers
{
    public class ATDController : Controller
    {
        IRepository<ATD> atdRepo = new ZooRepository<ATD>();
        
      
       //AnimalTransorDie
        public ViewResult IndexAnimalsTrans()
        {
            return View();
        }


        // GET: /Animal/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var anim = await atdRepo.GetOne(id);
                return PartialView("_Details", anim);
        }

       

        public ActionResult ListAnimalsTrans(int? page)
        {
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            var animals = atdRepo.GetAll
                   .Include(p => p.Gender)
                   .Include(p => p.Department)
                   .Include(p => p.User)
                   .Include(p => p.Feeding)
                   .Include(t => t.Lifecycles)
                   .AsEnumerable().ToList();

            return View(animals.ToPagedList(pageNumber, pageSize));
        }

       
    }
}
