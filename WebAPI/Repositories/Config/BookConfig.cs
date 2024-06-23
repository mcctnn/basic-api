using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Model;

namespace WebAPI.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                  new Book { Id = 1, Title = "Karagöz ve Hacivat", Price = 75 },
                  new Book { Id = 2, Title = "Mesnevi", Price = 100 },
                  new Book { Id = 3, Title = "Nutuk", Price = 120 }
            );
        }
    }
}
