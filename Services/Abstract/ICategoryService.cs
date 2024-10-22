using Entities.DataTransferObjects;
using Entities.Model;

namespace Services.Abstract
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<CategoryDto> GetOneCategoryByIdAsync(int id ,bool trackChanges);
        Task<CategoryDto> CreateOneCategoryAsync(CategoryDtoForInsertion category);
        Task<(CategoryDtoForUpdate categoryDtoForUpdate, Category category)> GetOneCategoryForPatchAsync(int id, bool trackChanges);
        Task UpdateOneCategoryAsync(int id ,CategoryDtoForUpdate categoryDto,bool trackChanges);
        Task DeleteOneCategoryAsync(int id,bool trackChanges);
        Task SaveChangesForPatchAsync(CategoryDtoForUpdate categoryDtoForUpdate, Category category);
    }
}
