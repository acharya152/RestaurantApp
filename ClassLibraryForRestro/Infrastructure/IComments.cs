using ClassLibraryForRestro.Models;

namespace ClassLibraryForRestro.Infrastructure
{
    public interface IComments
    {
        List<Comments> GetAll(int id);
        void DelById(int id);
        void Insert(Comments comments);
        void Delete(Comments comments);
        void save();
    }
}
