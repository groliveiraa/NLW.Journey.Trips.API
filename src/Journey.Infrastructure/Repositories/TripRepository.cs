using Journey.Domain;
using Journey.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly JourneyDbContext _dbContext;

        public TripRepository(JourneyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Trip> GetAll()
        {
            return _dbContext.Trips.ToList();
        }

        public Trip GetById(Guid id)
        {
            return _dbContext.Trips
                .Include(t => t.Activities)
                .FirstOrDefault(t => t.Id == id);
        }

        public void Add(Trip trip)
        {
            _dbContext.Trips.Add(trip);
            _dbContext.SaveChanges();
        }

        public void Update(Trip trip)
        {
            _dbContext.Trips.Update(trip);
            _dbContext.SaveChanges();
        }

        public void Delete(Trip trip)
        {
            _dbContext.Trips.Remove(trip);
            _dbContext.SaveChanges();
        }
    }
}
