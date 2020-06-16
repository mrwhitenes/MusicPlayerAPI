using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Profiles
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<Entities.Song, Models.SongDto>();
            CreateMap<Models.SongForCreationDto, Entities.Song>();
            CreateMap<Models.SongForUpdateDto, Entities.Song>();
        }
    }
}
