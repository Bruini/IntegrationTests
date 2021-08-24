using Microsoft.Extensions.DependencyInjection;
using NintendoGameStore.Infrastructure.AmiiboAPI.Endpoints;
using NintendoGameStore.Infrastructure.AmiiboAPI.Interfaces;

namespace NintendoGameStore.Infrastructure.AmiiboAPI.IoC
{
    public class AmiiboAPIIoC
    {
        public void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IAmiiboEndpoint, AmiiboEndpoint>();
        }
    }
}
