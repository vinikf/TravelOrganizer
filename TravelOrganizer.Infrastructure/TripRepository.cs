using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.DTOs;
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

        public async Task Create(Trip viagem)
        {
            _db.Trips.Add(viagem);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Trip>> List(int usuarioId)
        {
            return await _db.Trips
                            .Where(v => v.UserId == usuarioId)
                            .ToListAsync();
        }
    }
}
