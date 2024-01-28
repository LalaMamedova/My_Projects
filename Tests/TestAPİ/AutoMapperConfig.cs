using AutoMapper;
using Microsoft.CodeAnalysis;
using System.Reflection.PortableExecutable;
using TestsLib.Dto;
using TestsLib.Models;
using TestsLib.Models.UserModels;

namespace TestAPI
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
          
            CreateMap<Test, TestDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Questions))
               .ReverseMap();

            CreateMap<TestQuestion, TestQuestionDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.TestId, opt => opt.MapFrom(src => src.TestId))
                 .ReverseMap();

            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ReverseMap();


        }
    }
}
