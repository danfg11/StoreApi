﻿using AutoMapper;
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
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

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
