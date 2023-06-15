using FlyNewShore.Entities;

namespace FlyNewShore.Repository
{
    public interface IFlightRepository 
    {
        Task<List<Flight>> GetFlight();
    }
}
