using Microsoft.EntityFrameworkCore;
using NintendoGameStore.Core.Models;
using NintendoGameStore.Infrastructure.Data.Config;
using NintendoGameStore.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NintendoGameStore.Infrastructure.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly StoreDBContext _context;

        public GameRepository(StoreDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Game>> GetAllAsync(bool withCategoryList)
        {
            if (withCategoryList)
                return await _context.Games.Include(c => c.Categories)
                    .ToListAsync();

                return await _context.Games.ToListAsync();
        }

        public async Task<Game> GetByIdAsync(Guid id, bool withCategoryList)
        {
            if (withCategoryList)
                return await _context.Games.Include(c => c.Categories)
                    .FirstOrDefaultAsync(g => g.Id == id);

            return await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        }


        public async Task<Game> AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            return game;
        }

        public Game Update(Game game)
        {
            _context.Update(game);
            return game;
        }

        public void Delete(Game game)
        {
            _context.Games.Remove(game);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
