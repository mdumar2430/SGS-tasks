using demoSqlite.Controllers.Data;
using demoSqlite.Models;
using System.Collections;

namespace demoSqlite.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IAppDbContext _appDbContext;
        public UsersServices(IAppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public List<User> GetActiveUsers()
        {
            var users = _appDbContext.Users.Where(u => u.IsActive == true).OrderBy(u => u.Name.ToLower());

            if (users.ToList().Count > 0)
            {
                List<User> activeUsers = users.ToList();
                return activeUsers;
            }
            throw new Exception("No Users Found");
        }

        public async Task<Boolean> DeleteUsers(ArrayList ids)
        {
            if (ids.Count > 0)
            {
                foreach (var id in ids)
                {
                    var user = _appDbContext.Users.FirstOrDefault(x => (x.Id.ToString() == id.ToString()) && (x.IsActive == true));
                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            user.IsActive = false;
                            await _appDbContext.SaveChangesAsync();
                        }
                    }
                    else 
                    {
                        throw new Exception("Some user ids doesn't exist.");
                    
                    }
                }
                return true;
            }
            return false;
        }
    }
}
