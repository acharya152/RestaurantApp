using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ClassLibraryForRestro.Data;
using ClassLibraryForRestro.Infrastructure;
using ClassLibraryForRestro.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClassLibraryForRestro.Services
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
            SqlParameter id1 = new SqlParameter("@ID",detailsRestro.ID);
            _db.Database.ExecuteSqlRaw($"EXEC deleterestro @ID",id1);
        }

        public List<DetailsRestro> GetAll()
        {
            // return _db.DetailsRestroo.ToList();
            return _db.DetailsRestroo.FromSqlRaw($" EXEC select_all").ToList();
        }

        public DetailsRestro GetByID(int id)
        {
            SqlParameter id1 = new SqlParameter("@id",id);
            return _db.DetailsRestroo.FromSqlRaw($"EXEC getbyid @id",id1).AsEnumerable().FirstOrDefault();
            // return _db.DetailsRestroo.Where(x => x.ID == id).FirstOrDefault();
        }

        public void Insert(DetailsRestro detailsRestro)
        {
            SqlParameter param1 = new SqlParameter("@Name", detailsRestro.Name);
            SqlParameter param2 = new SqlParameter("@Location", detailsRestro.Location);
            SqlParameter param3 = new SqlParameter("@Description", detailsRestro.Description);
            SqlParameter param4 = new SqlParameter("@DetailedDescription", detailsRestro.DetailedDescription);
            SqlParameter param5 = new SqlParameter("@PhoneNo", detailsRestro.PhoneNo);
            SqlParameter param6 = new SqlParameter("@Time", detailsRestro.Time);
            SqlParameter param7 = new SqlParameter("@Website", detailsRestro.Website);
            SqlParameter param8 = new SqlParameter("@CloseTime", detailsRestro.CloseTime);
            SqlParameter param9 = new SqlParameter("@Photo", detailsRestro.Photo);

            _db.Database.ExecuteSqlRaw($"EXEC insertinto @Name, @Location, @Description, @DetailedDescription, @PhoneNo, @Time, @Website, @CloseTime, @Photo", param1, param2, param3, param4, param5, param6, param7, param8, param9);

            _db.SaveChanges();
            // _db.DetailsRestroo.Add(detailsRestro);
            
        }


        public void save()
        {
            _db.SaveChanges();
        }

        public void Update(DetailsRestro detailsRestro)
        {
            SqlParameter param1 = new SqlParameter("@Name", detailsRestro.Name);
            SqlParameter param2 = new SqlParameter("@Location", detailsRestro.Location);
            SqlParameter param3 = new SqlParameter("@Description", detailsRestro.Description);
            SqlParameter param4 = new SqlParameter("@DetailedDescription", detailsRestro.DetailedDescription);
            SqlParameter param5 = new SqlParameter("@PhoneNo", detailsRestro.PhoneNo);
            SqlParameter param6 = new SqlParameter("@Time", detailsRestro.Time);
            SqlParameter param7 = new SqlParameter("@Website", detailsRestro.Website);
            SqlParameter param8 = new SqlParameter("@CloseTime", detailsRestro.CloseTime);
            SqlParameter param9 = new SqlParameter("@Photo", detailsRestro.Photo);
            SqlParameter param10 = new SqlParameter("@ID", detailsRestro.ID);
            _db.Database.ExecuteSqlRaw($"EXEC updaterestro @ID, @Name, @Location, @Description, @DetailedDescription, @PhoneNo, @Time, @Website, @CloseTime, @Photo",param10, param1, param2, param3, param4, param5, param6, param7, param8, param9);
            _db.SaveChanges();
            //_db.DetailsRestroo.Update(detailsRestro);
        }
    }
}
