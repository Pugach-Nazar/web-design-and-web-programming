using lab15.Data;
using lab15.Models;
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
                .ToList();

            return View(devices);
        }

        public async Task<IActionResult> Details(int? Id)
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
            //foreach (var device in seller.Devices)
            //{
            //    device.Manufacturer = _dbContext.Devices
            //        .Include(d => d.Manufacturer)
            //        .FirstOrDefault(device => device.Id == Id).Manufacturer;
            //}

            ViewBag.Manufacturers = await _dbContext.Manufacturers.ToListAsync();
            return View(seller);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            if (ModelState.IsValid)
            {
                _dbContext.Sellers.Add(seller);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seller);
        }

        public IActionResult Edit(int? Id)
        {
            if(Id == null) 
            {
                return NotFound();
            }

            var seller = _dbContext.Sellers.FirstOrDefault(s => s.Id == Id);
            return View(seller);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int Id, Seller seller)
        {
            if (Id != seller.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
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
            return View(seller);
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
            return View(seller);
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
    }
}
    