using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaleSystem.DAL.DBContext;

namespace SaleSystem.IOC
{
    public static class Dependency
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbsaleContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("stringSQL"));
            });
        }
    }
}
