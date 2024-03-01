using Logic.TechnicalAssement.App.Exceptions;
using Logic.TechnicalAssement.App.Interfaces.Data;
using Logic.TechnicalAssement.App.Interfaces.Services;
using Logic.TechnicalAssement.App.Models;

namespace Logic.TechnicalAssement.App.Services
{
    public class LeaveService : ILeaveService
    {

        private ILeaveRepository _leaveRepository;
        private ILogger<LeaveService> _logger;

        public LeaveService(ILeaveRepository leaveRepository,
            ILogger<LeaveService> logger)
        {
            _leaveRepository = leaveRepository ?? throw new ArgumentNullException(nameof(leaveRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Add(LeaveViewModel leaveViewModel)
        {
            if (leaveViewModel == null) throw new ArgumentNullException(nameof(leaveViewModel));

            try
            {
                _leaveRepository.Add(leaveViewModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to add leave");
                throw new UserFriendlyException("Unable to add leave request");
            }
        }

        public List<LeaveViewModel> Get()
        {
            try
            {
                return _leaveRepository.Get() ?? new List<LeaveViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add leave");
                throw new UserFriendlyException("Unable to add leave request");
            }
        }
    }
}
