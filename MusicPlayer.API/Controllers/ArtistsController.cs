using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MusicPlayer.API.Entities;
using MusicPlayer.API.Helpers;
using MusicPlayer.API.Models;
using MusicPlayer.API.ResourceParameters;
using MusicPlayer.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IMusicPlayerRepository repository;
        private readonly IMapper mapper;
        private readonly IPropertyMappingService mappingService;
        private readonly IPropertyCheckerService propertyChecker;

        public ArtistsController(IMusicPlayerRepository repository,
            IMapper mapper,
            IPropertyMappingService mappingService,
            IPropertyCheckerService propertyChecker)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.mappingService = mappingService;
            this.propertyChecker = propertyChecker;
        }

        [Produces("application/json",
            "application/vnd.mrwhiteness.hateoas+json",
            "application/vnd.mrwhiteness.friendly+json",
            "application/vnd.mrwhiteness.friendly.hateoas+json",
            "application/vnd.mrwhiteness.full+json",
            "application/vnd.mrwhiteness.full.hateoas+json")]
        [HttpGet(Name = "GetArtists")]
        public IActionResult GetArtists(
            [FromQuery] ArtistResourceParameters parameters,
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!mappingService.ValidMappingExistsFor<ArtistDto, Artist>
                (parameters.OrderBy))
            {
                return BadRequest();
            }

            if (!propertyChecker.TypeHasProperties<ArtistDto>(parameters.Fields))
            {
                return BadRequest();
            }

            if (!MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var artists = repository.GetArtists(parameters);

            var paginationMetadata = new
            {
                pageSize = artists.PageSize,
                currentPage = artists.CurrentPage,
                totalPages = artists.TotalPages,
                totalCount = artists.TotalCount
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith(
                "hateoas", StringComparison.InvariantCultureIgnoreCase);

            IEnumerable<LinkDto> links = new List<LinkDto>();

            if (includeLinks)
            {
                links = CreateLinksForArtists(parameters,
                    artists.HasNext, artists.HasPrev); 
            }

            var primaryMediaType = includeLinks ?
                parsedMediaType.SubTypeWithoutSuffix
                .Substring(0, parsedMediaType.SubTypeWithoutSuffix.Length - 8) :
                parsedMediaType.SubTypeWithoutSuffix;

            if (primaryMediaType == "vnd.mrwhiteness.full")
            {
                var fullResources = mapper.Map
                    <IEnumerable<ArtistFullDto>>(artists)
                    .ShapeData(parameters.Fields);

                if (includeLinks)
                {
                    var fullResourcesWithLinks = fullResources.Select(artist =>
                    {
                        var artistAsDictionary = artist
                            as IDictionary<string, object>;
                        var artistLinks = CreateLinksForArtist(
                            (Guid)artistAsDictionary["Id"], null);
                        artistAsDictionary.Add("links", artistLinks);
                        return artistAsDictionary;
                    });

                    var fullLinkedCollectionResource = new
                    {
                        values = fullResourcesWithLinks,
                        links
                    };

                    return Ok(fullLinkedCollectionResource);
                }

                return Ok(fullResources);
            }

            var friendlyResources = mapper.Map
                <IEnumerable<ArtistDto>>(artists)
                .ShapeData(parameters.Fields);

            if (includeLinks)
            {
                var friendlyResourcesWithLinks = friendlyResources.Select(artist =>
                {
                    var artistAsDictionary = artist
                           as IDictionary<string, object>;
                    var artistLinks = CreateLinksForArtist(
                        (Guid)artistAsDictionary["Id"], null);
                    artistAsDictionary.Add("links", artistLinks);
                    return artistAsDictionary;
                });

                var friendlyLinkedCollectionResource = new
                {
                    values = friendlyResourcesWithLinks,
                    links
                };

                return Ok(friendlyLinkedCollectionResource);
            }

            return Ok(friendlyResources);
        }


        [Produces("application/json",
            "application/vnd.mrwhiteness.hateoas+json",
            "application/vnd.mrwhiteness.friendly+json",
            "application/vnd.mrwhiteness.friendly.hateoas+json",
            "application/vnd.mrwhiteness.full+json",
            "application/vnd.mrwhiteness.full.hateoas+json")]
        [HttpGet]
        [Route("{artistId}", Name = "GetArtist")]
        public ActionResult<ArtistDto> GetArtist(Guid artistId, string fields,
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!propertyChecker.TypeHasProperties<ArtistDto>(fields))
            {
                return BadRequest();
            }

            if (!MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var artist = repository.GetArtist(artistId);

            if (artist == null)
            {
                return NotFound();
            }

            // Choosing right type to return 

            // Checking if type includes hateoas
            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith(
                "hateoas", StringComparison.InvariantCultureIgnoreCase);

            IEnumerable<LinkDto> links = new List<LinkDto>();
            links = CreateLinksForArtist(artistId, fields);

            var primaryMediaType = includeLinks ?
                parsedMediaType.SubTypeWithoutSuffix
                .Substring(0, parsedMediaType.SubTypeWithoutSuffix.Length - 8) :
                parsedMediaType.SubTypeWithoutSuffix;

            // Returning ArtistFullDto
            if (primaryMediaType == "vnd.mrwhiteness.full")
            {
                var fullResource = mapper.Map<ArtistFullDto>(artist)
                    .ShapeData(fields) as IDictionary<string, object>;

                if (includeLinks)
                {
                    fullResource.Add("links", links);
                }

                return Ok(fullResource);
            }

            // Returning friendly ArtistDto
            var friendlyResource = mapper.Map<ArtistDto>(artist)
                    .ShapeData(fields) as IDictionary<string, object>;

            if (includeLinks)
            {
                friendlyResource.Add("links", links);
            }

            return Ok(friendlyResource);
        }

        [HttpPost(Name = "CreateArtist")]
        public ActionResult<ArtistDto> CreateArtist(
            ArtistForCreationDto artist)
        {
            var artistEntity = mapper.Map<Artist>(artist);
            repository.AddArtist(artistEntity);
            repository.Commit();
            var artistToReturn = mapper.Map<ArtistDto>(artistEntity);

            var links = CreateLinksForArtist(artistToReturn.Id, null);

            var linkedResource = artistToReturn.ShapeData(null)
                as IDictionary<string, object>;

            linkedResource.Add("links", links);

            return CreatedAtRoute("GetArtist",
                new { artistId = linkedResource["Id"] },
                linkedResource);
        }

        [HttpDelete("{artistId}", Name = "DeleteArtist")]
        public IActionResult DeleteArtist(Guid artistId)
        {
            var artist = repository.GetArtist(artistId);

            if (artist == null)
            {
                return NotFound();
            }

            repository.DeleteArtist(artist);
            repository.Commit();

            return NoContent();
        }

        private string CreateArtistsResourceUri(
            ArtistResourceParameters parameters,
            ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPageUri:
                    return Url.Link("GetArtists",
                        new
                        {
                            fields = parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageSize = parameters.PageSize,
                            pageNumber = parameters.PageNumber - 1,
                            mainCategory = parameters.MainCategory,
                            searchQuery = parameters.SearchQuery
                        });
                case ResourceUriType.NextPageUri:
                    return Url.Link("GetArtists",
                        new
                        {
                            fields = parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageSize = parameters.PageSize,
                            pageNumber = parameters.PageNumber + 1,
                            mainCategory = parameters.MainCategory,
                            searchQuery = parameters.SearchQuery
                        });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetArtists",
                        new
                        {
                            fields = parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageSize = parameters.PageSize,
                            pageNumber = parameters.PageNumber,
                            mainCategory = parameters.MainCategory,
                            searchQuery = parameters.SearchQuery
                        });
            }
        }

        public IEnumerable<LinkDto> CreateLinksForArtist(
            Guid artistId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkDto(Url.Link(
                    "GetArtist", new { artistId }),
                    "self",
                    "GET"));
            }
            else
            {
                links.Add(new LinkDto(Url.Link(
                    "GetArtist", new { artistId, fields }),
                    "self",
                    "GET"));
            }

            links.Add(new LinkDto(Url.Link(
                "GetSongsForArtist", new { artistId }),
                "songs",
                "GET"));

            links.Add(new LinkDto(Url.Link(
                "DeleteArtist", new { artistId }),
                "delete_artist",
                "DELETE"));

            links.Add(new LinkDto(Url.Link(
                "CreateSongForArtist", new { artistId }),
                "create_song_for_artist",
                "POST"));

            return links;
        }

        public IEnumerable<LinkDto> CreateLinksForArtists(
            ArtistResourceParameters parameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(CreateArtistsResourceUri(
                parameters, ResourceUriType.Current),
                "self", "GET"));

            if (hasNext)
            {
                links.Add(new LinkDto(CreateArtistsResourceUri(
                    parameters, ResourceUriType.NextPageUri),
                    "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new LinkDto(CreateArtistsResourceUri(
                    parameters, ResourceUriType.PreviousPageUri),
                    "previousPage", "GET"));
            }

            return links;
        }
    }
}
