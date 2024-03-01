using FluentAssertions;
using Logic.TechnicalAssement.App.Data;
using Logic.TechnicalAssement.App.Models;
using Logic.TechnicalAssement.App.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Text;
using System.Text.Json;

namespace Logic.TechnicalAssessment.App.UnitTests
{
    public class LeaveRepositoryTests
    {

        Mock<IHttpContextAccessor> _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        Mock<ISession> sessionMock = new Mock<ISession>();

        List<LeaveViewModel> data = new List<LeaveViewModel>() {
            new LeaveViewModel()
            {
                Email = "email@domain.com",
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now,
                FirstName = "Name",
                LastName = "Last",
                Id = 1,
                IsHalfDay = false,
                LeaveType = LeaveTypeEnum.AnnualLeave
            }
        };
        


        [Fact]
        public void Get_Should_Return_No_Data()
        {
            _mockHttpContextAccessor.Setup(m => m.HttpContext.Session).Returns(sessionMock.Object);
            // Arrange
            LeaveRepository repository = new LeaveRepository(_mockHttpContextAccessor.Object);
            var result = repository.Get();

            // Assert
            var resultData = Assert.IsType<List<LeaveViewModel>>(result);
            var model = Assert.IsAssignableFrom<List<LeaveViewModel>>(resultData);
            resultData.Should().BeAssignableTo(typeof(List<LeaveViewModel>));
            model.Should().BeEmpty();
        }

        [Fact]
        public void Get_Should_Return_Data()
        {
            // Arrange
            var contextMock = new Mock<HttpContext>();

            string dataString = JsonSerializer.Serialize(data);
            var value = Encoding.ASCII.GetBytes(dataString);
            sessionMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out value)).Returns(true); //the out dummy does the trick
            contextMock.Setup(s => s.Session).Returns(sessionMock.Object);
            

            //sessionMock.Setup(m=>m.Set("Leave", Encoding.ASCII.GetBytes(dataString)));
            _mockHttpContextAccessor.Setup(m => m.HttpContext).Returns(contextMock.Object);
            
            LeaveRepository repository = new LeaveRepository(_mockHttpContextAccessor.Object);
            var result = repository.Get();

            // Assert
            var resultData = Assert.IsType<List<LeaveViewModel>>(result);
            var model = Assert.IsAssignableFrom<List<LeaveViewModel>>(resultData);
            resultData.Should().BeAssignableTo(typeof(List<LeaveViewModel>));
            model.Should().HaveCount(1);
        }

        [Fact]
        public void Add_With_Null_Param_Throws_Error()
        {
            _mockHttpContextAccessor.Setup(m => m.HttpContext.Session).Returns(sessionMock.Object);
            LeaveRepository repository = new LeaveRepository(_mockHttpContextAccessor.Object);
            Assert.Throws<ArgumentNullException>(() => repository.Add(null));
        }

    }
}