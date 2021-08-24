using NintendoGameStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NintendoGameStore.Infrastructure.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync(bool withCategoryList);
        Task<Game> GetByIdAsync(Guid id, bool withCategoryList);
        Task<Game> AddAsync(Game game);
        Game Update(Game game);
        void Delete(Game game);
        Task SaveChangesAsync();
    }
}