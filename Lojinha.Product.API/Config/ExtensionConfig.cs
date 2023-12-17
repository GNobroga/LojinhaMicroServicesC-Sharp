namespace Lojinha.Product.API.Config;

public static class ExtensionConfig
{
    public static IServiceCollection AddRepositoriesWithServices(this IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemService, ItemService>();
        return services;
    }
}