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
        public IActionResult Create(Device device)
        {
            if (device != null) 
            {
                _dbContext.Devices.AddAsync(device);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(device);
        }
    }
}
