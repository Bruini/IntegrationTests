using NintendoGameStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NintendoGameStore.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task<Category> AddAsync(Category category);
        Category Update(Category category);
        void Delete(Category category);
        Task SaveChangesAsync();
    }
}
