using Microsoft.AspNetCore.Mvc;

namespace SchoolCoupons.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
