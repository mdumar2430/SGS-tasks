using demoSqlite.Controllers;
using demoSqlite.Models;
using demoSqlite.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAppApiTest
{
    public class UsersControllerTest
    {
        private readonly Mock<IUsersServices> _usersService;

        public UsersControllerTest()
        {
            _usersService = new Mock<IUsersServices>();
        }

        [Fact]
        public async Task GetUsers_OK()
        {
            //Arrange
            List<User> testUsers = new List<User>()
            {
                new User(),
                new User()
            };
            _usersService.Setup(x => x.GetActiveUsers()).Returns(testUsers);
            var controller = new UsersController(_usersService.Object);
            
            //Act
            var okResult = await controller.GetUsers();
            var data = okResult.Result as OkObjectResult;
            var userList = data.Value as List<User>;

            //Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal(userList.ToString(), testUsers.ToString());
            Assert.Equal(userList.Count(), userList.Count());
        }

        [Fact]
        public async Task GetUsers_Exception_BadRequest()
        {
            //Arrange
            List<User> testUsers = new List<User>();
            _usersService.Setup(x => x.GetActiveUsers()).Throws(new Exception("Empty List"));
            var controller = new UsersController(_usersService.Object);

            //Act
            var badReqResult = await controller.GetUsers();
            var data = badReqResult.Result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(badReqResult);
            Assert.IsType<BadRequestObjectResult>(badReqResult.Result);
            Assert.Equal("Empty List", data.Value);
        }

        [Fact]
        public async Task DeleteUsers_Ok()
        {
            //Arrange
            ArrayList testUserIds = new ArrayList() { 1, 2, 3, 4, 10};
            _usersService.Setup(x => x.DeleteUsers(testUserIds)).ReturnsAsync(true);
            var controller = new UsersController(_usersService.Object);

            //Act
            var okResult = await controller.DeleteUsers(testUserIds);
            var data = okResult.Result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal(data.Value, "Users Deleted Successfully!");

        }

        [Fact]
        public async Task DeleteUsers_NotFound()
        {
            //Arrange
            ArrayList testUserIds = new ArrayList();
            _usersService.Setup(x => x.DeleteUsers(testUserIds)).ReturnsAsync(false);
            var controller = new UsersController(_usersService.Object);

            //Act
            var notfoundResult = await controller.DeleteUsers(testUserIds);
            var data = notfoundResult.Result as NotFoundObjectResult;

            //Assert
            Assert.NotNull(notfoundResult);
            Assert.IsType<NotFoundObjectResult>(notfoundResult.Result);
            Assert.Equal(data.Value, "User Ids Missing");

        }

        [Fact]
        public async Task DeleteUsers_Exception_BadRequest()
        {
            //Arrange
            ArrayList testUserIds = new ArrayList() { 1, 2, 3, 4, 100 };
            _usersService.Setup(x => x.DeleteUsers(testUserIds)).ThrowsAsync(new Exception("Invalid User Ids"));
            var controller = new UsersController(_usersService.Object);

            //Act
            var badReqResult = await controller.DeleteUsers(testUserIds);
            var data = badReqResult.Result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(badReqResult);
            Assert.IsType<BadRequestObjectResult>(badReqResult.Result);
            Assert.Equal(data.Value, "Invalid User Ids");

        }
    }
}
