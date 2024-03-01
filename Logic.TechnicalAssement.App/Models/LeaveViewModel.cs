using System.ComponentModel.DataAnnotations;

namespace Logic.TechnicalAssement.App.Models
{
    public class LeaveViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Leave Type is required")]
        public LeaveTypeEnum LeaveType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Email address is not in a valid format")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
        public bool IsHalfDay {  get; set; }
    }
    public enum LeaveTypeEnum
    {
        [Display(Name = "Annual Leave")]
        AnnualLeave,
        [Display(Name = "Sick Leave")]
        SickLeave,
        [Display(Name = "Compassionate Leave")]
        CompassionateLeave,
        [Display(Name = "Unpaid Leave")]
        UnpaidLeave,
    }
}
