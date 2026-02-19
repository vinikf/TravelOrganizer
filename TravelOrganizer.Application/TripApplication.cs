using TravelOrganizer.Api.Controllers;
using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.DTOs;
using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Application
{
    public class TripApplication : ITripApplication
    {
        private readonly ITripRepository _tripRepository;
        private readonly IUserContext _userContext;

        public TripApplication(ITripRepository tripRepository, IUserContext userContext)
        {
            _tripRepository = tripRepository;
            _userContext = userContext;
        }

        public async Task Create(TripDTO dto)
        {
            var trip = new Trip
            {
                Name = dto.Name,
                EndDate = dto.EndDate,
                StartDate = dto.StartDate,
                Itineraries = dto.Itineraries,
                UserId = _userContext.User.Id,
                Travelers = dto.Travelers
            };

            await _tripRepository.Create(trip);
        }

        public async Task<List<Trip>> List()
        {
            var userId = _userContext.User.Id;
            return await _tripRepository.List(userId);
        }
    }
}
