using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelOrganizer.Domain.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public List<Traveler> Travelers { get; set; } = new();
        public List<Itinerary> Itineraries { get; set; } = new();
        
    }
}
