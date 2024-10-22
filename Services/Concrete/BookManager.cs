using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Model;
using Entities.RequestFeatures;
using Repositories.Abstract;
using Services.Abstract;

namespace Services.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ICategoryService _categoryService;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IBookLinks _bookLinks;
        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IBookLinks bookLinks, ICategoryService categoryService)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _bookLinks = bookLinks;
            _categoryService = categoryService;
        }

        public async Task< BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var category = _categoryService.GetOneCategoryByIdAsync(bookDto.CategoryId, false);

            var entity=_mapper.Map<Book>(bookDto);
            
            _manager.BookRepo.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id,trackChanges);

            _manager.BookRepo.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters,bool trackChanges)
        {
            if (!linkParameters.BookParameters.ValidPriceRange)
                throw new PriceOutOfRangeException();

            var booksWithMetaData=await _manager.BookRepo.GetAllBooksAsync(linkParameters.BookParameters,trackChanges);
            var booksDto=_mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
            var links = _bookLinks.TryGenerateLinks(booksDto, linkParameters.BookParameters.Fields, linkParameters.HttpContext);
            return (linkResponse:links,metaData: booksWithMetaData.MetaData);
        }

        public async Task< BookDto> GetOneBookAsync(int id, bool trackChanges)
        {
            var book = await _manager.BookRepo.GetOneBookbyIdAysnc(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<List<Book>> GetOneBookAsync(bool trackChanges)
        {
            var books= await _manager.BookRepo.GetAllBooksAsync(trackChanges);
            return books;
        }

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id,trackChanges);

            var bookDtoForUpdate=_mapper.Map<BookDtoForUpdate>(book);

            return (bookDtoForUpdate, book);
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges)
        {
            return await _manager.BookRepo.GetAllBooksWithDetailsAsync(trackChanges);
        }

        public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            _mapper.Map(bookDtoForUpdate, book);
             await _manager.SaveAsync();
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

            entity=_mapper.Map< Book>(bookDto);

            _manager.BookRepo.Update(entity);
            await _manager.SaveAsync();
        }

        private async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager.BookRepo.GetOneBookbyIdAysnc(id, trackChanges);

            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }
            return entity;
        }
    }
}
