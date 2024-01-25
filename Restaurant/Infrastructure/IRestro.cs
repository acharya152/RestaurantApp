using Restaurant.Models;

namespace Restaurant.Infrastructure
{
    public interface IRestro
    {
        List<DetailsRestro> GetAll();
        DetailsRestro GetByID(int id);
        

        void Insert (DetailsRestro detailsRestro);
        void Update (DetailsRestro detailsRestro);
        void Delete (DetailsRestro detailsRestro);
        void save();
    }
}
