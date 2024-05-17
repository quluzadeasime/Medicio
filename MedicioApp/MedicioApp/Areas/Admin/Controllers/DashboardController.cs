using Data.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MedicioApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
