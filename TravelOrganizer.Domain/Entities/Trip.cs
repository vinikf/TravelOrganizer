using TravelOrganizer.Domain.Exceptions;

namespace TravelOrganizer.Domain.Entities
{
    public class Trip
    {
        protected Trip() { }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int UserId { get; private set; }
        public virtual User User { get; private set; }
        private readonly List<Traveler> _travelers = new();
        public IReadOnlyCollection<Traveler> Travelers => _travelers.AsReadOnly();

        private readonly List<Itinerary> _itineraries = new();
        public IReadOnlyCollection<Itinerary> Itineraries => _itineraries.AsReadOnly();

        public Trip(string name, DateTime startDate, DateTime endDate, int userId)
        {
            Validate(name, startDate, endDate);

            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
        }

        private static void Validate(string name, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainExceptions("Trip name is required.");

            if (name.Length > 100)
                throw new DomainExceptions("Trip name cannot exceed 100 characters.");

            if (endDate < startDate)
                throw new DomainExceptions("End date must be after start date.");
        }

        public void UpdateDetails(string name, DateTime startDate, DateTime endDate)
        {
            Validate(name, startDate, endDate);

            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void AddTraveler(Traveler traveler)
        {
            if (traveler is null)
                throw new DomainExceptions("Traveler cannot be null.");

            if (_travelers.Any(t => t.Id == traveler.Id))
                throw new DomainExceptions("Traveler already added to this trip.");

            _travelers.Add(traveler);
        }

        public void RemoveTraveler(int travelerId)
        {
            var traveler = _travelers.FirstOrDefault(t => t.Id == travelerId);

            if (traveler is null)
                throw new DomainExceptions("Traveler not found in this trip.");

            _travelers.Remove(traveler);
        }

        public void AddItinerary(Itinerary itinerary)
        {
            if (itinerary is null)
                throw new DomainExceptions("Itinerary cannot be null.");

            if (itinerary.Days.Any(x => x.Date < StartDate || x.Date > EndDate))
                throw new DomainExceptions("Itinerary date must be within the trip period.");

            _itineraries.Add(itinerary);
        }

        public bool IsActive(DateTime referenceDate)
        {
            return referenceDate >= StartDate && referenceDate <= EndDate;
        }
    }
}
