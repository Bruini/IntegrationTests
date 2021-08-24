using System;
using System.Collections.Generic;

namespace NintendoGameStore.Core.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberOfPlayers { get; set; }
        public List<Category> Categories { get; set; }

        public Game()
        {
            this.Categories = new List<Category>();
        }
        public void AddCategory(Category category)
        {
            this.Categories.Add(category);
        }
    }
}
