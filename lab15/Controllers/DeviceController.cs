using lab15.Data;
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
            var devices = _dbContext.Devices.FirstOrDefault(d => d.Id == Id);
            if(devices == null)
            {
                return NotFound();
            }
            return View(devices);
        }
    }
}
