using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.API.Models;
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
        public ActionResult<IEnumerable<ArtistDto>> GetArtists()
        {
            var artists = repository.GetArtists();
            return Ok(mapper.Map<IEnumerable<ArtistDto>>(artists));
        }

        [HttpGet]
        [Route("{artistId}")]
        public ActionResult<ArtistDto> GetArtist(Guid artistId)
        {
            var artist = repository.GetArtist(artistId);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ArtistDto>(artist));
        }
    }
}
