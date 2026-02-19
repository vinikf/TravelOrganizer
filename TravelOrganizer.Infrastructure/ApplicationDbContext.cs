using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
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

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
    }
}

