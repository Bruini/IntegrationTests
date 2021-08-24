using System;
using System.Collections.Generic;

namespace NintendoGameStore.Core.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; }

        public Category(string name)
        {
            this.Name = name;
            this.Games = new List<Game>();
        }
        public void UpdateName(string name)
        {
            this.Name = name;
        }

        public void AddGame(Game game)
        {
            this.Games.Add(game);
        }
    }
}
