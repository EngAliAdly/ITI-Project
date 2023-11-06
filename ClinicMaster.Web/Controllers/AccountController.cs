using ClinicMaster.Core;
using ClinicMaster.Core.Models;
using ClinicMaster.Core.Models.Extend;
using ClinicMaster.Core.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

namespace ClinicMaster.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Prop
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Login
        //
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //
        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else if (result.RequiresTwoFactor)
            {
                return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }
        #endregion
        
        #region Register
        //
        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Name = model.Name, UserName = model.Email, Email = model.Email, IsActive = true };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleName.AdministratorRoleName);
                    var claim = new Claim(ClaimTypes.GivenName, model.Name);
                    await _userManager.AddClaimAsync(user, claim);
                    await _signInManager.SignInAsync(user, isPersistent: false);

 
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region RegisterDoctor

        //Doctor registration
        [HttpGet]
        public ActionResult RegisterDoctor()
        {
            var viewModel = new DoctorFormViewModel()
            {
                Specializations = _unitOfWork.Specializations.GetSpecializations()
                // Doctors = _doctorRepository.GetDectors()
            };
            return View("DoctorForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDoctor(DoctorFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = viewModel.RegisterViewModel.Email,
                    Email = viewModel.RegisterViewModel.Email,
                    IsActive = true
                };
                var result = await _userManager.CreateAsync(user, viewModel.RegisterViewModel.Password);

                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, RoleName.DoctorRoleName);


                    Doctor doctor = new Doctor()
                    {
                        Name = viewModel.Name,
                        Phone = viewModel.Phone,
                        Address = viewModel.Address,
                        IsAvailable = true,
                        SpecializationId = viewModel.Specialization,
                        PhysicianId = user.Id
                    };
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, doctor.Name));
                    //Mapper.Map<DoctorFormViewModel, Doctor>(model, doctor);
                    _unitOfWork.Doctors.Add(doctor);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index", "Doctors");
                }

                this.AddErrors(result);
            }

            viewModel.Specializations = _unitOfWork.Specializations.GetSpecializations();

            // If we got this far, something failed, redisplay form
            return View("DoctorForm", viewModel);
        }


        #endregion

        #region Helper

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
