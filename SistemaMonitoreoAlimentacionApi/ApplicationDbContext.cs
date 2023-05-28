using Microsoft.EntityFrameworkCore;

namespace SistemaMonitoreoAlimentacionApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
