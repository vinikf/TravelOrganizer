using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        private static readonly Lazy<IConfiguration> Configuration = new(() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build());

        public ApplicationDbContext() : base(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(Configuration.Value.GetConnectionString("Database"))
            .Options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Viagem> Viagens { get; set; }
        public DbSet<Viajante> Viajantes { get; set; }
        public DbSet<Roteiro> Roteiros { get; set; }
    }
}

