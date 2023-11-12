using lab15.Data;
using lab15.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab15.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DeviceController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var devices = _dbContext.Devices
                .Include(d => d.Manufacturer)
                .Include(d => d.Seller)
                .ToList();
            return View(devices);
        }

        public IActionResult Details(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            var devices = _dbContext.Devices
                .Include (d => d.Manufacturer)
                .Include(d => d.Seller)
                .FirstOrDefault(d => d.Id == Id);
            if(devices == null)
            {
                return NotFound();
            }
            return View(devices);
        }
        
        public async Task<IActionResult> Create()
        {
            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device)
        {
            if (ModelState.IsValid) 
            {
                //device.Seller = _dbContext.Sellers.FirstOrDefault(s => s.Id == device.SellerId);
                //device.Manufacturer = _dbContext.Manufacturers.FirstOrDefault(m => m.Id == device.ManufacturerId);
                _dbContext.Devices.AddAsync(device);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
            return View(device);
        }
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();

            var device = _dbContext.Devices.FirstOrDefault(s => s.Id == Id);
            return View(device);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int Id, Device device)
        {
            if (Id != device.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Devices.Update(device);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
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
            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
            return View(device);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var device = _dbContext.Devices
                .Include(d => d.Manufacturer)
                .Include(d => d.Seller)
                .FirstOrDefault(s => s.Id == id);
            if (device == null)
            {
                return NotFound();
            }
            return View(device);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            var device = _dbContext.Devices.FirstOrDefault(s => s.Id == id);
            if (device != null)
            {
                _dbContext.Devices.Remove(device);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool DeviceExists(int id)
        {
            return (_dbContext.Devices?.Any(item => item.Id == id)).GetValueOrDefault();
        }
    }
}
