using System.ComponentModel.DataAnnotations;

namespace FlyNewShore.Models
{
    public class Journey
    {
        public List<Flight> Flights { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public double Price { get; set; }
    }
}
