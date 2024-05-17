using Data.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MedicioApp.Controllers
{
    public class HomeController : Controller
    {

        AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var doctors = _dbContext.Doctors.ToList();
            return View(doctors);
        }

    }
}
