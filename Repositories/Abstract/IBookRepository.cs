﻿using Entities.Model;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters ,bool trackChanges);
        Task<List<Book>> GetAllBooksAsync(bool trackChanges);
        Task<Book> GetOneBookbyIdAysnc(int id,bool trackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
        Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges);
    }
}
