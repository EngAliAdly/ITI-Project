﻿using ClinicMaster.Core.ViewModel;
using ClinicMaster.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ClinicMaster.Web.Controllers
{
    [Authorize(Roles = "Administrator,Doctor,Assistant")]
    public class DoctorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var doctors = _unitOfWork.Doctors.GetDectors();
            return View(doctors);
        }

        //Details for admin
        public IActionResult Details(int id)
        {
            var viewModel = new DoctorDetailViewModel
            {
                Doctor = _unitOfWork.Doctors.GetDoctor(id),
                UpcomingAppointments = _unitOfWork.Appointments.GetTodaysAppointments(id),
                Appointments = _unitOfWork.Appointments.GetAppointmentByDoctor(id),
            };
            return View(viewModel);
        }

        public IActionResult DoctorProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var viewModel = new DoctorDetailViewModel
            {
                Doctor = _unitOfWork.Doctors.GetProfile(userId),
                Appointments = _unitOfWork.Appointments.GetUpcommingAppointments(userId),
            };
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Specializations = new SelectList(_unitOfWork.Specializations.GetSpecializations(), "Id", "Name");
            var doctor = _unitOfWork.Doctors.GetDoctor(id);
            if (doctor == null) return NotFound();
            var viewModel = new DoctorFormViewModel()
            {

                Id = doctor.Id,
                Name = doctor.Name,
                Phone = doctor.Phone,
                Address = doctor.Address,
                IsAvailable = doctor.IsAvailable,
                Specialization = doctor.SpecializationId,
                Specializations = _unitOfWork.Specializations.GetSpecializations()

            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(DoctorFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Specializations = new SelectList(_unitOfWork.Specializations.GetSpecializations(), "Id", "Name", viewModel.Specialization);
                viewModel.Specializations = _unitOfWork.Specializations.GetSpecializations();
                TempData["error"] = "Doctor Edited Not Valid";
                return View(viewModel);
            }

            var doctorInDb = _unitOfWork.Doctors.GetDoctor(viewModel.Id);
            doctorInDb.Id = viewModel.Id;
            doctorInDb.Name = viewModel.Name;
            doctorInDb.Phone = viewModel.Phone;
            doctorInDb.Address = viewModel.Address;
            doctorInDb.IsAvailable = viewModel.IsAvailable;
            doctorInDb.SpecializationId = viewModel.Specialization;

            _unitOfWork.Complete();
            TempData["success"] = "Doctor Edited successfully";
            return RedirectToAction("Details", new { id = viewModel.Id });
        }
    }
}
