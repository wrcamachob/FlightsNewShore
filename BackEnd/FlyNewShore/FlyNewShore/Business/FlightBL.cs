using FlyNewShore.Models;
using FlyNewShore.Repository;

namespace FlyNewShore.Business
{
    public class FlightBL : IFlightBL<Journey>
    {
        private readonly IFlightRepository fligRep;

        public FlightBL()
        {
            fligRep = new FlightRepository();
        }

        private List<Flight> GetAll()
        {
            var lstFlightRep = fligRep.GetFlight().Result;
            var listFlightBL = new List<Flight>();
            foreach (Entities.Flight flight in lstFlightRep)
            {
                listFlightBL.Add(new Flight
                {
                    Destination = flight.Destination,
                    Origin = flight.Origin,
                    Price = flight.Price,
                    Transport = new Transport() { 
                        FlightCarrier = flight.Transport.FlightCarrier,
                        FlightNumber = flight.Transport.FlightNumber
                    }
                });
            }
            return listFlightBL;
        }

        public Task<List<Journey>> GetJourney(string origin, string destination)
        {
            try
            {
                double priceLocal = 0;
                var lstFlight = GetAll();
                List<Journey> journeyList = new();

                var resultOri = from flight in lstFlight
                                where flight.Origin == origin
                                select new Flight
                                {
                                    Origin = flight.Origin,
                                    Destination = flight.Destination,
                                    Price = flight.Price,
                                    Transport = new Transport()
                                    {
                                        FlightCarrier = flight.Transport.FlightCarrier,
                                        FlightNumber = flight.Transport.FlightNumber
                                    }
                                };                
                List<Flight> lstFlightGen = new();
                foreach (Flight fliOri in resultOri)
                {
                    var resultDest = (from flight in resultOri
                                      where flight.Destination == destination
                                      && flight.Destination == fliOri.Destination
                                      && flight.Origin == origin
                                      select new Flight
                                      {
                                          Origin = flight.Origin,
                                          Destination = flight.Destination,
                                          Price = flight.Price,
                                          Transport = new Transport()
                                          {
                                              FlightCarrier = flight.Transport.FlightCarrier,
                                              FlightNumber = flight.Transport.FlightNumber
                                          }
                                      }).ToList();

                    if (resultDest.Count() == 0)
                    {

                        resultDest = (from flight in lstFlight
                                      where flight.Origin == fliOri.Destination
                                      && flight.Destination == destination
                                      select new Flight
                                      {
                                          Origin = flight.Origin,
                                          Destination = flight.Destination,
                                          Price = flight.Price,
                                          Transport = new Transport()
                                          {
                                              FlightCarrier = flight.Transport.FlightCarrier,
                                              FlightNumber = flight.Transport.FlightNumber
                                          }
                                      }).ToList();
                        foreach (Flight fliDest in resultDest)
                        {
                            if (fliDest.Destination == destination)
                            {
                                lstFlightGen.Add(fliOri);
                                lstFlightGen.Add(fliDest);
                                priceLocal = fliOri.Price + fliDest.Price;
                            }
                        }

                        //foreach (Flight fliDest in resultDest)
                        //{
                        //    if (fliOri.Destination == fliDest.Origin)
                        //    {

                        //    }
                        //    else
                        //    {
                        //        var prueba = (from flight in lstFlight                                              
                        //                      where flight.Origin == fliOri.Destination
                        //                      select new Flight
                        //                      {
                        //                          Origin = flight.Origin,
                        //                          Destination = flight.Destination,
                        //                          Price = flight.Price,
                        //                          Transport = new Transport()
                        //                          {
                        //                              FlightCarrier = flight.Transport.FlightCarrier,
                        //                              FlightNumber = flight.Transport.FlightNumber
                        //                          }
                        //                      }).ToList();

                        //        foreach (Flight pru in prueba)
                        //        {
                        //            if (pru.Destination == destination)
                        //            {

                        //            }
                        //        }
                        //    }
                        //}

                    }
                    else
                    {
                        priceLocal = fliOri.Price;
                        lstFlightGen.Add(fliOri);
                    }

                }

                if (lstFlightGen.Count > 0)
                {
                    journeyList.Add(new Journey
                    {
                        Destination = destination,
                        Origin = origin,
                        Price = priceLocal,
                        Flights = lstFlightGen
                    });
                    return Task.FromResult(journeyList);
                }
                else
                {
                    return Task.FromResult(new List<Journey>());
                }
                

                
            }
            catch (Exception ex)
            {
                return Task.FromResult(new List<Journey>());
            }
        }
    }
}
