namespace TravelOrganizer.Domain.ValueObjects
{
    // Value Object para localização geográfica
    public class GeoLocation
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoLocation(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude deve estar entre -90 e 90.");
            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude deve estar entre -180 e 180.");

            Latitude = latitude;
            Longitude = longitude;
        }

        // Value Objects devem implementar igualdade por valor
        public override bool Equals(object? obj)
        {
            if (obj is not GeoLocation other) return false;
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        public override int GetHashCode() => HashCode.Combine(Latitude, Longitude);
    }
}