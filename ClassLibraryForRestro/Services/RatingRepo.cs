using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public void Delete(int id)
        {
            SqlParameter param = new SqlParameter("@RestroId",id);
            _db.Database.ExecuteSqlRaw($"EXEC deleteallrate @RestroId",param);
            // _db.UserRatings.RemoveRange(_db.UserRatings.Where(x =>x.RestroID==id));
           
        }

        public List<Rating> GetAll(int id)
        {
            try
            {
                SqlParameter param = new SqlParameter("@RestroId", id);
                return _db.UserRatings.FromSqlRaw($"EXEC getrate @RestroId", param).ToList();
                // return _db.UserRatings.Where(x =>x.RestroID==id).ToList();
            }catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Insert(Rating rating)
        {
            SqlParameter param1 = new SqlParameter("@RestroId",rating.RestroID);
            SqlParameter param2 = new SqlParameter("@Ratings", rating.Ratings);
            _db.Database.ExecuteSqlRaw($"EXEC insertrate @RestroId,@Ratings",param1,param2);
           // _db.Add(rating);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
