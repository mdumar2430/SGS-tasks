using demoSqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace demoSqlite.Controllers.Data
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}