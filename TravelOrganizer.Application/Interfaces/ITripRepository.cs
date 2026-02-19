using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Application.Interfaces
{
    public interface ITripRepository
    {
        Task Create(Trip trip);
        Task<List<Trip>> List(int userId);
    }
}
