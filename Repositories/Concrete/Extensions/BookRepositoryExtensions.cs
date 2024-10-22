using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repositories.Concrete.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book>
            FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>
            books.Where(
            book => book.Price >= minPrice && book.Price <= maxPrice);

        public static IQueryable<Book> Search(this IQueryable<Book> books,string searchterm)
        {
            if(string.IsNullOrWhiteSpace(searchterm))
                return books;

            var lowerCaseTerm=searchterm.Trim().ToLower();

            return books.
                Where(b => b.Title.ToLower().Contains(searchterm));
        }
        public static IQueryable<Book> Sort(this IQueryable<Book> books,string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b => b.Id);

            var orderQuery=OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

            if(orderQuery is null)
                return books.OrderBy(b=>b.Id);

            return books.OrderBy(orderQuery);
        }
    }
}
