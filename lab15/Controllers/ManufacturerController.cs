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
                .Include(s => s.Devices)
                .ToList();

            return View(devices);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var manufacturer = _dbContext.Manufacturers
                .Include(s => s.Devices)
                .FirstOrDefault(d => d.Id == Id);

            if (manufacturer == null)
            {
                return NotFound();
            }
            foreach (var device in manufacturer.Devices)
            {
                device.Seller = _dbContext.Devices
                    .Include(d => d.Seller)
                    .FirstOrDefault(device => device.Id == Id).Seller;
            }
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
            if (manufacturer != null /*&& manufacturer.Name != null && manufacturer.Description != null && manufacturer.Country != null*/)
            {
                _dbContext.Manufacturers.Add(manufacturer);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }
    }
}
