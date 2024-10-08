﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.Sections.Data;
using SignalR.Sections.Hubs;
using SignalR.Sections.Models;

namespace SignalR.Sections.Controllers
{
    public class Section7Controller(ApplicationDbContext context, IHubContext<OrderUpdateHub> orderHub) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHubContext<OrderUpdateHub> _orderHub = orderHub;

        [ActionName("Order")]
        public IActionResult Order()
        {
            string[] name = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
            string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

            Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(name.Length);

            Order order = new Order()
            {
                Name = name[index],
                ItemName = itemName[index],
                Count = index
            };

            return View(order);
        }

        [ActionName("Order")]
        [HttpPost]
        public async Task<IActionResult> OrderPost(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await _orderHub.Clients.All.SendAsync("newOrder");
            return RedirectToAction(nameof(Order));
        }

        [ActionName("OrderList")]
        public IActionResult OrderList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var productList = await _context.Orders.ToListAsync();
            return Json(new { data = productList });
        }
    }
}
