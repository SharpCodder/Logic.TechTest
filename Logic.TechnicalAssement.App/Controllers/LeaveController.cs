using Logic.TechnicalAssement.App.Exceptions;
using Logic.TechnicalAssement.App.Interfaces.Services;
using Logic.TechnicalAssement.App.Interfaces.Validators;
using Logic.TechnicalAssement.App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Logic.TechnicalAssement.App.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILogger<LeaveController> _logger;
        private readonly ILeaveService _leaveService;
        private readonly ILeaveViewModelValidator _leaveViewModelValidator;


        public LeaveController(ILogger<LeaveController> logger, 
            ILeaveService leaveService,
            ILeaveViewModelValidator leaveViewModelValidator)
        {
            _logger = logger;        
            _leaveService = leaveService;
            _leaveViewModelValidator = leaveViewModelValidator;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Post(LeaveViewModel leaveViewModel)
        {
            List<string> validationErrors = _leaveViewModelValidator.Validate(leaveViewModel);
            if(validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            try
            {
                _leaveService.Add(leaveViewModel);
                return Ok();
            }
            catch(UserFriendlyException userFriendlyException)
            {
                return BadRequest(userFriendlyException.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        public IActionResult LeaveRequests()
        {
            try
            {
                IEnumerable<LeaveViewModel> leaveRequests;
                leaveRequests = _leaveService.Get();
                return PartialView("_LeaveRequests", leaveRequests);
            }
            catch(UserFriendlyException userFriendlyException)
            {
                return BadRequest(userFriendlyException.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
