using Logic.TechnicalAssement.App.Interfaces.Data;
using Logic.TechnicalAssement.App.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Logic.TechnicalAssement.App.Data
{
    public class LeaveRepository : ILeaveRepository
    {
        IHttpContextAccessor _httpContextAccessor;
        ISession _session;
        public LeaveRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _session = (httpContextAccessor?.HttpContext) != null ? httpContextAccessor.HttpContext.Session : throw new ArgumentNullException(nameof(httpContextAccessor.HttpContext));
        }

        public const string SessionKeyName = "Leave";

        public void Add(LeaveViewModel leaveViewModel)
        {
            if(leaveViewModel == null) throw new ArgumentNullException(nameof(leaveViewModel));

            List<LeaveViewModel> leave = Get() ?? new List<LeaveViewModel>();
            leaveViewModel.Id = leave.Count + 1;
            leave.Add(leaveViewModel);
            _session.SetString(SessionKeyName, JsonSerializer.Serialize(leave));
            
        }

        public List<LeaveViewModel>? Get()
        {
            string? leaveData = _session.GetString(SessionKeyName);
            return !string.IsNullOrWhiteSpace(leaveData) ? JsonSerializer.Deserialize<List<LeaveViewModel>>(leaveData) : new List<LeaveViewModel>(); 

        }
    }
}
