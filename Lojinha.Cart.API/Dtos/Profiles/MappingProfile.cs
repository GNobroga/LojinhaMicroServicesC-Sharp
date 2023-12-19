using AutoMapper;
using Lojinha.Cart.API.Entities;
namespace Lojinha.Cart.API.Dtos.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Entities.Cart, CartDTO>().ReverseMap();
        CreateMap<CartDetail, CartDetailDTO>().ReverseMap();
        CreateMap<Item, ItemDTO>().ReverseMap();
    }
}