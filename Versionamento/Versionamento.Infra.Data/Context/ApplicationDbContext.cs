using Microsoft.EntityFrameworkCore;
using Versionamento.Domain.Entities;

namespace Versionamento.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
