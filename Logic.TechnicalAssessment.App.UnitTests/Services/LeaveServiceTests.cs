using FluentAssertions;
using Logic.TechnicalAssement.App.Controllers;
using Logic.TechnicalAssement.App.Exceptions;
using Logic.TechnicalAssement.App.Interfaces.Data;
using Logic.TechnicalAssement.App.Interfaces.Services;
using Logic.TechnicalAssement.App.Interfaces.Validators;
using Logic.TechnicalAssement.App.Models;
using Logic.TechnicalAssement.App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.ComponentModel;

namespace Logic.TechnicalAssessment.App.UnitTests
{
    public class LeaveServiceTests
    {
        private readonly Mock<ILogger<LeaveService>> _loggerMock;
        private readonly Mock<ILeaveRepository> _leaveRepositoryMock;
        
        public LeaveServiceTests()
        {
            _loggerMock = new Mock<ILogger<LeaveService>>();
            _leaveRepositoryMock = new Mock<ILeaveRepository>();
        }

        [Fact]
        public void Service_Initialisation_Null_Repository_Errors()
        {
            Assert.Throws<ArgumentNullException>(() => new LeaveService(
                null,
                _loggerMock.Object));
        }

        [Fact]
        public void Service_Initialisation_Null_Logger_Errors()
        {
            Assert.Throws<ArgumentNullException>(() => new LeaveService(
                _leaveRepositoryMock.Object,
                null));
        }

        [Fact]
        public void Get_Should_Return_No_Data()
        {
            // Arrange
            _leaveRepositoryMock.Setup(s => s.Get()).Returns(new List<LeaveViewModel>());
            var leaveService = new LeaveService(
                _leaveRepositoryMock.Object,
                _loggerMock.Object);

            // Act
            var result = leaveService.Get();

            // Assert
            var resultData = Assert.IsType<List<LeaveViewModel>>(result);
            var model = Assert.IsAssignableFrom<List<LeaveViewModel>>(resultData);
            resultData.Should().BeAssignableTo(typeof(List<LeaveViewModel>));
            model.Should().BeEmpty();
        }

        [Fact]
        public void Get_Should_Return_Results()
        {
            // Arrange
            _leaveRepositoryMock.Setup(s => s.Get()).Returns(new List<LeaveViewModel>() {
                new LeaveViewModel () {
                        Email = "email@domain.com",
                        EndDate = DateTime.Now.AddDays(1),
                        StartDate = DateTime.Now,
                        FirstName = "Name",
                        LastName = "Last",
                        Id = 1,
                        IsHalfDay = false,
                        LeaveType = LeaveTypeEnum.AnnualLeave
                } 
            });
            var _leaveController = new LeaveService(
                _leaveRepositoryMock.Object,
                _loggerMock.Object);
            // Act
            var result = _leaveController.Get();

            // Assert
            var resultData = Assert.IsType<List<LeaveViewModel>>(result);
            var model = Assert.IsAssignableFrom<List<LeaveViewModel>>(resultData);
            resultData.Should().BeAssignableTo(typeof(List<LeaveViewModel>));
            model.Should().HaveCount(1);
        }

        [Fact]
        public void Get_Should_Return_UserFriendlyError()
        {
            _leaveRepositoryMock.Setup(s => s.Get()).Throws(() => new Exception());

            var leaveService = new LeaveService(
                _leaveRepositoryMock.Object,
                _loggerMock.Object);
            
            Assert.Throws<UserFriendlyException>(() => leaveService.Get());
        }

       
        [Fact]
        public void Add_Should_Return_Error()
        {
            // Arrange
            _leaveRepositoryMock.Setup(s => s.Add(It.IsAny<LeaveViewModel>())).Throws(() => new Exception("Something went wrong"));

            var leaveService = new LeaveService(
                _leaveRepositoryMock.Object,
                _loggerMock.Object);

            var model = new LeaveViewModel();

            Assert.Throws<UserFriendlyException>(() => leaveService.Add(model));
        }

        [Fact]
        public void Add_With_Null_Parameter_Errors()
        {
            var leaveService = new LeaveService(_leaveRepositoryMock.Object,
                _loggerMock.Object);
            Assert.Throws<ArgumentNullException>(() => leaveService.Add(null));
        }

    }
}