using Microsoft.EntityFrameworkCore;
using ClassLibraryForRestro.Models;

namespace ClassLibraryForRestro.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<DetailsRestro> DetailsRestroo {  get; set; }
    }
}
