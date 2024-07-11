using assignment3_991605014.Data;
using assignment3_991605014.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace assignment3_991605014.Controllers
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

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public async Task<IActionResult> Signup(User user, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "A user with this email address already exists.");
                    return View("Index");
                }

                if (user.Password != confirmPassword)
                {
                    ModelState.AddModelError("", "The password and confirmation password do not match.");
                    return View("Index");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View("Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", $"{user.FirstName} {user.LastName}");
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid login attempt";
                return View();
            }
        }

        public IActionResult Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        public async Task<IActionResult> Truck()
        {
            ViewBag.Trucks = await _context.Trucks.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTruck(Truck truck)
        {
            if (ModelState.IsValid)
            {
                _context.Trucks.Add(truck);
                await _context.SaveChangesAsync();
                return RedirectToAction("Truck");
            }
            ViewBag.Trucks = await _context.Trucks.ToListAsync();
            return View("Truck", truck);
        }


        public async Task<IActionResult> TransportRoute()
        {
            ViewBag.Routes = await _context.Routes.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTransportRoute(TransportRoute route)
        {
            if (ModelState.IsValid)
            {
                _context.Routes.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction("TransportRoute");
            }
            ViewBag.Routes = await _context.Routes.ToListAsync();
            return View("TransportRoute", route);
        }
        public async Task<IActionResult> TruckWorkshop()
        {
            ViewBag.TruckIDs = new SelectList(await _context.Trucks.ToListAsync(), "TruckId", "TruckNum");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // This will clear the session, effectively logging the user out
            return RedirectToAction("Index"); // Redirect to the home page
        }

    }
}

