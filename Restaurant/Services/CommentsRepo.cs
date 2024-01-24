using Restaurant.Data;
using Restaurant.Infrastructure;
using Restaurant.Models;

namespace Restaurant.Services
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
           _db.UserComments.Remove(comments);
        }

        public List<Comments> GetAll(int id)
        {
            return _db.UserComments.Where(x => x.RestroId == id).OrderByDescending(x => x.CmtId).ToList();
        }

        public void DelById(int id)
        {
            _db.UserComments.RemoveRange(_db.UserComments.Where(x => x.RestroId == id));
        }

        public void Insert(Comments comments)
        {
            _db.UserComments.Add(comments);
        }

        public void save()
        {
            _db.SaveChanges();
        }

        
    }
}
