using FluentAssertions;
using Logic.TechnicalAssement.App.Models;
using Logic.TechnicalAssement.App.Validators.Model;

namespace Logic.TechnicalAssessment.App.UnitTests
{
    public class LeaveViewModelValidatorTests
    {
        public static IEnumerable<object[]> LeaveObjectsUnderTest =>
        new List<object[]>
        {
            new object[] {
                new LeaveViewModel()
                {
                     Email = "email@domain.com",
                     EndDate = DateTime.Now.AddDays(1),
                     FirstName = "First",
                     Id = 1,
                     IsHalfDay = false,
                     LastName = "Last",
                     LeaveType = LeaveTypeEnum.AnnualLeave,
                     StartDate = DateTime.Now
                }, 0
            },
            new object[] {
                new LeaveViewModel()
                {
                     Email = "",
                     EndDate = DateTime.Now.AddDays(1),
                     FirstName = "First",
                     Id = 1,
                     IsHalfDay = false,
                     LastName = "Last",
                     LeaveType = LeaveTypeEnum.AnnualLeave,
                     StartDate = DateTime.Now
                }, 1
            },
            new object[] {
                new LeaveViewModel()
                {
                     Email = "rubbish",
                     EndDate = DateTime.Now.AddDays(1),
                     FirstName = "First",
                     Id = 1,
                     IsHalfDay = false,
                     LastName = "Last",
                     LeaveType = LeaveTypeEnum.AnnualLeave,
                     StartDate = DateTime.Now
                }, 1
            },
            new object[] {
                new LeaveViewModel()
                {
                     Email = "",
                     EndDate = DateTime.Now.AddDays(1),
                     FirstName = "",
                     Id = 1,
                     IsHalfDay = false,
                     LastName = "",
                     LeaveType = LeaveTypeEnum.AnnualLeave,
                     StartDate = DateTime.Now
                }, 3
            },
            new object[] {
                new LeaveViewModel()
                {
                     Email = "email@domain.com",
                     EndDate = DateTime.Now.AddDays(1),
                     FirstName = "First",
                     Id = 1,
                     IsHalfDay = true,
                     LastName = "Last",
                     LeaveType = LeaveTypeEnum.AnnualLeave,
                     StartDate = DateTime.Now
                }, 1
            }
        };


        [Theory, MemberData(nameof(LeaveObjectsUnderTest))]
        public void Validate_Should_Return_Expected_Errors(LeaveViewModel leaveViewModel, int numberOfErrors)
        {
            // Arrange
            LeaveViewModelValidator leaveViewModelValidator = new LeaveViewModelValidator();
            // Act
            var result = leaveViewModelValidator.Validate(leaveViewModel);

            //Assert
            result.Should().BeAssignableTo<List<string>>();
            ((List<string>)result).Should().HaveCount(numberOfErrors);
        }
    }
}