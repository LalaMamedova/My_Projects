using AutoMapper;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.Users;

namespace EcommerceServer.Helper;

public class AutoMapperConfig: Profile
{
    public AutoMapperConfig() 
    {
        CreateMap<CharacteristicDto, Characteristic>().ReverseMap();

        CreateMap<UserLikedProductDto, UserLikedProduct>().ReverseMap();

        CreateMap<ReviewDto, Review>().ReverseMap();

        CreateMap<PurchasedProductDto, PurchasedProduct>().ReverseMap();

        CreateMap<ProductImgDto, ProductsImg>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath)).ReverseMap();

        CreateMap<ProductOriginalImgDto, ProductOriginalImg>()
           .ForMember(dest => dest.OriginalImgPath, opt => opt.MapFrom(src => src.OriginalImgPath)).ReverseMap();

        CreateMap<SubCategoryDto, SubCategory>()
              .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
              .ForMember(dest => dest.Characteristics, opt => opt.MapFrom(src => src.Characteristics)).ReverseMap();

        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories)).ReverseMap();

        CreateMap<ProductDto, Product>()
           .ForMember(dest => dest.ProductСharacteristics, opt => opt.MapFrom(src => src.ProductСharacteristics))
           .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
           .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
           .ForMember(dest => dest.ProductsImg, opt => opt.MapFrom(src => src.ProductsImg))
           .ForMember(dest => dest.OriginalImgs, opt => opt.MapFrom(src => src.OriginalImgs))
           .ReverseMap();

        CreateMap<BrandDto, Brand>().ReverseMap();

        CreateMap<ProductСharacteristicDto, ProductСharacteristic>()
            .ForMember(dest => dest.CharacteristicId, opt => opt.MapFrom(src => src.CharacteristicId)).ReverseMap();

    }
}
