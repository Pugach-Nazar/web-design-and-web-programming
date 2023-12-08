using lab15.Data;
using lab15.Models;
using lab15.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab15.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var devices = _dbContext.Categories
                .Select(c => new CategoryViewModel(c))
                .ToList();

            return View(devices);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var category = _dbContext.Categories
                .Include(c => c.Devices)
                .FirstOrDefault(c => c.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            var devicesFromBb = _dbContext.Devices
                    .Include(d => d.Manufacturer)
                    .Include(d => d.Seller);

            foreach (var device in category.Devices)
            {
                var deviceFromBd = devicesFromBb.FirstOrDefault(d => d.Id == device.Id);
                device.Manufacturer = deviceFromBd.Manufacturer;
                device.Seller = deviceFromBd.Seller;
            }

            var categoryModel = new CategoryViewModel(category);

            return View(categoryModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Categories.Any(c => c.Name.ToLower() == categoryModel.Name.ToLower()))
                {
                    ViewData["ErrorMessage"] = $"Category {categoryModel.Name} already exist";
                    return View(categoryModel);
                }
                var category = new Category(categoryModel);
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryModel);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var category = _dbContext.Categories.FirstOrDefault(s => s.Id == Id);
            if (category == null) 
                return NotFound();
            var categoryModel = new CategoryViewModel(category);
            return View(categoryModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int Id, CategoryViewModel categoryModel)
        {
            if (Id != categoryModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (_dbContext.Categories.Any(c => c.Name.ToLower() == categoryModel.Name.ToLower()))
                {
                    ViewData["ErrorMessage"] = $"Category {categoryModel.Name} already exist";
                    return View(categoryModel);
                }

                var category = new Category(categoryModel);
                try
                {
                    _dbContext.Categories.Update(category);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(categoryModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _dbContext.Categories
                .Include(с => с.Devices)
                .FirstOrDefault(с => с.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryModel = new CategoryViewModel(category);
            return View(categoryModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }


        private bool CategoryExists(int id)
        {
            return (_dbContext.Categories?.Any(item => item.Id == id)).GetValueOrDefault();
        }

        public IActionResult IsNameUnique(string name)
        {
            
            if (_dbContext.Categories.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}
