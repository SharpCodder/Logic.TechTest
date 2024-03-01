using FluentAssertions;
using Logic.TechnicalAssement.App.Models;
using Logic.TechnicalAssement.App.Validators.Model;

namespace Logic.TechnicalAssessment.App.UnitTests
{
    public class EmailValidatorTests
    {
        public static IEnumerable<object[]> EmailAddressObjectsUnderTest =>
        new List<object[]>
        {
            new object[] {
                "email@domain.com", true
            },
            new object[] {
                "", false
            },
            new object[] {
                "rubbish", false
            }
        };


        [Theory, MemberData(nameof(EmailAddressObjectsUnderTest))]
        public void Validate_Should_Return_Expected_Errors(string email, bool valid)
        {
            var result = EmailValidator.IsValid(email);

            result.Should().Be(valid);
        }
    }
}