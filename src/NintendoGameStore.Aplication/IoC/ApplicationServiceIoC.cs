using Microsoft.Extensions.DependencyInjection;
using NintendoGameStore.Aplication.Interfaces;
using NintendoGameStore.Aplication.Services;

namespace NintendoGameStore.Aplication.IoC
{
    public class ApplicationServiceIoC
    {
        public void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
