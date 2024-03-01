using Logic.TechnicalAssement.App.Models;

namespace Logic.TechnicalAssement.App.Interfaces.Data
{
  public interface ILeaveRepository
  {
        public List<LeaveViewModel>? Get();

        public void Add(LeaveViewModel leaveViewModel);
  }
}
