using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;
        public BooksController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _repositoryContext.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        [HttpGet("{id:int}")]
        public IActionResult GetBook([FromRoute(Name ="id")]int id) 
        {
            try
            {
                var book = _repositoryContext.Books
                .Find(id);

                if (book == null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if(book is null)
                    return BadRequest();

                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody]Book book)
        {
            try
            {
                //check book?
                var updatedEntity= _repositoryContext.Books
                    .Where(x => x.Id == id).
                    SingleOrDefault();

                if (updatedEntity == null)
                    return NotFound();

                //checking id

                if (id!=book.Id)
                    return BadRequest();

                updatedEntity.Title = book.Title;
                updatedEntity.Price = book.Price;

                _repositoryContext.SaveChanges();
                return Ok(book);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var deletedEntity=_repositoryContext.Books
                    .Where(b=>b.Id.Equals(id))
                    .SingleOrDefault();

                if(deletedEntity == null)
                    return NotFound(
                        new
                        {
                            StatusCode=404,
                            message=$"Book with id:{id} could not found."
                        });
                  
                 _repositoryContext.Books.Remove(deletedEntity);
                _repositoryContext.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody]JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                //checking entity
                var entity = _repositoryContext.Books
                  .Where(b => b.Id.Equals(id))
                  .SingleOrDefault();

                if (entity == null)
                    return NotFound();

                bookPatch.ApplyTo(entity);
                _repositoryContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
