using lab15.Data;
using lab15.Models;
using lab15.ViewModels;
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
                .Include(d => d.Category)
                .Select(d => new DeviceViewModel(d))
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
                .Include(d => d.Manufacturer)
                .Include(d => d.Seller)
                .Include(d => d.Category)
                .FirstOrDefault(d => d.Id == Id);
            if(devices == null)
            {
                return NotFound();
            }
            var deviceView = new DeviceViewModel(devices);
            return View(deviceView);
        }
        
        public async Task<IActionResult> Create()
        {
            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
            ViewBag.Categories = await _dbContext.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeviceViewModel deviceModel)
        {

            if (ModelState.IsValid) 
            {
                var seller = _dbContext.Sellers.Include(s=> s.Devices).FirstOrDefault(s => s.Id == deviceModel.SellerId);
                if (seller.Devices.Any(d=>d.Name.ToLower() == deviceModel.Name.ToLower()))
                {
                    ViewData["ErrorMessage"] = $"This seller already has this product";
                    ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
                    ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
                    ViewBag.Categories = await _dbContext.Categories.ToListAsync();
                    return View(deviceModel);
                }
                var sellerDevices = _dbContext.Sellers
                    .Include(s => s.Devices)
                    .FirstOrDefault(s => s.Id == deviceModel.SellerId);
                var device = new Device(deviceModel);
                await _dbContext.Devices.AddAsync(device);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
            ViewBag.Categories = await _dbContext.Categories.ToListAsync();
            return View(deviceModel);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
            ViewBag.Categories = await _dbContext.Categories.ToListAsync();

            var device = _dbContext.Devices.FirstOrDefault(s => s.Id == Id);
            if (device != null)
            {
                var deviceModel = new DeviceViewModel(device);
                return View(deviceModel);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int Id, DeviceViewModel deviceModel)
        {
            if (Id != deviceModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var device = _dbContext.Devices.FirstOrDefault(d => d.Id == Id);
                if (device.Name != deviceModel.Name || device.SellerId != deviceModel.SellerId)
                {
                    var seller = _dbContext.Sellers.Include(s => s.Devices).FirstOrDefault(s => s.Id == deviceModel.SellerId);
                    if (seller.Devices.Any(d => d.Name.ToLower() == deviceModel.Name.ToLower()))
                    {
                        ViewData["ErrorMessage"] = $"This seller already has this product";
                        ViewBag.Sellers = await _dbContext.Sellers.ToListAsync();
                        ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
                        ViewBag.Categories = await _dbContext.Categories.ToListAsync();
                        return View(deviceModel);
                    }
                }
                device.UpdateFromViewModel(deviceModel);
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
            ViewBag.Categories = await _dbContext.Categories.ToListAsync();
            return View(deviceModel);
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
                .Include (d => d.Category)
                .FirstOrDefault(s => s.Id == id);
            if (device == null)
            {
                return NotFound();
            }
            var deviceModel = new DeviceViewModel(device);
            return View(deviceModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            var device = _dbContext.Devices.FirstOrDefault(s => s.Id == id);
            if (device != null)
            {
                _dbContext.Devices.Remove(device);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Buy(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var device = _dbContext.Devices
                .Include(d => d.Manufacturer)
                .Include(d => d.Seller)
                .Include(d => d.Category)
                .FirstOrDefault(d => d.Id == Id);
            if (device == null)
            {
                return NotFound();
            }
            var purchaseView = new PurchaseViewModel();
            purchaseView.DeviceView = new DeviceViewModel(device);
            purchaseView.DeviceId = device.Id;
            return View(purchaseView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(PurchaseViewModel purchaseViewModel)
        {
            purchaseViewModel.Id = 0;
            if (ModelState.IsValid)
            {
                var purchase = new Purchase(purchaseViewModel);

                var device = _dbContext.Devices
                    .FirstOrDefault(d=>d.Id == purchaseViewModel.DeviceId);
                if (device == null || device.Amount == 0)
                {
                    return NotFound();
                }
                device.Amount --;
                try
                {
                    await _dbContext.Purchase.AddAsync(purchase);
                    _dbContext.Devices.Update(device);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        private bool DeviceExists(int id)
        {
            return (_dbContext.Devices?.Any(item => item.Id == id)).GetValueOrDefault();
        }
    }
}
