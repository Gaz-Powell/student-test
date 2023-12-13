using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using University.Students.DataProvider;
using University.Students.Web.Models;

namespace University.Students.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentsRepository _studentsRepository;

        public HomeController(ILogger<HomeController> logger, IStudentsRepository studentsRepository)
        {
            _logger = logger;
            _studentsRepository = studentsRepository ?? throw new ArgumentNullException(nameof(studentsRepository));
        }

        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Students");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
