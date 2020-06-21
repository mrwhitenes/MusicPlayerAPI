using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.API.Entities;
using MusicPlayer.API.Helpers;
using MusicPlayer.API.Models;
using MusicPlayer.API.ResourceParameters;
using MusicPlayer.API.Services;
using System;
using System.Collections.Generic;
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

        public ArtistsController(IMusicPlayerRepository repository,
            IMapper mapper,
            IPropertyMappingService mappingService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.mappingService = mappingService;
        }

        [HttpGet(Name = "GetArtists")]
        public IActionResult GetArtists(
            [FromQuery] ArtistResourceParameters parameters)
        {
            if (!mappingService.ValidMappingExistsFor<ArtistDto, Artist>
                (parameters.OrderBy))
            {
                return BadRequest();
            }

            var artists = repository.GetArtists(parameters);

            var previousArtistsPageLink = artists.HasPrev ?
                CreateArtistsResourceUri(parameters,
                ResourceUriType.PreviousPageUri) : null;

            var nextArtistsPageLink = artists.HasNext ?
                CreateArtistsResourceUri(parameters,
                ResourceUriType.NextPageUri) : null;

            var paginationMetadata = new
            {
                pageSize = artists.PageSize,
                currentPage = artists.CurrentPage,
                totalPages = artists.TotalPages,
                totalCount = artists.TotalCount,
                previousArtistsPageLink,
                nextArtistsPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(mapper.Map<IEnumerable<ArtistDto>>(artists)
                .ShapeData(parameters.Fields));
        }

        [HttpGet]
        [Route("{artistId}", Name = "GetArtist")]
        public ActionResult<ArtistDto> GetArtist(Guid artistId)
        {
            var artist = repository.GetArtist(artistId);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ArtistDto>(artist));
        }

        [HttpPost]
        public ActionResult<ArtistDto> CreateArtist(
            ArtistForCreationDto artist)
        {
            var artistEntity = mapper.Map<Artist>(artist);
            repository.AddArtist(artistEntity);
            repository.Commit();
            var artistToReturn = mapper.Map<ArtistDto>(artistEntity);

            return CreatedAtRoute("GetArtist",
                new { artistId = artistEntity.Id },
                artistToReturn);
        }

        [HttpDelete("{artistId}")]
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
    }
}
