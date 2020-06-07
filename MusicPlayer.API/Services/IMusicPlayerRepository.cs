using MusicPlayer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    public interface IMusicPlayerRepository
    {
        IEnumerable<Artist> GetArtists();
    }
}
