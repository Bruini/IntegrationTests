using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NintendoGameStore.Infrastructure.Data.Config;
using NintendoGameStore.Infrastructure.Data.Repositories;
using NintendoGameStore.Infrastructure.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace NintendoGameStore.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public class RepositoryIoC
    {
        public void ChildServiceRegister(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDBContext>(options =>
                   options.UseSqlServer(configuration.GetSection("ConnectionString").Value,
                   optionsBuilder => optionsBuilder.MigrationsAssembly("NintendoGameStore.Migrations")));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
        }
    }
}
