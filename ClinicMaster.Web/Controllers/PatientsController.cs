using AutoMapper;
using ClinicMaster.Core;
using ClinicMaster.Core.Dto;
using ClinicMaster.Core.Helpers;
using ClinicMaster.Core.Models;
using ClinicMaster.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicMaster.Web.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var patients = _unitOfWork.Patients.GetPatients();
            return View(patients);
        }

        public IActionResult Details(int id)
        {
            var viewModel = new PatientDetailViewModel()
            {
                Patient = _unitOfWork.Patients.GetPatient(id),
                Appointments = _unitOfWork.Appointments.GetAppointmentWithPatient(id),
                Attendances = _unitOfWork.Attandences.GetAttendance(id),
                CountAppointments = _unitOfWork.Appointments.CountAppointments(id),
                CountAttendance = _unitOfWork.Attandences.CountAttendances(id)
            };
            return View("Details", viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.CitiesList = new SelectList(_unitOfWork.Cities.GetCities(), "Id", "Name");
            ViewBag.GenderList = EnumHelpers.ToSelectList(typeof(Gender));

            var viewModel = new PatientFormViewModel
            {
                Cities = _unitOfWork.Cities.GetCities(),
                Heading = "New Patient"
            };
            return View("PatientForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.GenderList = EnumHelpers.ToSelectList(typeof(Gender));
                ViewBag.CitiesList = new SelectList(_unitOfWork.Cities.GetCities(), "Id", "Name");
                viewModel.Cities = _unitOfWork.Cities.GetCities();
                return View("PatientForm", viewModel);

            }

            var patient = new Patient
            {
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Address = viewModel.Address,
                DateTime = DateTime.Now,
                BirthDate = viewModel.GetBirthDate(),
                Height = viewModel.Height,
                Weight = viewModel.Weight,
                CityId = viewModel.City,
                Sex = viewModel.Sex,
                Token = (0001 + _unitOfWork.Patients.GetPatients().Count()).ToString().PadLeft(7, '0')
            };

            _unitOfWork.Patients.Add(patient);
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Patients");

            // TODO: BUG redirect to detail 
            //return RedirectToAction("Details", new { id = viewModel.Id });
        }


        public IActionResult Edit(int id)
        {
            var patient = _unitOfWork.Patients.GetPatient(id);
            ViewBag.CitiesList = new SelectList(_unitOfWork.Cities.GetCities(), "Id", "Name");
            ViewBag.GenderList = EnumHelpers.ToSelectList(typeof(Gender));
            var viewModel = new PatientFormViewModel
            {
                Heading = "Edit Patient",
                Id = patient.Id,
                Name = patient.Name,
                Phone = patient.Phone,
                Date = patient.DateTime,
                //Date = patient.DateTime.ToString("d MMM yyyy"),
                BirthDate = patient.BirthDate.ToString("dd/MM/yyyy"),
                Address = patient.Address,
                Height = patient.Height,
                Weight = patient.Weight,
                Sex = patient.Sex,
                City = patient.CityId,
                Cities = _unitOfWork.Cities.GetCities()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PatientFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CitiesList = new SelectList(_unitOfWork.Cities.GetCities(), "Id", "Name");
                ViewBag.GenderList = EnumHelpers.ToSelectList(typeof(Gender));
                viewModel.Cities = _unitOfWork.Cities.GetCities();
                return View(viewModel);
            }


            var patientInDb = _unitOfWork.Patients.GetPatient(viewModel.Id);
            patientInDb.Id = viewModel.Id;
            patientInDb.Name = viewModel.Name;
            patientInDb.Phone = viewModel.Phone;
            patientInDb.BirthDate = viewModel.GetBirthDate();
            patientInDb.Address = viewModel.Address;
            patientInDb.Height = viewModel.Height;
            patientInDb.Weight = viewModel.Weight;
            patientInDb.Sex = viewModel.Sex;
            patientInDb.CityId = viewModel.City;

            _unitOfWork.Complete();
            return RedirectToAction("Index", "Patients")
;
        }


        #region Api Calls

        [HttpGet]
        public IActionResult GetPatients() 
        {

            var patientsQuery = _unitOfWork.Patients.GetPatients();


            var patientDto = patientsQuery.ToList()
                                          .Select(_mapper.Map<Patient, PatientDto>);
            return Json(patientDto);
        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var patient = _unitOfWork.Patients.GetPatient(id);
            _unitOfWork.Patients.Remove(patient);
            _unitOfWork.Complete();
            return Json(new { success = true, message = "Delete Product Successful" });
        }


        #endregion
    }
}
