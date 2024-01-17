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
            var testUser = new User() { Id = 1, Email = "test@gmail.com", Name = "test1", Password = "test"};
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
        public async Task AddUser_Failure_InvalidEmail()
        {
            //Arrange
            var testUser = new User() { Id = 1, Email = "test", Name = "test1", Password = "test" };
            _userServiceMock.Setup(x => x.AddUser(testUser)).ThrowsAsync(new Exception("Invalid Email format"));
            var controller = new UserController(_userServiceMock.Object);

            //Act
            var result = await controller.AddUser(testUser);
            var message = result.Result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Email format", message.Value);
        }

        [Fact]
        public async Task AddUser_Failure_EmptyFields()
        {
            //Test Case 1: Empty Name Field
            //Arrange
            var testUser = new User() { Id = 1, Email = "test@gamil.com", Name = "", Password = "test" };
            _userServiceMock.Setup(x => x.AddUser(testUser)).ThrowsAsync(new Exception("Fields cannot be empty."));
            var controller = new UserController(_userServiceMock.Object);

            //Act
            var result = await controller.AddUser(testUser);
            var message = result.Result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Fields cannot be empty.", message.Value);

            //Test Case 2: Empty Email Field
            //Arrange
            testUser.Email = "";
            testUser.Name = "tester";
            _userServiceMock.Setup(x => x.AddUser(testUser)).ThrowsAsync(new Exception("Fields cannot be empty."));
            controller = new UserController(_userServiceMock.Object);

            //Act
            result = await controller.AddUser(testUser);
            message = result.Result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Fields cannot be empty.", message.Value);

            //Test Case 3: Empty Password Field
            //Arrange
            testUser.Email = "tester@gmail.com";
            testUser.Name = "";
            _userServiceMock.Setup(x => x.AddUser(testUser)).ThrowsAsync(new Exception("Fields cannot be empty."));
            controller = new UserController(_userServiceMock.Object);

            //Act
            result = await controller.AddUser(testUser);
            message = result.Result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Fields cannot be empty.", message.Value);
        }

        [Fact]
        public async Task AddUser_Failure_NullUser()
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
        public async Task DeleteUserById_BadRequest_NotAnActiveUser()
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
        public async Task DeleteUserById_BadRequest_ThrowsException_InvalidUserID()
        {
            //Arrange
            var testUserId = 112;
            _userServiceMock.Setup(x => x.DeleteUserById(testUserId)).ThrowsAsync(new Exception("Error: Invalid UserId"));
            var controller = new UserController(_userServiceMock.Object);

            //Act
            var badReqResult = await controller.DeleteUserbyId(testUserId);
            var message = badReqResult as BadRequestObjectResult;

            //Assert
            Assert.NotNull(badReqResult);
            Assert.IsType<BadRequestObjectResult>(badReqResult);
            Assert.Equal("Error: Invalid UserId", message.Value);
        }
    }
}