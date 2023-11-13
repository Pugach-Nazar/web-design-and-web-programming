using lab15.Data;
using lab15.Models;
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
                .Include(c => c.Devices)
                .ToList();

            return View(devices);
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var seller = _dbContext.Categories
                .Include(c => c.Devices)
                .FirstOrDefault(c => c.Id == Id);

            if (seller == null)
            {
                return NotFound();
            }
            //foreach (var device in seller.Devices)
            //{
            //    device.Manufacturer = _dbContext.Devices
            //        .Include(d => d.Manufacturer)
            //        .FirstOrDefault(device => device.Id == Id).Manufacturer;
            //}

            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            return View(seller);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var category = _dbContext.Categories.FirstOrDefault(s => s.Id == Id);
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int Id, Category category)
        {
            if (Id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
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
            return View(category);
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
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool CategoryExists(int id)
        {
            return (_dbContext.Categories?.Any(item => item.Id == id)).GetValueOrDefault();
        }
    }
}
