using lab15.Data;
using lab15.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab15.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ManufacturerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var devices = _dbContext.Manufacturers
                .Include(m => m.Devices)
                .ToList();

            return View(devices);
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var manufacturer = _dbContext.Manufacturers
                .Include(m => m.Devices)
                .FirstOrDefault(d => d.Id == Id);

            if (manufacturer == null)
            {
                return NotFound();
            }
            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            ViewBag.Categories = await _dbContext.Categories.ToListAsync();
            return View(manufacturer);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Manufacturers.Add(manufacturer);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var manufacturer = _dbContext.Manufacturers.FirstOrDefault(s => s.Id == Id);
            return View(manufacturer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int Id, Manufacturer manufacturer)
        {
            if (Id != manufacturer.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Manufacturers.Update(manufacturer);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturerExists(manufacturer.Id))
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
            return View(manufacturer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var manufacturer = _dbContext.Manufacturers
                .Include(m => m.Devices)
                .FirstOrDefault(s => s.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return View(manufacturer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            var manufacturer = _dbContext.Manufacturers.FirstOrDefault(s => s.Id == id);
            if (manufacturer != null)
            {
                _dbContext.Manufacturers.Remove(manufacturer);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool ManufacturerExists(int id)
        {
            return (_dbContext.Manufacturers?.Any(item => item.Id == id)).GetValueOrDefault();
        }
    }
}
