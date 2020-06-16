using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.API.Entities;
using MusicPlayer.API.Models;
using MusicPlayer.API.Services;

namespace MusicPlayer.API.Controllers
{
    [Route("api/artists/{artistId}/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly IMusicPlayerRepository repository;
        private readonly IMapper mapper;

        public SongsController(IMusicPlayerRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SongDto>> GetSongsForArtist(
            Guid artistId)
        {
            if (!repository.ArtistExists(artistId))
            {
                return NotFound();
            }

            var songs = repository.GetSongs(artistId);
            return Ok(mapper.Map<IEnumerable<SongDto>>(songs));
        }

        [HttpGet("{songId}", Name = "GetSong")]
        public ActionResult<SongDto> GetSongForArtist(
            Guid artistId, Guid songId)
        {
            if (!repository.ArtistExists(artistId))
            {
                return NotFound();
            }

            var song = repository.GetSong(artistId, songId);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SongDto>(song));
        }

        [HttpPost]
        public ActionResult<SongDto> CreateSongForArtist(Guid artistId,
            SongForCreationDto song)
        {
            if (!repository.ArtistExists(artistId))
            {
                return NotFound();
            }

            var songEntity = mapper.Map<Song>(song);
            repository.AddSongForArtist(artistId, songEntity);
            repository.Commit();
            var songToReturn = mapper.Map<SongDto>(songEntity);

            return CreatedAtRoute("GetSong",
                new { artistId, songId = songEntity.Id },
                songToReturn);
        }

        [HttpPut]
        public IActionResult UpdateSongForArtist(Guid artistId,
            Guid songId, ...)
        {

        }
    }
}
