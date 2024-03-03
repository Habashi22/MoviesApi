using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                

        }

        public DbSet<genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }


    }
}
