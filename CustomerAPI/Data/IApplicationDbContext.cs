using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Entities.Customer> Customers { get; set; }
        Task<int> SaveChanges();
    }
}
