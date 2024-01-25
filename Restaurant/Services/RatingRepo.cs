using Restaurant.Data;
using Restaurant.Infrastructure;
using Restaurant.Models;

namespace Restaurant.Services
{
    public class RatingRepo : IRatings

    {

        private readonly ApplicationDbContext2 _db;

        public RatingRepo(ApplicationDbContext2 db)
        {
            _db = db;
        }
        public List<Rating> GetAll(int id)
        {
            return _db.UserRatings.Where(x =>x.RestroID==id).ToList();
        }

        public void Insert(Rating rating)
        {
            _db.Add(rating);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
