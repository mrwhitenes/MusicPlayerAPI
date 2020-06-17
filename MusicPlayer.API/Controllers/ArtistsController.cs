using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MusicPlayer.API.Entities;
using MusicPlayer.API.Models;
using MusicPlayer.API.ResourceParameters;
using MusicPlayer.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IMusicPlayerRepository repository;
        private readonly IMapper mapper;

        public ArtistsController(IMusicPlayerRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArtistDto>> GetArtists(
            [FromQuery] ArtistResourceParameters parameters)
        {
            var artists = repository.GetArtists(parameters);
            return Ok(mapper.Map<IEnumerable<ArtistDto>>(artists));
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
    }
}
