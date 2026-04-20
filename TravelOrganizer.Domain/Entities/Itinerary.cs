using System.ComponentModel.DataAnnotations;
using TravelOrganizer.Domain.Exceptions;

namespace TravelOrganizer.Domain.Entities
{
    public class Itinerary
    {
        public int Id { get; private set; }
        public List<ItineraryDay> Days { get; private set; } = new();
    }
}
