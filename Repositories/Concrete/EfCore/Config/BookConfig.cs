﻿using Entities.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete.EfCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                  new Book { Id = 1, CategoryId=1,Title = "Karagöz ve Hacivat", Price = 75 },
                  new Book { Id = 2, CategoryId=2,Title = "Mesnevi", Price = 100 },
                  new Book { Id = 3, CategoryId=2,Title = "Nutuk", Price = 120 }
            );
        }
    }
}
