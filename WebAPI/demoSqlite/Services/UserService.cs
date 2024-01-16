using demoSqlite.Controllers.Data;
using demoSqlite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demoSqlite.Services
{
    public class UserService : IUserService
    {
        private readonly IAppDbContext _appDbContext;

        public UserService(IAppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<User> AddUser(User user)
        {
            if (user != null)
            {
                if(String.IsNullOrEmpty(user.Name) || String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password))
                {
                    throw new Exception("Fields Cannot be Empty");
                }
                else
                {
                    var addr = new System.Net.Mail.MailAddress(user.Email);
                    if (addr.Address == user.Email)
                    {
                        _appDbContext.Users.Add(user);
                        await _appDbContext.SaveChangesAsync();
                        return user;
                    }
                }
                              
            }
            throw new Exception("Error in Adding User");
        }

        public async Task<bool> DeleteUserById(int userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                if (user.IsActive)
                {
                    user.IsActive = false;
                    await _appDbContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            throw new Exception("Invalid Userid");
        }
    }
}
