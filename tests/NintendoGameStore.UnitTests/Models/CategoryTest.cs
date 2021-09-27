using NintendoGameStore.Core.Models;
using System.Linq;
using Xunit;

namespace NintendoGameStore.UnitTests.Models
{
    public class CategoryTest
    {
        [Fact]
        public void CategoryConstructor_Should_Instantiated()
        {
            var category = new Category("Test Name");

            Assert.NotNull(category);
            Assert.Equal("Test Name", category.Name);
            Assert.NotNull(category.Games);
            Assert.Empty(category.Games);
        }

        [Fact]
        public void AddGame_Should_Add_Item_List()
        {
            var category = new Category("Test Name");
            var game = new Game
            {
                Name = "Test Game"
            };

            category.AddGame(game);

            Assert.NotNull(category.Games);
            Assert.NotEmpty(category.Games);
            Assert.Single(category.Games);
            Assert.Equal(game.Name, category.Games.FirstOrDefault().Name);
        }

        [Fact]
        public void UpdateName_Should_Update_Name()
        {
            var category = new Category("Test Name");

            category.UpdateName("abc");

            Assert.NotNull(category);
            Assert.Equal("abc", category.Name);
        }
    }
}
