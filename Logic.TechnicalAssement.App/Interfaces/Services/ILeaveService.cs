using Logic.TechnicalAssement.App.Models;

namespace Logic.TechnicalAssement.App.Interfaces.Services
{
  public interface ILeaveService
  {
        public List<LeaveViewModel> Get();

        public void Add(LeaveViewModel leaveViewModel);
  }
}
