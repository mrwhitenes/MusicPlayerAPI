using MusicPlayer.API.DbContexts;
using MusicPlayer.API.Entities;
using MusicPlayer.API.Helpers;
using MusicPlayer.API.Models;
using MusicPlayer.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.API.Services
{
    public class MusicPlayerRepository : IMusicPlayerRepository
    {
        private readonly MusicPlayerDbContext context;
        private readonly IPropertyMappingService mappingService;

        public MusicPlayerRepository(MusicPlayerDbContext context,
            IPropertyMappingService mappingService)
        {
            this.context = context;
            this.mappingService = mappingService;
        }

        public void AddArtist(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            artist.Id = Guid.NewGuid();

            foreach (var song in artist.Songs)
            {
                song.Id = Guid.NewGuid();
            }

            context.Artists.Add(artist);
        }

        public void AddSongForArtist(Guid artistId, Song song)
        {
            if (artistId == null)
            {
                throw new ArgumentNullException(nameof(artistId));
            }

            if (song == null)
            {
                throw new ArgumentNullException(nameof(song));
            }

            song.ArtistId = artistId;

            context.Songs.Add(song);
        }

        public bool ArtistExists(Guid artistId)
        {
            return context.Artists.Any(a => a.Id == artistId);
        }

        public bool Commit()
        {
            return context.SaveChanges() > 0;
        }

        public void DeleteArtist(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            context.Artists.Remove(artist);
        }

        public void DeleteSongForArtist(Song song)
        {
            if (song == null)
            {
                throw new ArgumentNullException(nameof(song));
            }

            context.Songs.Remove(song);
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

        public PagedList<Artist> GetArtists(
            ArtistResourceParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var artists = context.Artists as IQueryable<Artist>;

            // Filtering
            if (!string.IsNullOrWhiteSpace(parameters.MainCategory))
            {
                var mainCategory = parameters.MainCategory.Trim();
                artists = artists.Where(a => a.MainCategory == mainCategory);
            }
                    
            // Searching
            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                var stringQuery = parameters.SearchQuery.Trim();
                artists = artists
                    .Where(a => a.FirstName.Contains(stringQuery) ||
                                a.LastName.Contains(stringQuery));
            }

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                var artistPropertiesMappingDictionary =
                    mappingService.GetPropertyMapping<ArtistDto, Artist>();

                artists = artists.ApplySort(parameters.OrderBy,
                    artistPropertiesMappingDictionary);
            }

            return PagedList<Artist>.Create(artists,
                parameters.PageSize, parameters.PageNumber);
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
