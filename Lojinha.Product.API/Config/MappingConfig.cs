using AutoMapper;

namespace Lojinha.Product.API.Config;

public static class MappingConfig
{
    public static IServiceCollection AddRegisterMaps(this IServiceCollection services)
    {
        var config = new MapperConfiguration(opt => {
            opt.CreateMap<Item, ItemVO>().ReverseMap();
        });

        IMapper mapper = new Mapper(config);

        services.AddSingleton(mapper);
        
        return services;
    }
}