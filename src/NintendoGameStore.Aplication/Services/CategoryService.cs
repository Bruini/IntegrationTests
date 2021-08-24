
using NintendoGameStore.Aplication.Inputs;
using NintendoGameStore.Aplication.Interfaces;
using NintendoGameStore.Core.Models;
using NintendoGameStore.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NintendoGameStore.Aplication.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> InsertOrUpdateAsync(CategoryInput categoryInput)
        {
            if (categoryInput.Id.HasValue)
                return await UpdateCategoryAsync(categoryInput);

            return await InsertCategoryAsync(categoryInput);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await this._categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NullReferenceException($"{nameof(Category)} not found");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
        }

        private async Task<Category> InsertCategoryAsync(CategoryInput categoryInput)
        {
            if (string.IsNullOrWhiteSpace(categoryInput.Name))
                throw new ArgumentNullException();

            var category = new Category(categoryInput.Name);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return category;
        }

        private async Task<Category> UpdateCategoryAsync(CategoryInput categoryInput)
        {
            var category = await this._categoryRepository.GetByIdAsync(categoryInput.Id.Value);
            if (category != null)
            {
                if (string.IsNullOrWhiteSpace(categoryInput.Name))
                    throw new ArgumentNullException();

                category.UpdateName(categoryInput.Name);
                _categoryRepository.Update(category);
                await _categoryRepository.SaveChangesAsync();
                return category;
            }
            else throw new NullReferenceException($"{nameof(Category)} not found");
        }
    }
}
