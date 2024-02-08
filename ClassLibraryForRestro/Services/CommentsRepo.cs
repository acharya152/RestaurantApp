using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ClassLibraryForRestro.Data;
using ClassLibraryForRestro.Infrastructure;
using ClassLibraryForRestro.Models;
using System.Xml.Linq;

namespace ClassLibraryForRestro.Services
{
    public class CommentsRepo : IComments
    {
        private readonly ApplicationDbContext2 _db;
        public CommentsRepo(ApplicationDbContext2 db)
        {
                _db= db;
        }

        public void Delete(Comments comments)
        {
            SqlParameter param1 = new SqlParameter("@RestroId",comments.RestroId);
            _db.Database.ExecuteSqlRaw($"EXEC deletecomment @RestroID",param1);
        }

        public List<Comments> GetAll(int id)
        {

            try
            {
                SqlParameter param1 = new SqlParameter("@RestroId", id);
                return _db.UserComments.FromSqlRaw($"EXEC getallcmt @RestroId", param1).ToList();
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
          

            //return _db.UserComments.Where(x => x.RestroId == id).OrderByDescending(x => x.CmtId).ToList();
        }

        public void DelById(int id)
        {
            SqlParameter param1 = new SqlParameter("@RestroId", id);
            _db.Database.ExecuteSqlRaw($"EXEC deletecomment @RestroID", param1);
            //_db.UserComments.RemoveRange(_db.UserComments.Where(x => x.RestroId == id));
        }

        public void Insert(Comments comments)
        {
            SqlParameter param1 = new SqlParameter("@UserId",comments.UserId);
            SqlParameter param2 = new SqlParameter("@RestroId", comments.RestroId);
            SqlParameter param3 = new SqlParameter("@Content", comments.Content);
            SqlParameter param4 = new SqlParameter("@UserName", comments.UserName);
            _db.Database.ExecuteSqlRaw($"EXEC insertcmt @UserId,@RestroId,@Content,@UserName ",param1,param2,param3,param4);

            _db.SaveChanges();
        }

        public void save()
        {
            _db.SaveChanges();
        }

        
    }
}
