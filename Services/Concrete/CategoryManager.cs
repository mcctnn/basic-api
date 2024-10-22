using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Model;
using Repositories.Abstract;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CategoryManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateOneCategoryAsync(CategoryDtoForInsertion categoryDto)
        {
            var entity=_mapper.Map<Category>(categoryDto);
            _manager.CategoryRepo.CreateOneCategory(entity);
            await _manager.SaveAsync();
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task DeleteOneCategoryAsync(int id, bool trackChanges)
        {
            var entity=await GetOneCategoryByIdAndCheckExistsAsync(id,trackChanges);

            _manager.CategoryRepo.DeleteOneCategory(entity);
            await _manager.SaveAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            return await _manager.CategoryRepo.GetAllCategoriesAsync(trackChanges);
        }

        public async Task<CategoryDto> GetOneCategoryByIdAsync(int id, bool trackChanges)
        {
            var category = await _manager.CategoryRepo.GetCategoryByIdAsync(id, trackChanges);
            if(category is null)
                throw new CategoryNotFoundException(id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<(CategoryDtoForUpdate categoryDtoForUpdate, Category category)> GetOneCategoryForPatchAsync(int id, bool trackChanges)
        {
            var category = await GetOneCategoryByIdAndCheckExistsAsync(id, trackChanges);

            var categoryDtoForUpdate = _mapper.Map<CategoryDtoForUpdate>(category);

            return (categoryDtoForUpdate, category);
        }

        public async Task SaveChangesForPatchAsync(CategoryDtoForUpdate categoryDtoForUpdate, Category category)
        {
            _mapper.Map(categoryDtoForUpdate, category);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneCategoryAsync(int id, CategoryDtoForUpdate categoryDto, bool trackChanges)
        {
            var entity = await GetOneCategoryByIdAndCheckExistsAsync(id, trackChanges);

            entity = _mapper.Map<Category>(categoryDto);

            _manager.CategoryRepo.Update(entity);
            await _manager.SaveAsync();
        }
        private async Task<Category> GetOneCategoryByIdAndCheckExistsAsync(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager.CategoryRepo.GetCategoryByIdAsync(id, trackChanges);

            if (entity is null)
            {
                throw new CategoryNotFoundException(id);
            }
            return entity;
        }
    }
}
