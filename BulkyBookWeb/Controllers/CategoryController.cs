using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryist = _db.Categories.ToList();
            return View(objCategoryist);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            //Here we are checking for custom validations,key should be unique and wecould add labek names and erros can be summerize
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order should not be equal");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Category added successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        
        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null && id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.FirstOrDefault(c => c.Id == id); // we can use firstOrDefualt,singleOrdefault,Find,Single,
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            //Here we are checking for custom validations,key should be unique and wecould add labek names and erros can be summerize
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order should not be equal");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["Success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.FirstOrDefault(c => c.Id == id); // we can use firstOrDefualt,singleOrdefault,Find,Single,
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
