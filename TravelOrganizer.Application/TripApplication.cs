using TravelOrganizer.Application.Interfaces;
using TravelOrganizer.Domain.Entities;
using TravelOrganizer.Domain.DTOs;

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
            var trip = new Trip(dto.Name, dto.StartDate, dto.EndDate, _userContext.User.Id);
            
            foreach (var traveler in dto.Travelers)
            {
                trip.AddTraveler(traveler);
            }
            foreach (var itinerary in dto.Itineraries)
            {
                trip.AddItinerary(itinerary);
            }

            await _tripRepository.Create(trip);
        }

        public async Task<List<Trip>> List()
        {
            var userId = _userContext.User.Id;
            return await _tripRepository.List(userId);
        }
    }
}
