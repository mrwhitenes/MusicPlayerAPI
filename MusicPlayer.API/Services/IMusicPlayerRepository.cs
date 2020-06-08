using MusicPlayer.API.Entities;
using MusicPlayer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    public interface IMusicPlayerRepository
    {
        IEnumerable<Artist> GetArtists();
        IEnumerable<Artist> GetArtists(
            ArtistResourceParameters parameters);
        Artist GetArtist(Guid artistId);
        IEnumerable<Song> GetSongs(Guid artistId);
        Song GetSong(Guid artistId, Guid songId);
        bool ArtistExists(Guid artistId);
    }
}
