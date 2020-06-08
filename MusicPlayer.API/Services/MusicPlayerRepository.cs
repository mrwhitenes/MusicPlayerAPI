using MusicPlayer.API.DbContexts;
using MusicPlayer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.API.Services
{
    public class MusicPlayerRepository : IMusicPlayerRepository
    {
        private readonly MusicPlayerDbContext context;

        public MusicPlayerRepository(MusicPlayerDbContext context)
        {
            this.context = context;
        }

        public bool ArtistExists(Guid artistId)
        {
            return context.Artists.Any(a => a.Id == artistId);
        }

        public Artist GetArtist(Guid artistId)
        {
            if (artistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(artistId));
            }

            return context.Artists.FirstOrDefault(a => a.Id == artistId);
        }

        public IEnumerable<Artist> GetArtists()
        {
            return context.Artists.ToList();
        }

        public Song GetSong(Guid artistId, Guid songId)
        {
            if (artistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(artistId));
            }

            if (songId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(songId));
            }

            return context.Songs
                .Where(s => s.ArtistId == artistId && s.Id == songId)
                .FirstOrDefault();
        }

        public IEnumerable<Song> GetSongs(Guid artistId)
        {
            if (artistId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(artistId));
            }

            return context.Songs.Where(s => s.ArtistId == artistId).ToList();
        }
    }
}
