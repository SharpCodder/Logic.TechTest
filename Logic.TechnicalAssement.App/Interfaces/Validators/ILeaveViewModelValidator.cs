using Logic.TechnicalAssement.App.Models;

namespace Logic.TechnicalAssement.App.Interfaces.Validators
{
  public interface ILeaveViewModelValidator
  {
        public List<string> Validate(LeaveViewModel leaveViewModel);
  }
}
