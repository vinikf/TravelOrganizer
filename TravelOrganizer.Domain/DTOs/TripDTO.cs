using TravelOrganizer.Domain.Entities;

namespace TravelOrganizer.Domain.DTOs
{
    public class TripDTO
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public List<Traveler> Travelers { get; set; } = new();
        public List<Itinerary> Itineraries { get; set; } = new();
    }
}