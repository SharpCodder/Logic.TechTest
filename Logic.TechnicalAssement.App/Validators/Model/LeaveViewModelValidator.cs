using Logic.TechnicalAssement.App.Interfaces.Validators;
using Logic.TechnicalAssement.App.Models;
using System.Net.Mail;

namespace Logic.TechnicalAssement.App.Validators.Model
{
  public class LeaveViewModelValidator : ILeaveViewModelValidator
  {
        private List<string> errors = new List<string>();

        public List<string> Validate(LeaveViewModel leaveViewModel)
        {
            if(string.IsNullOrWhiteSpace(leaveViewModel.Email) || !EmailValidator.IsValid(leaveViewModel.Email))
            {
                errors.Add("Email address is not in a valid format");
            }

            if(leaveViewModel.StartDate == default)
            {
                errors.Add("Start date is not valid");
            }

            if (leaveViewModel.EndDate == default)
            {
                errors.Add("End date is not valid");
            }

            if(string.IsNullOrWhiteSpace(leaveViewModel.FirstName))
            {
                errors.Add("First name is required");
            }

            if (string.IsNullOrWhiteSpace(leaveViewModel.LastName))
            {
                errors.Add("Last name is required");
            }

            if (leaveViewModel.IsHalfDay && leaveViewModel.StartDate.Date != leaveViewModel.EndDate.Date)
            {
                errors.Add("The start and end date must be the same for half a day of leave");
            }

            return errors;
        }

  }
}
