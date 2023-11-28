using lab15.Data;
using lab15.Models;
using lab15.ViewModels;
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
            var devicesModel = _dbContext.Manufacturers
                .Select(m => new ManufacturerViewModel(m))
                .ToList();

            return View(devicesModel);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var manufacturer = _dbContext.Manufacturers
                .Include(m => m.Devices)
                .FirstOrDefault(m => m.Id == Id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            var devicesFromDb = _dbContext.Devices
                .Include(d => d.Seller)
                .Include(d => d.Category);

            foreach (var device in manufacturer.Devices)
            {
                var deviceFromDb = devicesFromDb.FirstOrDefault(d => d.Id == device.Id);
                device.Seller = deviceFromDb.Seller;
                device.Category = deviceFromDb.Category;
            }
            var manufacturerModel = new ManufacturerViewModel(manufacturer);
            return View(manufacturerModel);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ManufacturerViewModel manufacturerModel)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Manufacturers.Any(c => c.Name.ToLower() == manufacturerModel.Name.ToLower()))
                {
                    ViewData["ErrorMessage"] = $"Manufacturer {manufacturerModel.Name} already exist";
                    return View(manufacturerModel);
                }
                var manufacturer = new Manufacturer(manufacturerModel);
                _dbContext.Manufacturers.Add(manufacturer);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturerModel);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var manufacturer = _dbContext.Manufacturers.FirstOrDefault(s => s.Id == Id);
            if (manufacturer == null)
                return NotFound();
            var manufacturerModel = new ManufacturerViewModel(manufacturer);
            return View(manufacturerModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int Id, ManufacturerViewModel manufacturerModel)
        {
            if (Id != manufacturerModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var manufacturer = _dbContext.Manufacturers.FirstOrDefault(m => m.Id == Id);
                if(manufacturer.Name != manufacturerModel.Name)
                {
                    if (_dbContext.Manufacturers.Any(c => c.Name.ToLower() == manufacturerModel.Name.ToLower()))
                    {
                        ViewData["ErrorMessage"] = $"Manufacturer {manufacturerModel.Name} already exist";
                        return View(manufacturerModel);
                    }
                }
                manufacturer.UpdateFromViewModel(manufacturerModel);
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
            return View(manufacturerModel);
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
            var manufacturerModel = new ManufacturerViewModel(manufacturer);
            return View(manufacturerModel);
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
