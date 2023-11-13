using ClinicMaster.Core;
using ClinicMaster.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicMaster.Web.Controllers
{
    [Authorize(Roles = "Administrator,Assistant")]
    public class AssistantsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssistantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var assistants = _unitOfWork.Assistants.GetAssistants();
            return View(assistants);
        }

        //Details for admin
        public IActionResult Details(int id)
        {
            var viewModel = new AssistantDetailViewModel
            {
                Assistant = _unitOfWork.Assistants.GetAssistant(id),
            };
            return View(viewModel);
        }

        public IActionResult AssistantProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var viewModel = new AssistantDetailViewModel
            {
                Assistant = _unitOfWork.Assistants.GetProfile(userId)
            };
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var assistants = _unitOfWork.Assistants.GetAssistant(id);
            if (assistants == null) return NotFound();
            var viewModel = new AssistantFormViewModel()
            {

                Id = assistants.Id,
                Name = assistants.Name,
                Phone = assistants.Phone,
                Address = assistants.Address,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AssistantFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Assistant Edited Not Valid";
                return View(viewModel);
            }

            var assistantInDb = _unitOfWork.Assistants.GetAssistant(viewModel.Id);
            assistantInDb.Id = viewModel.Id;
            assistantInDb.Name = viewModel.Name;
            assistantInDb.Phone = viewModel.Phone;
            assistantInDb.Address = viewModel.Address;

            _unitOfWork.Complete();
            TempData["success"] = "Assistant Edited successfully";
            return RedirectToAction("Details", new { id = viewModel.Id });
        }

    }
}
