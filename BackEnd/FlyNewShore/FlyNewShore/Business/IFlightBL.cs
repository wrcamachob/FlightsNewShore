using FlyNewShore.Models;

namespace FlyNewShore.Business
{
    public interface IFlightBL<T>
    {
        Task<List<Journey>> GetJourney(string origin, string destination);
    }
}
