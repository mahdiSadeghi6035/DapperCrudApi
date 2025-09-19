using CRUD.Common;
using CRUD.Context;
using CRUD.Interfaces;
using CRUD.Repositories;

namespace CRUD;

public static class WebDependencyInjection
{
    public static void RegisterDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.RegisterSwagger();
    }
}
