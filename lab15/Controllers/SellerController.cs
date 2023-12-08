using lab15.Data;
using lab15.Models;
using lab15.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab15.Controllers
{
    public class SellerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SellerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var devices = _dbContext.Sellers
                .Include(s => s.Devices)
                .Select(s => new SellerViewModel(s))
                .ToList();

            return View(devices);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var seller = _dbContext.Sellers
                .Include(s => s.Devices)
                .FirstOrDefault(s => s.Id == Id);
            
            if (seller == null)
            {
                return NotFound();
            }
            var devicesFromDb = _dbContext.Devices
                .Include(d => d.Manufacturer)
                .Include(d => d.Category);

            foreach (var device in seller.Devices)
            {
                var deviceFromDb = devicesFromDb.FirstOrDefault(d => d.Id == device.Id);
                device.Manufacturer = deviceFromDb.Manufacturer;
                device.Category = deviceFromDb.Category;
            }
            var sellseModel = new SellerViewModel(seller);
            return View(sellseModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SellerViewModel sellerModel)
        {
            if (ModelState.IsValid)
            {
                if (_dbContext.Sellers.Any(c => c.Name.ToLower() == sellerModel.Name.ToLower()))
                {
                    ViewData["ErrorMessage"] = $"Seller {sellerModel.Name} already exist";
                    return View(sellerModel);
                }
                var seller = new Seller(sellerModel);
                _dbContext.Sellers.Add(seller);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sellerModel);
        }

        public IActionResult Edit(int? Id)
        {
            if(Id == null) 
            {
                return NotFound();
            }

            var seller = _dbContext.Sellers.FirstOrDefault(s => s.Id == Id);
            if (seller == null)
                return NotFound();
            var sellerModel = new SellerViewModel(seller);
            return View(sellerModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int Id, SellerViewModel sellerModel)
        {
            if (Id != sellerModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var seller = _dbContext.Sellers.FirstOrDefault(s=>s.Id == Id);
                if (seller.Name == sellerModel.Name)
                {
                    if (_dbContext.Sellers.Any(c => c.Name.ToLower() == sellerModel.Name.ToLower()))
                    {
                        ViewData["ErrorMessage"] = $"Seller {sellerModel.Name} already exist";
                        return View(sellerModel);
                    }
                }
                seller = new Seller(sellerModel);
                try
                {
                    _dbContext.Sellers.Update(seller);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
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
            return View(sellerModel);
        }

        public IActionResult Delete(int? id) 
        {
            if(id == null)
            {
                return NotFound();
            }
            var seller = _dbContext.Sellers.Include(s => s.Devices)
                .FirstOrDefault(s => s.Id == id);
            if (seller == null)
            {
                return NotFound();
            }
            var sellerModel = new SellerViewModel(seller);
            return View(sellerModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(int id)
        {
            var seller = _dbContext.Sellers.FirstOrDefault(s => s.Id == id);
            if (seller != null)
            {
                _dbContext.Sellers.Remove(seller);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool SellerExists(int id)
        {
            return (_dbContext.Sellers?.Any(item => item.Id == id)).GetValueOrDefault();
        }

        public IActionResult IsNameUnique(string name)
        {

            if (_dbContext.Sellers.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}
    