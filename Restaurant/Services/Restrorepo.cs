using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Infrastructure;
using Restaurant.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            // return _db.DetailsRestroo.ToList();
            return _db.DetailsRestroo.FromSqlRaw($"select_all").ToList();
        }

        public DetailsRestro GetByID(int id)
        {
            return _db.DetailsRestroo.FromSqlRaw($"getbyid").FirstOrDefault();
           // return _db.DetailsRestroo.Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(DetailsRestro detailsRestro)
        { 
            /*_db.Database.ExecuteSqlRaw("EXEC insertinto @Name, @Location, @Description, @DetailedDescription, @PhoneNo, @Time, @CloseTime, @Website, @Photo",
                new SqlParameter("@Name", detailsRestro.Name),
                new SqlParameter("@Location", detailsRestro.Location),
                new SqlParameter("@Description", detailsRestro.Description),
                new SqlParameter("@DetailedDescription", detailsRestro.DetailedDescription),
                new SqlParameter("@PhoneNo", detailsRestro.PhoneNo),
                new SqlParameter("@Time", DateTime.Parse(detailsRestro.Time.ToString())), // Assuming detailsRestro.Time is TimeSpan
                new SqlParameter("@CloseTime", DateTime.Parse(detailsRestro.Time.ToString())), // Assuming detailsRestro.CloseTime is TimeSpan
                new SqlParameter("@Website", detailsRestro.Website),
                new SqlParameter("@Photo", detailsRestro.Photo)
            );*/
            _db.DetailsRestroo.Add(detailsRestro);
            _db.SaveChanges();
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
