using ClinicMaster.Core;
using ClinicMaster.Core.Models;
using ClinicMaster.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicMaster.Web.Controllers
{
    [Authorize(Roles = "Administrator,Assistant")]
    public class AppointmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var appointments = _unitOfWork.Appointments.GetAppointments();
            return View(appointments);
        }

        public IActionResult Details(int id)
        {
            var appointment = _unitOfWork.Appointments.GetAppointmentWithPatient(id);
            return View("_AppointmentPartial", appointment);
        }
        public IActionResult Create(int id)
        {
            ViewBag.DoctorsList = new SelectList(_unitOfWork.Doctors.GetAvailableDoctors(), "Id", "Name");
            var viewModel = new AppointmentFormViewModel
            {
                Patient = id,
                Doctors = _unitOfWork.Doctors.GetAvailableDoctors(),

                Heading = "New Appointment"
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppointmentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DoctorsList = new SelectList(_unitOfWork.Doctors.GetAvailableDoctors(), "Id", "Name");
                viewModel.Doctors = _unitOfWork.Doctors.GetAvailableDoctors();
                TempData["error"] = "Appointment Created Not Valid";
                return View(viewModel);

            }

            var startDateTime = viewModel.GetStartDateTime();

            // Validate that the appointment date is today's date or higher
            if (startDateTime.Date < DateTime.Now.Date)
            {
                ViewBag.DoctorsList = new SelectList(_unitOfWork.Doctors.GetAvailableDoctors(), "Id", "Name");
                viewModel.Doctors = _unitOfWork.Doctors.GetAvailableDoctors();
                TempData["error"] = "Appointment Date must be today's date or higher.";
                return View(viewModel);
            }

            var appointment = new Appointment()
            {
                StartDateTime = viewModel.GetStartDateTime(),
                Detail = viewModel.Detail,
                Status = false,
                PatientId = viewModel.Patient,
                Doctor = _unitOfWork.Doctors.GetDoctor(viewModel.Doctor)

            };
            //Check if the slot is available
            if (_unitOfWork.Appointments.ValidateAppointment(appointment.StartDateTime, viewModel.Doctor))
            {
                ViewBag.DoctorsList = new SelectList(_unitOfWork.Doctors.GetAvailableDoctors(), "Id", "Name");
                viewModel.Doctors = _unitOfWork.Doctors.GetAvailableDoctors();
                TempData["error"] = "Appointment Time Is Not Valid";
                return View(viewModel);
            }


            _unitOfWork.Appointments.Add(appointment);
            _unitOfWork.Complete();
            TempData["success"] = "Appointment Created successfully";
            return RedirectToAction("Index", "Appointments");
        }

        public IActionResult Edit(int id)
        {
            var appointment = _unitOfWork.Appointments.GetAppointment(id);
            ViewBag.DoctorsList = new SelectList(_unitOfWork.Doctors.GetAvailableDoctors(), "Id", "Name");
            var viewModel = new AppointmentFormViewModel()
            {
                Heading = "New Appointment",
                Id = appointment.Id,
                Date = appointment.StartDateTime.ToString("dd/MM/yyyy"),
                Time = appointment.StartDateTime.ToString("HH:mm"),
                Detail = appointment.Detail,
                Status = appointment.Status,
                Patient = appointment.PatientId,
                Doctor = appointment.DoctorId,
                //Patients = _unitOfWork.Patients.GetPatients(),
                Doctors = _unitOfWork.Doctors.GetDectors()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AppointmentFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DoctorsList = new SelectList(_unitOfWork.Doctors.GetAvailableDoctors(), "Id", "Name");
                viewModel.Doctors = _unitOfWork.Doctors.GetDectors();
                viewModel.Patients = _unitOfWork.Patients.GetPatients();
                TempData["error"] = "Appointment Edited successfully";
                return View(viewModel);
            }

            var appointmentInDb = _unitOfWork.Appointments.GetAppointment(viewModel.Id);
            appointmentInDb.Id = viewModel.Id;
            appointmentInDb.StartDateTime = viewModel.GetStartDateTime();
            appointmentInDb.Detail = viewModel.Detail;
            appointmentInDb.Status = viewModel.Status;
            appointmentInDb.PatientId = viewModel.Patient;
            appointmentInDb.DoctorId = viewModel.Doctor;

            _unitOfWork.Complete();
            TempData["success"] = "Appointment Edited successfully";
            return RedirectToAction("Index");

        }

        public PartialViewResult GetUpcommingAppointments(int id)
        {
            var appointments = _unitOfWork.Appointments.GetTodaysAppointments(id);
            return PartialView(appointments);
        }

        public IActionResult DoctorsList()
        {
            var doctors = _unitOfWork.Doctors.GetAvailableDoctors();

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new SelectList(doctors, "Id", "Name"));
            }

            return RedirectToAction("Create");
        }
    }
}
