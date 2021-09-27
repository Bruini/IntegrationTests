using NintendoGameStore.Core.Models;
using NintendoGameStore.Infrastructure.Data.Repositories;
using NintendoGameStore.IntegrationTests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NintendoGameStore.IntegrationTests.Repositories
{
    public class GameRepositoryIntegrationTest : IClassFixture<BaseEfRepoTestFixture>
    {
        public BaseEfRepoTestFixture _efRepoTestFixture { get; }

        public GameRepositoryIntegrationTest(BaseEfRepoTestFixture efRepoTestFixture)
        {
            _efRepoTestFixture = efRepoTestFixture;
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_One_By_Id()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var gameRepository = new GameRepository(context);

                Guid uuid = Guid.Parse("073e74bf-5d44-4cdf-91c7-d5bedb37e007");

                var gameFromDb = await gameRepository.GetByIdAsync(uuid, false);

                Assert.NotNull(gameFromDb);
                Assert.Equal("Tetris", gameFromDb.Name);
                Assert.Equal("Tetris is a video game developed in the Soviet Union in 1984", gameFromDb.Description);
                Assert.Equal(2, gameFromDb.NumberOfPlayers);
                Assert.Equal(10, gameFromDb.Price);
                Assert.Equal(new DateTime(1984, 01, 15), gameFromDb.ReleaseDate);
                Assert.Empty(gameFromDb.Categories);
            }
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_One_By_Id_With_Category_List()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var gameRepository = new GameRepository(context);

                Guid uuid = Guid.Parse("073e74bf-5d44-4cdf-91c7-d5bedb37e007");

                var gameFromDb = await gameRepository.GetByIdAsync(uuid, true);

                Assert.NotNull(gameFromDb);
                Assert.Equal("Tetris", gameFromDb.Name);
                Assert.Equal("Tetris is a video game developed in the Soviet Union in 1984", gameFromDb.Description);
                Assert.Equal(2, gameFromDb.NumberOfPlayers);
                Assert.Equal(10, gameFromDb.Price);
                Assert.Equal(new DateTime(1984, 01, 15), gameFromDb.ReleaseDate);
                Assert.Single(gameFromDb.Categories);
                Assert.Equal("Puzzle", gameFromDb.Categories.FirstOrDefault().Name);
            }
        }

        [Fact]
        public async Task GetAllAsync_Should_Returns_All()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var gameRepository = new GameRepository(context);

                var games = await gameRepository.GetAllAsync(false);

                Assert.NotNull(games);
                Assert.Equal(2, games.Count());
            }
        }

        [Fact]
        public async Task GetAllAsync_Should_Returns_All_With_Category_List()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var gameRepository = new GameRepository(context);

                var games = await gameRepository.GetAllAsync(true);

                Assert.NotNull(games);
                Assert.Equal(2, games.Count());
                foreach (var game in games)
                {
                    Assert.Single(game.Categories);
                }
            }
        }

        [Fact]
        public async Task AddAsync_Should_AddGame()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var gameRepository = new GameRepository(context);

                var uuid = Guid.NewGuid();
                var game = new Game()
                {
                    Id = uuid,
                    Name = "Super Mario",
                    Description = "Super Mario is a video game developed by Nintendo",
                    NumberOfPlayers = 1,
                    Price = 59,
                    ReleaseDate = new DateTime(1984, 01, 15)
                };

                await gameRepository.AddAsync(game);
                await context.SaveChangesAsync();

                var gameFromDb = await gameRepository.GetByIdAsync(uuid, false);

                Assert.NotNull(gameFromDb);
                Assert.Equal(game.Name, gameFromDb.Name);
                Assert.Equal(game.Description, gameFromDb.Description);
                Assert.Equal(game.NumberOfPlayers, gameFromDb.NumberOfPlayers);
                Assert.Equal(game.Price, gameFromDb.Price);
                Assert.Equal(game.ReleaseDate, gameFromDb.ReleaseDate);
                Assert.Empty(gameFromDb.Categories);

                await ClearData(gameRepository, gameFromDb);
            }
        }

        [Fact]
        public async Task Delete_Should_DeleteGame()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var gameRepository = new GameRepository(context);

                var uuid = Guid.NewGuid();
                var game = new Game()
                {
                    Id = uuid,
                    Name = "Fifa 21",
                    Description = "Fifa is a video game developed by EA",
                    NumberOfPlayers = 2,
                    Price = 59,
                    ReleaseDate = new DateTime(1921, 01, 15)
                };

                await gameRepository.AddAsync(game);
                await context.SaveChangesAsync();

                var gameFromDb = await gameRepository.GetByIdAsync(uuid, false);

                Assert.NotNull(gameFromDb);
                Assert.Empty(gameFromDb.Categories);

                gameRepository.Delete(gameFromDb);
                await context.SaveChangesAsync();

                var gameFromDbDeleted = await gameRepository.GetByIdAsync(uuid, false);
                Assert.Null(gameFromDbDeleted);
            }
        }

        [Fact]
        public async Task Update_Should_Update_Game()
        {
            using (var context = _efRepoTestFixture.CreateContext())
            {
                var gameRepository = new GameRepository(context);

                var uuid = Guid.NewGuid();
                var game = new Game()
                {
                    Id = uuid,
                    Name = "Fifa 21",
                    Description = "Fifa is a video game developed by EA",
                    NumberOfPlayers = 2,
                    Price = 59,
                    ReleaseDate = new DateTime(1921, 01, 15)
                };

                await gameRepository.AddAsync(game);
                await context.SaveChangesAsync();

                game.Name = "Fifa 22";
                game.Description = "N/A";
                game.NumberOfPlayers = 4;

                var gameUpdated = gameRepository.Update(game);

                Assert.NotNull(gameUpdated);
                Assert.Equal(uuid, gameUpdated.Id);
                Assert.Equal("Fifa 22", gameUpdated.Name);
                Assert.Equal("N/A", gameUpdated.Description);
                Assert.Equal(4, gameUpdated.NumberOfPlayers);
                Assert.Equal(game.Price, gameUpdated.Price);
                Assert.Equal(game.ReleaseDate, gameUpdated.ReleaseDate);

                await ClearData(gameRepository, gameUpdated);
            }
        }
        private async Task ClearData(GameRepository gameRepository, Game game)
        {
            gameRepository.Delete(game);
            await gameRepository.SaveChangesAsync();
        }
    }
}
