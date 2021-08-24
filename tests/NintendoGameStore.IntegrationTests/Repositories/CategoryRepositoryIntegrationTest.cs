using NintendoGameStore.Core.Models;
using NintendoGameStore.Infrastructure.Data.Repositories;
using NintendoGameStore.IntegrationTests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NintendoGameStore.IntegrationTests.Repositories
{
    public class CategoryRepositoryIntegrationTest : IClassFixture<BaseEfRepoTestFixture>
    {
        public BaseEfRepoTestFixture _efRepoTestFixture { get; }
        public CategoryRepositoryIntegrationTest(BaseEfRepoTestFixture efRepoTestFixture)
        {
            _efRepoTestFixture = efRepoTestFixture;
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_One_By_Id()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var categoryRepository = new CategoryRepository(context);

                Guid uuid = Guid.Parse("36305232-2e6e-4b94-a69b-e654e796cdf3");

                var categoryFromDb = await categoryRepository.GetByIdAsync(uuid);

                Assert.NotNull(categoryFromDb);
                Assert.Equal("Puzzle", categoryFromDb.Name);
            }

        }

        [Fact]
        public async Task GetAllAsync_Should_Returns_All()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var categoryRepository = new CategoryRepository(context);

                var categories = await categoryRepository.GetAllAsync();

                Assert.NotNull(categories);
                Assert.Equal(2, categories.Count());
            }
        }

        [Fact]
        public async Task AddAsync_Should_Add_Category()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var categoryRepository = new CategoryRepository(context);

                var uuid = Guid.NewGuid();
                var category1 = new Category("Sport")
                {
                    Id = uuid
                };

                await categoryRepository.AddAsync(category1);
                await context.SaveChangesAsync();

                var categoryFromDb = await categoryRepository.GetByIdAsync(uuid);

                Assert.NotNull(categoryFromDb);
                Assert.Equal(category1.Name, categoryFromDb.Name);

                await ClearData(categoryRepository, categoryFromDb);
            }
        }

        [Fact]
        public async Task Delete_Should_DeleteCategory()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var categoryRepository = new CategoryRepository(context);

                var uuid = Guid.NewGuid();
                var category1 = new Category("Sport")
                {
                    Id = uuid
                };

                await categoryRepository.AddAsync(category1);
                await context.SaveChangesAsync();

                var categoryFromDb = await categoryRepository.GetByIdAsync(uuid);

                Assert.NotNull(categoryFromDb);
                Assert.Equal(category1.Name, categoryFromDb.Name);

                categoryRepository.Delete(categoryFromDb);
                await context.SaveChangesAsync();

                var categoryFromDbDeleted = await categoryRepository.GetByIdAsync(uuid);
                Assert.Null(categoryFromDbDeleted);
            }
        }

        [Fact]
        public async Task Update_Should_Update_Name_Category()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var categoryRepository = new CategoryRepository(context);

                var uuid = Guid.NewGuid();
                var category1 = new Category("Sport")
                {
                    Id = uuid
                };

                await categoryRepository.AddAsync(category1);
                await context.SaveChangesAsync();

                var categoryFromDb = await categoryRepository.GetByIdAsync(uuid);

                Assert.NotNull(categoryFromDb);
                Assert.Equal(category1.Name, categoryFromDb.Name);

                var updatedName = "RPG";
                categoryFromDb.UpdateName(updatedName);

                categoryRepository.Update(categoryFromDb);
                await context.SaveChangesAsync();

                var categoryFromDbUpdated = await categoryRepository.GetByIdAsync(uuid);

                Assert.NotNull(categoryFromDbUpdated);
                Assert.Equal(updatedName, categoryFromDbUpdated.Name);

                await ClearData(categoryRepository, categoryFromDbUpdated);
            }
        }

        private async Task ClearData(CategoryRepository categoryRepository, Category category)
        {
            categoryRepository.Delete(category);
            await categoryRepository.SaveChangesAsync();
        }

    }
}
