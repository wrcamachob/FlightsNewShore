using FlyNewShore.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;

namespace FlyNewShore.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private string URL = "https://recruiting-api.newshore.es/api/flights/2";
        private int Parameter = 2;
        public async Task<List<Flight>> GetFlight()
        {
            List<Flight> lstFlights = new();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
                        
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        
            HttpResponseMessage response = client.GetAsync(URL).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var flightList = JsonConvert.DeserializeObject<List<FlightApi>>(jsonString);

                if (flightList != null)
                {
                    foreach (var flight in flightList)
                    {
                        Flight flightPL = new Flight()
                        {
                            Destination = flight.arrivalStation,
                            Origin = flight.departureStation,
                            Price = flight.price,
                            Transport = new Transport() { 
                                FlightCarrier = flight.flightCarrier,
                                FlightNumber = flight.flightNumber
                            }
                        };
                        lstFlights.Add(flightPL);
                    }
                    
                }
            }
            client.Dispose();


            return lstFlights;
        }
    }
}
