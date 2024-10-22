using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController:ControllerBase
    {
        private readonly IServiceManager _services;

        public CategoryController(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet(Name ="GetAllCategoriesAsync")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            return Ok(await _services.CategoryService.GetAllCategoriesAsync(false));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllCategoriesAsync([FromRoute]int id)
        {
            var category = await _services.
                CategoryService.
                GetOneCategoryByIdAsync(id, false);
            return Ok(category);
        }

        [HttpPost(Name ="CreateOneCategoryAsync")]
        public async Task<IActionResult> CreateOneCategoryAsync([FromBody] CategoryDtoForInsertion categoryDto)
        {
            var category= await _services.CategoryService.CreateOneCategoryAsync(categoryDto);
            return StatusCode(201,category);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneCategoryAsync([FromRoute(Name = "id")] int id,
            [FromBody] CategoryDtoForUpdate categoryDto)
        {
            if (categoryDto is null)
                return NotFound();
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _services.CategoryService.UpdateOneCategoryAsync(id, categoryDto, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
        {
            await _services.CategoryService.DeleteOneCategoryAsync(id, false);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<CategoryDtoForUpdate> categoryPatch)
        {

            if (categoryPatch is null) return BadRequest();
            //checking entity
            var result = await _services.CategoryService.GetOneCategoryForPatchAsync(id, true);

            categoryPatch.ApplyTo(result.categoryDtoForUpdate, ModelState);

            TryValidateModel(result.categoryDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity();
            await _services.CategoryService.SaveChangesForPatchAsync(result.categoryDtoForUpdate, result
                .category);

            return NoContent();

        }

        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,DELETE,HEAD,OPTIONS,PATCH");
            return Ok();
        }
    }
}
