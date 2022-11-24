using Microsoft.AspNetCore.Mvc;

namespace MovieRental.Controllers
{
    public class RentalsController : Controller
    {
        public IActionResult New()
        {
            return View();
        }
    }
}
