﻿using lab15.Data;
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

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var seller = _dbContext.Sellers
                .Include(s => s.Devices)
                .FirstOrDefault(d => d.Id == Id);
            
            if (seller == null)
            {
                return NotFound();
            }
            foreach (var device in seller.Devices)
            {
                device.Manufacturer = _dbContext.Devices
                    .Include(d => d.Manufacturer)
                    .FirstOrDefault(device => device.Id == Id).Manufacturer;
            }
            return View(seller);
        }
    }
}
