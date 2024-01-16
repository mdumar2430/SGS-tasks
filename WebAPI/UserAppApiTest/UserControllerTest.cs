using demoSqlite.Controllers;
using demoSqlite.Controllers.Data;
using demoSqlite.Models;
using demoSqlite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UserAppApiTest
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        public UserControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact]
        public async Task AddUser_Success()
        {
            //Arrange
            var testUser = new User() { Id = 1, Email = "test", IsActive = true, Name = "test1", Password = "test"};
            _userServiceMock.Setup(x => x.AddUser(testUser)).ReturnsAsync(testUser);    
            var controller = new UserController(_userServiceMock.Object);

            //Act
            var okResult = await controller.AddUser(testUser);
            var message = okResult.Result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("User Added Successfully!", message.Value);         
        }

        [Fact]
        public async Task AddUser_Failure()
        {
            //Arrange
            var testUser = (User)null;
            _userServiceMock.Setup(x => x.AddUser(testUser)).ThrowsAsync(new Exception("Error in Adding User"));
            var controller = new UserController(_userServiceMock.Object);

            //Act
            var result = await controller.AddUser(testUser);
            var message = result.Result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Error in Adding User", message.Value);
        }

        [Fact]
        public async Task DeleteUserById_Ok()
        {
            //Arrange
            var testUserId = 12;
            _userServiceMock.Setup(x => x.DeleteUserById(testUserId)).ReturnsAsync(true);
            var controller = new UserController(_userServiceMock.Object);

            //Act
            var okResult = await controller.DeleteUserbyId(testUserId);

            //Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task DeleteUserById_BadRequest()
        {
            //Arrange
            var testUserId = 2;
            _userServiceMock.Setup(x => x.DeleteUserById(testUserId)).ReturnsAsync(false);
            var controller = new UserController(_userServiceMock.Object);

            //Act
            var badReqResult = await controller.DeleteUserbyId(testUserId);
            var message = badReqResult as BadRequestObjectResult;

            //Assert
            Assert.NotNull(badReqResult);
            Assert.IsType<BadRequestObjectResult>(badReqResult);
            Assert.Equal("User not found", message.Value);
        }

        [Fact]
        public async Task DeleteUserById_BadRequest_ThrowsException()
        {
            //Arrange
            var testUserId = 112;
            _userServiceMock.Setup(x => x.DeleteUserById(testUserId)).ThrowsAsync(new Exception("Error"));
            var controller = new UserController(_userServiceMock.Object);

            //Act
            var badReqResult = await controller.DeleteUserbyId(testUserId);
            var message = badReqResult as BadRequestObjectResult;

            //Assert
            Assert.NotNull(badReqResult);
            Assert.IsType<BadRequestObjectResult>(badReqResult);
            Assert.Equal("Error", message.Value);
        }
    }
}