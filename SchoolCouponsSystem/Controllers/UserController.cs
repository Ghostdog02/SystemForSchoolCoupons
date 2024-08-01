using Microsoft.AspNetCore.Mvc;

namespace SystemForSchoolCoupons.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
