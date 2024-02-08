using ClassLibraryForRestro.Models;

namespace ClassLibraryForRestro.Infrastructure
{
    public interface IRatings
    {
        List<Rating> GetAll(int id);
        void Insert(Rating rating);
        void Delete (int id);
        void save();
    }
}
