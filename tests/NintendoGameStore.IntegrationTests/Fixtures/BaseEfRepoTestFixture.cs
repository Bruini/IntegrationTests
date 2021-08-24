using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NintendoGameStore.Core.Models;
using NintendoGameStore.Infrastructure.Data.Config;
using System;
using System.Data.Common;

namespace NintendoGameStore.IntegrationTests.Fixtures
{
    public class BaseEfRepoTestFixture : IDisposable
    {
        private static readonly object _lock = new object();
        public DbConnection Connection { get; }
        private static bool _databaseInitialized;
        private static string _sqlConnectionString = $"Data Source=localhost,1433;User ID=sa;Password=Abc_001!; Database=integration_tests ;Persist Security Info=true;";

        public BaseEfRepoTestFixture()
        {
            Connection = new SqlConnection(_sqlConnectionString);
            Seed();
            Connection.Open();
        }

        public StoreDBContext CreateContext(DbTransaction transaction = null)
        {
            var storeDBContext = new StoreDBContext(new DbContextOptionsBuilder<StoreDBContext>().UseSqlServer(@Connection).Options);

            if (transaction != null)
            {
                storeDBContext.Database.UseTransaction(transaction);
            }

            return storeDBContext;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var storeDBContext = CreateContext())
                    {
                        storeDBContext.Database.EnsureDeleted();
                        storeDBContext.Database.EnsureCreated();


                        var category1 = new Category("Action")
                        {
                            Id = Guid.Parse("5581ad6e-c040-4026-8bf9-c27a4e8cf9d9")
                        };

                        var category2 = new Category("Puzzle")
                        {
                            Id = Guid.Parse("36305232-2e6e-4b94-a69b-e654e796cdf3")
                        };

                        storeDBContext.Categories.Add(category1);
                        storeDBContext.Categories.Add(category2);

                        var game1 = new Game()
                        {
                            Id = Guid.Parse("073e74bf-5d44-4cdf-91c7-d5bedb37e007"),
                            Name = "Tetris",
                            Description = "Tetris is a video game developed in the Soviet Union in 1984",
                            NumberOfPlayers = 2,
                            Price = 10,
                            ReleaseDate = new DateTime(1984, 01, 15)
                        };
                        game1.AddCategory(category2);

                        var game2 = new Game()
                        {
                            Id = Guid.Parse("5919c228-53e0-4647-87e5-be5172a77097"),
                            Name = "Pong",
                            Description = "Pong is the first profitable video game in history",
                            NumberOfPlayers = 2,
                            Price = 5,
                            ReleaseDate = new DateTime(1972, 11, 29)
                        };
                        game2.AddCategory(category1);

                        storeDBContext.Games.Add(game1);
                        storeDBContext.Games.Add(game2);


                        storeDBContext.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
