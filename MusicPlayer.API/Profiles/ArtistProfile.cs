using AutoMapper;
using MusicPlayer.API.Entities;
using MusicPlayer.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Profiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Entities.Artist, Models.ArtistDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                )
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
                )
                .ForMember(
                    dest => dest.MainCategory,
                    opt => opt.MapFrom(src => src.MainCategory.ToString())
                );
            CreateMap<Models.ArtistForCreationDto, Entities.Artist>()
                .ForMember(
                    dest => dest.MainCategory,
                    opt => opt.MapFrom(src => Enum.Parse<MainCategories>(src.MainCategory))
                );
        }
    }
}
