using demoSqlite.Controllers.Data;
using demoSqlite.Models;
using demoSqlite.RSA;
using demoSqlite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demoSqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger; 
        public LoginController(ILoginService _loginService, ILogger<LoginController> logger)
        {
            this._loginService = _loginService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult validateUser(LoginUser user)
        {
            
            try
            {
                _logger.LogInformation("Working");
                bool isValidUser = this._loginService.validateUser(user.Email, user.Password);
                if (isValidUser)
                {
                    return Ok(isValidUser);
                }
                return Ok(isValidUser);
            }
            catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("encrypt")]
        public ActionResult getEncryptedPassword(string inputPassword)
        {
            string encryptedPassword = this._loginService.encrpt(inputPassword);
            return Ok(encryptedPassword);

        }
    }
}
