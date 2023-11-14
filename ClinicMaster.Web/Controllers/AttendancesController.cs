using ClinicMaster.Core;
using ClinicMaster.Core.Models;
using ClinicMaster.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMaster.Web.Controllers
{
    [Authorize(Roles = "Administrator,Doctor,Assistant")]
    public class AttendancesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Details(int id)
        {
            var attendance = _unitOfWork.Attandences.GetAttendance(id);
            return View("_attendancePartial", attendance);
        }

        public IActionResult Create(int id)
        {
            var viewModel = new AttendanceFormViewModel
            {
                Patient = id,
                Heading = "Add Attendance"
            };
            return View("AttendanceForm", viewModel);
        }

        [HttpPost]
        public IActionResult Create(AttendanceFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Attendance Created Not Valid";
                return View("AttendanceForm", viewModel);
            }

            var attendance = new Attendance
            {
                ClinicRemarks = viewModel.ClinicRemarks,
                Diagnosis = viewModel.Diagnosis,
                Therapy = viewModel.Therapy,
                Date = DateTime.Now,
                Patient = _unitOfWork.Patients.GetPatient(viewModel.Patient)
            };
            _unitOfWork.Attandences.Add(attendance);
            _unitOfWork.Complete();
            TempData["success"] = "Attendance Created successfully";
            return RedirectToAction("Details", "Patients", new { id = viewModel.Patient });
        }
    }
}
