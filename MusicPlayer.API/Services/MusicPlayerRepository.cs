using MusicPlayer.API.DbContexts;
using MusicPlayer.API.Entities;
using MusicPlayer.API.ResourceParameters;
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

            return context.Artists
                .FirstOrDefault(a => a.Id == artistId);
        }

        public IEnumerable<Artist> GetArtists()
        {
            return context.Artists.ToList();
        }

        public IEnumerable<Artist> GetArtists(
            ArtistResourceParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrWhiteSpace(parameters.MainCategory) &&
                string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                return GetArtists();
            }

            var artists = context.Artists as IQueryable<Artist>;

            if (!string.IsNullOrWhiteSpace(parameters.MainCategory))
            {
                var mainCategory = parameters.MainCategory.Trim();
                artists = artists.Where(a => a.MainCategory == Enum.Parse<MainCategories>(mainCategory));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                var stringQuery = parameters.SearchQuery.Trim();
                artists = artists
                    .Where(a => a.FirstName.Contains(stringQuery) ||
                                a.LastName.Contains(stringQuery));
            }

            return artists.ToList();
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

            return context.Songs
                .Where(s => s.ArtistId == artistId).ToList();
        }
    }
}
