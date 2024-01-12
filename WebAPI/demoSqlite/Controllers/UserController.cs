using demoSqlite.Controllers.Data;
using demoSqlite.Models;
using demoSqlite.Services;
using Microsoft.AspNetCore.Mvc;


namespace demoSqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(AppDbContext appDbContext, IUserService userService)
        {
            _userService = userService;   
        }

        /// <summary>
        /// Adds User to the DB
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> AddUser(User newUser)
        {
            try
            {
                User user = new User();
                user = await this._userService.AddUser(newUser);
                return Ok("User Added Successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Soft Deletes User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<User>> DeleteUserbyId(int id)
        {
            try
            {
                bool isDeleted;
                isDeleted = await this._userService.DeleteUserById(id);
                if (isDeleted)
                {
                    return Ok("User Deleted Successfully!");
                }
                return BadRequest("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
