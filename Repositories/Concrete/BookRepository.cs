using Entities.Model;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;
using Repositories.Concrete.EfCore;
using Repositories.Concrete.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public void CreateOneBook(Book book)=>Create(book);
        public void DeleteOneBook(Book book)=>Delete(book);
        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters,bool trackChanges)
        {
            var books=await FindAll(trackChanges)
            .FilterBooks(bookParameters.MinPrice,bookParameters.MaxPrice)
            .Search(bookParameters.SearchTerm)
            .Sort(bookParameters.OrderBy)
            .ToListAsync();

            return PagedList<Book>.ToPagedList(books,
                bookParameters.PageNumber,
                bookParameters.PageSize);
        }

        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(b=>b.Id).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges)
        {
            return await _repositoryContext
                .Books
                .Include(b => b.Category)
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        public async Task<Book> GetOneBookbyIdAysnc(int id, bool trackChanges) =>
        await FindByCondition(b => b.Id.Equals(id),trackChanges).SingleOrDefaultAsync();
        public void UpdateOneBook(Book book)=>Update(book);  
    }
}
