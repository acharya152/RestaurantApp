using Restaurant.Models;

namespace Restaurant.Infrastructure
{
    public interface IRatings
    {
        List<Rating> GetAll(int id);
        void Insert(Rating rating);
        void Delete (int id);
        void save();
    }
}
