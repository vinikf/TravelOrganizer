using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelOrganizer.Domain.Exceptions;

namespace TravelOrganizer.Domain.Entities
{
    public class ItineraryDay
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public List<ItineraryItem> Items { get; private set; } = new();
    }
}
