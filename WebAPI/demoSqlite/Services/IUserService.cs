using demoSqlite.Models;

namespace demoSqlite.Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);

        Task<bool> DeleteUserById(int userId);
    }
}
