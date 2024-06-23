using Microsoft.EntityFrameworkCore;
using WebAPI.Model;
using WebAPI.Repositories.Config;

namespace WebAPI.Repositories
{
    public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
