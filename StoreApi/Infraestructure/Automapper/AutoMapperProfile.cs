using AutoMapper;
using StoreApi.Models;
using StoreApi.Dtos;

namespace StoreApi.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductGuid, opt => opt.MapFrom(src => src.ProductGuid))
                .ForMember(dest => dest.SkuNumber, opt => opt.MapFrom(src => src.SkuNumber))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.RecommendationId, opt => opt.MapFrom(src => src.RecommendationId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.ProductArtUrl, opt => opt.MapFrom(src => src.ProductArtUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
                .ForMember(dest => dest.Inventory, opt => opt.MapFrom(src => src.Inventory))
                .ForMember(dest => dest.LeadTime, opt => opt.MapFrom(src => src.LeadTime))
                .ReverseMap();

            CreateMap<Raincheck, RaincheckDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
