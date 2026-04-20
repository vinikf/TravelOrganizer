using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Infrastructure
{
    public class TripRepository : ITripRepository
    {
        private readonly ApplicationDbContext _db;

        public TripRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Trip trip)
        {
            await _db.Trips.AddAsync(trip);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Trip>> List(int userId)
        {
            return await _db.Trips
                            .Where(v => v.UserId == userId)
                            .ToListAsync();
        }
    }
}
