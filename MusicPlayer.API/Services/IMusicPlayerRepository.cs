using MusicPlayer.API.Entities;
using MusicPlayer.API.Helpers;
using MusicPlayer.API.Models;
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
        PagedList<Artist> GetArtists(
            ArtistResourceParameters parameters);
        Artist GetArtist(Guid artistId);
        void AddArtist(Artist artist);
        void DeleteArtist(Artist artist);
        IEnumerable<Song> GetSongs(Guid artistId);
        Song GetSong(Guid artistId, Guid songId);
        void AddSongForArtist(Guid artistId, Song song);
        void DeleteSongForArtist(Song song);
        bool ArtistExists(Guid artistId);
        bool Commit();
    }
}
