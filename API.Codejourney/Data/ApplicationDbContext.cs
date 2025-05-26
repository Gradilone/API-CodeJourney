using API.Codejourney.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Codejourney.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jornadas> Jornadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Jornadas>()
                .Property(j => j.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
