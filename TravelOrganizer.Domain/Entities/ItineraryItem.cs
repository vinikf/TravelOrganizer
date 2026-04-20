using TravelOrganizer.Domain.Enums;
using TravelOrganizer.Domain.ValueObjects;

namespace TravelOrganizer.Domain.Entities
{
    public class ItineraryItem
    {
        public int Id { get; private set; }
        public TimeOnly? StartTime { get; private set; }
        public TimeOnly? EndTime { get; private set; }

        public string Title { get; private set; }
        public string? Notes { get; private set; }

        public GeoLocation? Location { get; private set; }
        public ItineraryItemType Type { get; private set; }
    }
}