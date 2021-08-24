using NintendoGameStore.Infrastructure.AmiiboAPI.Models;
using System.Threading.Tasks;

namespace NintendoGameStore.Infrastructure.AmiiboAPI.Interfaces
{
    public interface IAmiiboEndpoint
    {
        Task<AmiibosJson> GetAmiibosAsync();
        Task<AmiibosJson> GetAmiibosByNameAsync(string name);
    }
}
