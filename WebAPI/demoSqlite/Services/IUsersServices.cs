using demoSqlite.Models;
using System.Collections;

namespace demoSqlite.Services
{
    public interface IUsersServices
    {
        Task<bool> DeleteUsers(ArrayList ids);
        List<User> GetActiveUsers();
    }
}