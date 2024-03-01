using FluentAssertions;
using Logic.TechnicalAssement.App.Controllers;
using Logic.TechnicalAssement.App.Exceptions;
using Logic.TechnicalAssement.App.Interfaces.Services;
using Logic.TechnicalAssement.App.Interfaces.Validators;
using Logic.TechnicalAssement.App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.ComponentModel;

namespace Logic.TechnicalAssessment.App.UnitTests
{
    public class LeaveControllerTests
    {
        private readonly Mock<ILogger<LeaveController>> _loggerMock;
        private readonly Mock<ILeaveService> _leaveServiceMock;
        private readonly Mock<ILeaveViewModelValidator> _leaveViewModelValidatorMock;
        public LeaveControllerTests()
        {
            _leaveServiceMock = new Mock<ILeaveService>();
            _loggerMock = new Mock<ILogger<LeaveController>>();
            _leaveViewModelValidatorMock = new Mock<ILeaveViewModelValidator>();
        }

        [Fact]
        public void LeaveRequests_Should_Return_No_Data()
        {
            // Arrange
            _leaveViewModelValidatorMock.Setup(v => v.Validate(It.IsAny<LeaveViewModel>())).Returns(new List<string>());
            _leaveServiceMock.Setup(s => s.Get()).Returns(new List<LeaveViewModel>());
            var _leaveController = new LeaveController(_loggerMock.Object,
                _leaveServiceMock.Object,
                _leaveViewModelValidatorMock.Object
                );
            // Act
            var result = _leaveController.LeaveRequests();

            // Assert
            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<List<LeaveViewModel>>(
                viewResult.ViewData.Model);
            viewResult.ViewData.Model.Should().BeAssignableTo(typeof(List<LeaveViewModel>));
            model.Should().BeEmpty();
        }

        [Fact]
        public void LeaveRequests_Should_Return_Results()
        {
            // Arrange
            _leaveViewModelValidatorMock.Setup(v => v.Validate(It.IsAny<LeaveViewModel>())).Returns(new List<string>());
            _leaveServiceMock.Setup(s => s.Get()).Returns(new List<LeaveViewModel>() {
                new LeaveViewModel () {
                        Email = "email@domain.com",
                         EndDate = DateTime.Now.AddDays(1),
                         StartDate = DateTime.Now,
                          FirstName = "Name",
                          LastName = "Last",
                           Id = 1,
                            IsHalfDay = false,
                             LeaveType = LeaveTypeEnum.AnnualLeave
                } });
            var _leaveController = new LeaveController(_loggerMock.Object,
                _leaveServiceMock.Object,
                _leaveViewModelValidatorMock.Object
                );
            // Act
            var result = _leaveController.LeaveRequests();

            // Assert
            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<List<LeaveViewModel>>(
                viewResult.ViewData.Model);
            viewResult.ViewData.Model.Should().BeAssignableTo(typeof(List<LeaveViewModel>));
            model.Should().HaveCount(1);
        }

        [Fact]
        public void LeaveRequests_Should_Return_UserFriendlyError()
        {
            // Arrange
            _leaveViewModelValidatorMock.Setup(v => v.Validate(It.IsAny<LeaveViewModel>())).Returns(new List<string>());
            _leaveServiceMock.Setup(s => s.Get()).Throws<UserFriendlyException>();

            var _leaveController = new LeaveController(_loggerMock.Object,
                _leaveServiceMock.Object,
                _leaveViewModelValidatorMock.Object
                );
            // Act
            var result = _leaveController.LeaveRequests();

            // Assert
            result.Should().BeAssignableTo(typeof(BadRequestObjectResult));
            ((BadRequestObjectResult)result).StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            ((BadRequestObjectResult)result).Value.Should().Be("Something has gone wrong");
        }

        [Fact]
        public void LeaveRequests_Should_Return_Error()
        {
            // Arrange
            _leaveViewModelValidatorMock.Setup(v => v.Validate(It.IsAny<LeaveViewModel>())).Returns(new List<string>());
            _leaveServiceMock.Setup(s => s.Get()).Throws(() => new Exception("Big Error"));

            var _leaveController = new LeaveController(_loggerMock.Object,
                _leaveServiceMock.Object,
                _leaveViewModelValidatorMock.Object
                );
            // Act
            var result = _leaveController.LeaveRequests();

            // Assert
            result.Should().BeAssignableTo(typeof(StatusCodeResult));
            ((StatusCodeResult)result).StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _loggerMock.Verify(f => f.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Big Error"),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Fact]
        public void Index_Should_Return_View()
        {
            // Arrange
            _leaveViewModelValidatorMock.Setup(v => v.Validate(It.IsAny<LeaveViewModel>())).Returns(new List<string>());
            _leaveServiceMock.Setup(s => s.Get()).Returns(new List<LeaveViewModel>());
            var _leaveController = new LeaveController(_loggerMock.Object,
                _leaveServiceMock.Object,
                _leaveViewModelValidatorMock.Object
                );
            // Act
            var result = _leaveController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Post_Should_Return_Ok()
        {
            // Arrange
            _leaveViewModelValidatorMock.Setup(v => v.Validate(It.IsAny<LeaveViewModel>())).Returns(new List<string>());
            _leaveServiceMock.Setup(s => s.Add(It.IsAny<LeaveViewModel>())).Verifiable();

            var _leaveController = new LeaveController(_loggerMock.Object,
                _leaveServiceMock.Object,
                _leaveViewModelValidatorMock.Object
                );
            // Act
            var result = _leaveController.Post(It.IsAny<LeaveViewModel>());

            // Assert
            result.Should().BeAssignableTo(typeof(StatusCodeResult));
            ((StatusCodeResult)result).StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void Post_Should_Return_Error()
        {
            // Arrange
            _leaveViewModelValidatorMock.Setup(v => v.Validate(It.IsAny<LeaveViewModel>())).Returns(new List<string>());
            _leaveServiceMock.Setup(s => s.Add(It.IsAny<LeaveViewModel>())).Throws(() => new Exception("Big Error"));

            var _leaveController = new LeaveController(_loggerMock.Object,
                _leaveServiceMock.Object,
                _leaveViewModelValidatorMock.Object
                );
            // Act
            var result = _leaveController.Post(It.IsAny<LeaveViewModel>());

            // Assert
            result.Should().BeAssignableTo(typeof(StatusCodeResult));
            ((StatusCodeResult)result).StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _loggerMock.Verify(f => f.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "Big Error"),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()));
        }

        [Fact]
        public void Post_Should_Return_UserFriendlyException()
        {
            // Arrange
            _leaveViewModelValidatorMock.Setup(v => v.Validate(It.IsAny<LeaveViewModel>())).Returns(new List<string>());
            _leaveServiceMock.Setup(s => s.Add(It.IsAny<LeaveViewModel>())).Throws(() => new UserFriendlyException("User friendly error"));

            var _leaveController = new LeaveController(_loggerMock.Object,
                _leaveServiceMock.Object,
                _leaveViewModelValidatorMock.Object
                );
            // Act
            var result = _leaveController.Post(It.IsAny<LeaveViewModel>());

            // Assert
            result.Should().BeAssignableTo(typeof(BadRequestObjectResult));
            ((BadRequestObjectResult)result).StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            ((BadRequestObjectResult)result).Value.Should().Be("User friendly error");
        }


    }
}