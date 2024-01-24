using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Data
{
    public class ApplicationDbContext2:DbContext
    {
        public ApplicationDbContext2(DbContextOptions<ApplicationDbContext2> options) : base(options)
        {

        }

        public DbSet<DetailsRestro> DetailsRestroo { get; set; }
        public DbSet<Comments> UserComments { get; set; }

    }
}
