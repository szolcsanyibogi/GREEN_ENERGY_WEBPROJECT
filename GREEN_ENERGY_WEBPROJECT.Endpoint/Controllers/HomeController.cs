using System.Diagnostics;
using GREEN_ENERGY_WEBPROJECT.Endpoint.Models;
using GREEN_ENERGY_WEBPROJECT.Endpoint.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GREEN_ENERGY_WEBPROJECT.Models;

namespace GREEN_ENERGY_WEBPROJECT.Endpoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var content = await _context.HomeContents.FirstOrDefaultAsync();
            ViewBag.ContentText = content?.Content ?? "Welcome to the site!";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveContent(string content)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin@green.com")
            {
                var existing = await _context.HomeContents.FirstOrDefaultAsync();
                if (existing == null)
                {
                    _context.HomeContents.Add(new HomeContent { Content = content });
                }
                else
                {
                    existing.Content = content;
                    _context.HomeContents.Update(existing);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
