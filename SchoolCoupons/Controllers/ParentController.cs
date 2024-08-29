using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolCoupons.Controllers
{
    [Authorize(Roles = "Admin,Parent")]
    public class ParentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
