using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelOrganizer.Domain.Entities
{
    public class Itinerary
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        [StringLength(8000)]
        public string Description { get; set; }

        public Itinerary(DateTime day, string description)
        {
            Day = day;
            Description = description;
        }
    }
}
