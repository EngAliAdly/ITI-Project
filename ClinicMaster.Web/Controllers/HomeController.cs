using ClinicMaster.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMaster.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region Dashboard Statistics
        [HttpGet]
        public IActionResult TotalPatients()
        {
            var patients = _context.Patients.ToList();
            return Json(patients.Count());
        }

        [HttpGet]
        public IActionResult TotalAppointments()
        {
            var appointments = _context.Appointments.ToList();
            return Json(appointments.Count());
        }
        
        [HttpGet]
        public IActionResult TotalDoctors()
        {
            var doctors = _context.Doctors.ToList();
            return Json(doctors.Count());
        }
        
        [HttpGet]
        public IActionResult TotalUsers()
        {
            var users = _context.Users.ToList();
            return Json(users.Count());
        }

        //Today's patients
        [HttpGet]
        public IActionResult TodaysPatients()
        {
            DateTime today = DateTime.Now.Date;
            var patients = _context.Patients.Where(p => p.DateTime >= today).ToList();
            return Json(patients.Count());
        }

        //Todays appointments
        [HttpGet]
        public IActionResult TodaysAppointments()
        {
            DateTime today = DateTime.Now.Date;
            var appointments = _context.Appointments
                .Where(a => a.StartDateTime >= today)
                .ToList();
            return Json(appointments.Count());
        }

        //Available doctors
        [HttpGet]
        public IActionResult AvailableDoctors()
        {
            var doctors = _context.Doctors
                .Where(d => d.IsAvailable)
                .ToList(); 
            return Json(doctors.Count());
        }

        //Active Accounts
        [HttpGet]
        public IActionResult ActiveAccounts()
        {
            var users = _context.Users
               .Where(u => u.IsActive == true)
               .ToList();
            return Json(users.Count());
        }

        #endregion

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

    }
}