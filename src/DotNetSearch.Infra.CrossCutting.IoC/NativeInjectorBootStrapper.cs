using DotNetSearch.Application.AutoMapper;
using DotNetSearch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetSearch.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services, 
                                            IConfiguration configuration)
        {
            // Infra Data - Context
            services.AddDbContext<DotNetSearchDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<DotNetSearchDbContext>();

            // Infra Data - Repositories

            // Application - AutoMapper
            services.AddAutoMapper(typeof(DotNetSearchMappingProfile));

            // Application - AppServices
        }
    }
}
