using NintendoGameStore.Aplication.Inputs;
using NintendoGameStore.Aplication.Interfaces;
using NintendoGameStore.Aplication.Services;
using NintendoGameStore.Core.Models;
using NintendoGameStore.Infrastructure.Data.Repositories;
using NintendoGameStore.Infrastructure.Interfaces;
using NintendoGameStore.IntegrationTests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NintendoGameStore.IntegrationTests.Services
{
    public class CategoryServiceIntegrationTest : IClassFixture<BaseEfRepoTestFixture>
    {
        public BaseEfRepoTestFixture _efRepoTestFixture { get; }
        private ICategoryService _categoryService;
        private ICategoryRepository _categoryRepository;
        public CategoryServiceIntegrationTest(BaseEfRepoTestFixture efRepoTestFixture)
        {
            _efRepoTestFixture = efRepoTestFixture;
        }

        [Fact]
        public async Task GetById_Should_Returns_One_By_Id()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                _categoryRepository = new CategoryRepository(_efRepoTestFixture.CreateContext());
                _categoryService = new CategoryService(_categoryRepository);

                Guid uuid = Guid.Parse("36305232-2e6e-4b94-a69b-e654e796cdf3");

                var category = await _categoryService.GetByIdAsync(uuid);

                Assert.NotNull(category);
                Assert.Equal("Puzzle", category.Name);
            }
        }

        [Fact]
        public async Task GetAll_Should_Returns_All()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                _categoryRepository = new CategoryRepository(_efRepoTestFixture.CreateContext());
                _categoryService = new CategoryService(_categoryRepository);

                var categories = await _categoryService.GetAllAsync();

                Assert.NotNull(categories);
                Assert.Equal(2, categories.Count());
            }
        }

        [Fact]
        public async Task InsertOrUpdate_Should_Insert_And_Returns_Category()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                _categoryRepository = new CategoryRepository(_efRepoTestFixture.CreateContext());
                _categoryService = new CategoryService(_categoryRepository);

                var categoryInput = new CategoryInput { Name = "Category Test" };

                var category = await _categoryService.InsertOrUpdateAsync(categoryInput);

                Assert.NotNull(category);
                Assert.IsType<Category>(category);
                Assert.Equal(categoryInput.Name, category.Name);
                Assert.NotEqual(Guid.Empty, category.Id);       
          
                await ClearData(category);
            }
        }

        [Fact]
        public async Task InsertOrUpdate_Should_Update_And_Returns_Category()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                _categoryRepository = new CategoryRepository(_efRepoTestFixture.CreateContext());
                _categoryService = new CategoryService(_categoryRepository);

                var categoryInput = new CategoryInput { Name = "Category Test" };

                var category = await _categoryService.InsertOrUpdateAsync(categoryInput);

                categoryInput.Id = category.Id;
                categoryInput.Name = "Test Category";

                var categoryUpdated = await _categoryService.InsertOrUpdateAsync(categoryInput);

                Assert.NotNull(categoryUpdated);
                Assert.IsType<Category>(category);
                Assert.Equal("Test Category", categoryUpdated.Name);
                Assert.Equal(category.Id, categoryUpdated.Id);             

                await ClearData(category);
            }
        }

        [Fact]
        public async Task InsertOrUpdate_Should_Not_Insert_And_Returns_ArgumentNullException()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                _categoryRepository = new CategoryRepository(_efRepoTestFixture.CreateContext());
                _categoryService = new CategoryService(_categoryRepository);

                var categoryInput = new CategoryInput { Name = "" };
                await Assert.ThrowsAsync<ArgumentNullException>(() => _categoryService.InsertOrUpdateAsync(categoryInput));
            }
        }

        [Fact]
        public async Task InsertOrUpdate_Should_Not_Update_And_Returns_NullReferenceException()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                _categoryRepository = new CategoryRepository(_efRepoTestFixture.CreateContext());
                _categoryService = new CategoryService(_categoryRepository);

                var categoryInput = new CategoryInput { Id= Guid.Empty, Name = "Category Test" };

                var ex = await Assert.ThrowsAsync<NullReferenceException>(() => _categoryService.InsertOrUpdateAsync(categoryInput));
                Assert.Equal($"{nameof(Category)} not found", ex.Message);
            }
        }

        [Fact]
        public async Task DeleteAsync_Should_Not_Found_And_Returns_NullReferenceException()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                _categoryRepository = new CategoryRepository(_efRepoTestFixture.CreateContext());
                _categoryService = new CategoryService(_categoryRepository);

                var ex = await Assert.ThrowsAsync<NullReferenceException>(() => _categoryService.DeleteAsync(Guid.Empty));
                Assert.Equal($"{nameof(Category)} not found", ex.Message);
            }
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                _categoryRepository = new CategoryRepository(_efRepoTestFixture.CreateContext());
                _categoryService = new CategoryService(_categoryRepository);

                var categoryInput = new CategoryInput { Name = "New Category" };

                var category = await _categoryService.InsertOrUpdateAsync(categoryInput);

                Assert.NotNull(category);

                await _categoryService.DeleteAsync(category.Id);

                var result = await _categoryRepository.GetByIdAsync(category.Id);
                Assert.Null(result);
            }
        }

        private async Task ClearData(Category category)
        {
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
