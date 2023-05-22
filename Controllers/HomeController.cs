using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioWebApp.DAL;
using StudioWebApp.Models;
using StudioWebApp.ViewModels;
using System.Diagnostics;

namespace StudioWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudioDbContext _studioDbContext;
        public HomeController(StudioDbContext studioDbContext)
        {
            _studioDbContext = studioDbContext;
        }

        public async Task< IActionResult> Index()
        {
            List<Team> teams = await _studioDbContext.Teams.Include(t=>t.Job).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Teams = teams,
            };
            return View(homeVM);
        }
    }
}