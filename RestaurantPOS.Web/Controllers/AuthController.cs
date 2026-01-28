using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.Service.Interfaces;
using RestaurantPOS.Web.Infrastructure;

namespace RestaurantPOS.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IWaiterService _waiterService;

        public AuthController(IWaiterService waiterService)
        {
            _waiterService = waiterService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, go to POS
            var waiterId = HttpContext.Session.GetString(SessionKeys.WaiterId);
            if (!string.IsNullOrWhiteSpace(waiterId))
            {
                return RedirectToAction("Index", "Pos");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
            {
                TempData["Error"] = "Enter your PIN.";
                return View();
            }

            var waiter = _waiterService.LoginWithPin(pin);
            if (waiter == null)
            {
                TempData["Error"] = "Invalid PIN.";
                return View();
            }

            HttpContext.Session.SetString(SessionKeys.WaiterId, waiter.Id.ToString());
            HttpContext.Session.SetString(SessionKeys.WaiterName, waiter.FullName);

            return RedirectToAction("Index", "Pos");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}