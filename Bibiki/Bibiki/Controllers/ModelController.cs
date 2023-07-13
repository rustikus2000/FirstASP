using Bibiki.Data;
using Bibiki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bibiki.Controllers
{
    public class ModelController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ModelController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            IEnumerable<Model> objModelList = from s in  _db.Models.Include(m => m.Brand) select s;
            
            switch (sortOrder)
            {
                case "name_desc":
                    objModelList = objModelList.OrderByDescending(s => s.Name);
                    break;
                default:
                    objModelList = objModelList.OrderBy(s => s.Name);
                    break;
            }
            return View(objModelList.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Brand = new SelectList(_db.Brands, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Model obj)
        {
            if (ModelState.IsValid) 
            {
                _db.Models.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Brand = new SelectList(_db.Brands, "Id", "Name");
            if (id == null || id == 0) 
            { 
                return NotFound();
            }
            var modelFromDb = _db.Models.Find(id);

            if (modelFromDb == null) 
            {
                return NotFound();
            }
            return View(modelFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Model obj)
        {
            if (ModelState.IsValid)
            {
                _db.Models.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

       
    }
}
