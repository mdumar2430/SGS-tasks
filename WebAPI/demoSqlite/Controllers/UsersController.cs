using demoSqlite.Controllers.Data;
using demoSqlite.Models;
using demoSqlite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace demoSqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            this._usersServices = usersServices;
        }
        /// <summary>
        /// Get a List of Active Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            try
            {
                List<User> activeUsers = this._usersServices.GetActiveUsers();
                return Ok(activeUsers);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete (soft delete) Multiple Users using a list of User ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<string>> DeleteUsers(ArrayList ids)
        {
            try
            {
                bool isDeleted;
                isDeleted = await this._usersServices.DeleteUsers(ids);
                if (isDeleted)
                {
                    return Ok("Users Deleted Successfully!");
                }
                else
                {
                    return NotFound("User Ids Missing");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
