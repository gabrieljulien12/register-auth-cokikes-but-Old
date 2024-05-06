using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace register_auth_cokikes.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
