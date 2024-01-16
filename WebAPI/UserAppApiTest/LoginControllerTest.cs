using demoSqlite.Controllers;
using demoSqlite.Models;
using demoSqlite.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAppApiTest
{
    public class LoginControllerTest
    {
        private readonly Mock<ILoginService> _loginServiceMock;
        public LoginControllerTest() {
            _loginServiceMock = new Mock<ILoginService>();
        }

        [Fact]
        public void validateUser_OK()
        {
            LoginUser validUser = new LoginUser()
            {
                Email = "testing@gmail.com",
                Password = "correct_password"
            };
            LoginUser inValidUser = new LoginUser()
            {
                Email = "testing0@gmail.com",
                Password = "incorrect_password"
            };

            _loginServiceMock.Setup(x => x.validateUser(validUser.Email, validUser.Password)).Returns(true);
            var controller = new LoginController(_loginServiceMock.Object);
            var okResult = controller.validateUser(validUser);
            var data = okResult as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(true, data.Value);

            _loginServiceMock.Setup(x => x.validateUser(inValidUser.Email, inValidUser.Password)).Returns(false);
            controller = new LoginController(_loginServiceMock.Object);
            okResult = controller.validateUser(inValidUser);
            data = okResult as OkObjectResult;
            
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.Equal(false, data.Value);

        }

        [Fact]
        public void validateUser_Exception_BadRequest_InvalidEmailFormat()
        {
            LoginUser inValidUser = new LoginUser()
            {
                Email = "testing",
                Password = "password"
            };
            _loginServiceMock.Setup(x => x.validateUser(inValidUser.Email, inValidUser.Password)).Throws(new Exception("Error: Invalid Email Format"));
            var controller = new LoginController( _loginServiceMock.Object);

            var badReqResult = controller.validateUser(inValidUser);
            var data = badReqResult as BadRequestObjectResult;

            Assert.NotNull(badReqResult);
            Assert.IsType<BadRequestObjectResult>(badReqResult);
            Assert.Equal("Error: Invalid Email Format", data.Value);
        }

        [Fact]
        public void validateUser_Exception_BadRequest_EmptyFields()
        {
            LoginUser inValidUser = new LoginUser()
            {
                Email = "",
                Password = "password"
            };
            _loginServiceMock.Setup(x => x.validateUser(inValidUser.Email, inValidUser.Password)).Throws(new Exception("Error: Fields cannot be empty."));
            var controller = new LoginController(_loginServiceMock.Object);

            var badReqResult = controller.validateUser(inValidUser);
            var data = badReqResult as BadRequestObjectResult;

            Assert.NotNull(badReqResult);
            Assert.IsType<BadRequestObjectResult>(badReqResult);
            Assert.Equal("Error: Fields cannot be empty.", data.Value);
        }
    }
}
