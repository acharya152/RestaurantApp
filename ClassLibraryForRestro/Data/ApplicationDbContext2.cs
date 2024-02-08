using Microsoft.EntityFrameworkCore;
using ClassLibraryForRestro.Models;

namespace ClassLibraryForRestro.Data
{
    public class ApplicationDbContext2:DbContext
    {
        public ApplicationDbContext2(DbContextOptions<ApplicationDbContext2> options) : base(options)
        {

        }

        public DbSet<DetailsRestro> DetailsRestroo { get; set; }
        public DbSet<Comments> UserComments { get; set; }
        public DbSet<Rating> UserRatings { get; set; }

    }
}
