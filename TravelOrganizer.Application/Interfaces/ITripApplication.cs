using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Application.Interfaces
{
    public interface ITripApplication
    {
        Task Create(TripDTO dto);
        Task<List<Trip>> List();
    }
}
