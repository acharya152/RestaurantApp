using Restaurant.Data;
using Restaurant.Infrastructure;
using Restaurant.Models;

namespace Restaurant.Services
{
    public class Restrorepo : IRestro
    {
        private readonly ApplicationDbContext2 _db;
        public Restrorepo(ApplicationDbContext2 db)
        {
                _db= db;
        }
        public void Delete(DetailsRestro detailsRestro)
        {
            _db.DetailsRestroo.Remove(detailsRestro);
        }

        public List<DetailsRestro> GetAll()
        {
            return _db.DetailsRestroo.ToList();
        }

        public DetailsRestro GetByID(int id)
        {
            return _db.DetailsRestroo.Where(x => x.ID == id).FirstOrDefault();
        }
        public DetailsRestro GetByName(string obj)
        {
            return _db.DetailsRestroo.FirstOrDefault(d => d.Name.ToUpper().StartsWith(obj.ToUpper()));
        }
        public void Insert(DetailsRestro detailsRestro)
        {
            _db.DetailsRestroo.Add(detailsRestro);
        }

        public void save()
        {
            _db.SaveChanges();
        }

        public void Update(DetailsRestro detailsRestro)
        {
            _db.DetailsRestroo.Update(detailsRestro);
        }
    }
}
