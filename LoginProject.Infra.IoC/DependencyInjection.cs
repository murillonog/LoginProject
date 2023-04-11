using LoginProject.Application.Interfaces;
using LoginProject.Application.Mappings;
using LoginProject.Application.Services;
using LoginProject.Domain.Interfaces;
using LoginProject.Infra.Data.Context;
using LoginProject.Infra.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoginProject.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
            ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IUserManagerService, UserManagerService>();

            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));

            return services;
        }
    }
}
