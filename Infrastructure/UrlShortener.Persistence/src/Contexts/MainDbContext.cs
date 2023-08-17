using Microsoft.EntityFrameworkCore;

using UrlShortener.Domain.src;

namespace UrlShortener.Persistence.src.Contexts
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UrlManagement> UrlManagement { get; set; }
    }
}
