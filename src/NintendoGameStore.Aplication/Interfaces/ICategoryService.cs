using NintendoGameStore.Aplication.Inputs;
using NintendoGameStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NintendoGameStore.Aplication.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task<Category> InsertOrUpdateAsync(CategoryInput categoryInput);
        Task DeleteAsync(Guid id);
    }
}
