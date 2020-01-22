using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarFleetOs.Database.App;
using StarFleetOs.Database.Tenants.Models;
using StarFleetOs.Model;

namespace StarFleetOs.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppTenant appTenant;
        private readonly AppDbContext db;

        public HomeController(AppTenant appTenant, AppDbContext db)
        {
            this.appTenant = appTenant;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                ShipName = appTenant.Name,
                ShipIdentifier = appTenant.Identifier,
                Image = appTenant.Identifier switch
                {
                    "NCC-1701" => "enterprise-lcars.png",
                    "NCC-74656" => "voyager-lcars.jpg",
                    "NX-74205" => "defiant-lcars.jpg",
                    _ => "shipyard.png",
                },
                CrewMembers = await db.CrewMembers.ToListAsync()
            };

            return View(model);
        }
    }
}