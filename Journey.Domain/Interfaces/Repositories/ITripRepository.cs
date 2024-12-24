using Journey.Domain.Entities;

namespace Journey.Domain
{
    public interface ITripRepository
    {
        IEnumerable<Trip> GetAll();
        Trip GetById(Guid id);
        void Add(Trip trip);
        void Update(Trip trip);
        void Delete(Trip trip);
    }
}
