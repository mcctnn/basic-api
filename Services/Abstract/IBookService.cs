﻿using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Model;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IBookService
    {
        Task<(LinkResponse linkResponse,MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters,bool trackChanges);
        Task<BookDto> GetOneBookAsync(int id,bool trackChanges);
        Task<BookDto> CreateOneBookAsync(BookDtoForInsertion book);
        Task DeleteOneBookAsync(int id, bool trackChanges);
        Task UpdateOneBookAsync(int id ,BookDtoForUpdate bookDto,bool trackChanges);
        Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book);
        Task<List<Book>> GetOneBookAsync(bool trackChanges);
        Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges);
    }
}
