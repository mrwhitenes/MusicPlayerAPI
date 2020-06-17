using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            songEntity.Id = Guid.NewGuid();
            repository.AddSongForArtist(artistId, songEntity);
            repository.Commit();
            var songToReturn = mapper.Map<SongDto>(songEntity);

            return CreatedAtRoute("GetSong",
                new { artistId, songId = songEntity.Id },
                songToReturn);
        }

        [HttpPut("{songId}")]
        public IActionResult UpdateSongForArtist(Guid artistId,
            Guid songId, SongForUpdateDto song)
        {
            if (!repository.ArtistExists(artistId))
            {
                return NotFound();
            }

            var songEntity = repository.GetSong(artistId, songId);

            if (songEntity == null)
            {
                // Upserting song with PUT
                var songToCreate = mapper.Map<Song>(song);
                songToCreate.Id = songId;

                repository.AddSongForArtist(artistId, songToCreate);
                repository.Commit();

                var songToReturn = mapper.Map<SongDto>(songToCreate);

                return CreatedAtRoute("GetSong",
                    new { artistId, songId = songToCreate.Id },
                    songToReturn);
            }

            mapper.Map(song, songEntity);

            repository.Commit();

            return NoContent();
        }

        [HttpPatch("{songId}")]
        public IActionResult PartiallyUpdateSongForArtist(Guid artistId,
            Guid songId,
            [FromBody] JsonPatchDocument<SongForUpdateDto> patchDocument)
        {
            if (!repository.ArtistExists(artistId))
            {
                return NotFound();
            }

            var songEntity = repository.GetSong(artistId, songId);

            if (songEntity == null)
            {
                var songToCreate = new SongForUpdateDto();
                patchDocument.ApplyTo(songToCreate, ModelState);

                if (!TryValidateModel(songToCreate))
                {
                    return ValidationProblem(ModelState);
                }

                var songToCreateEntity = mapper.Map<Song>(songToCreate);
                songToCreateEntity.Id = songId;

                repository.AddSongForArtist(artistId, songToCreateEntity);
                repository.Commit();

                var songToReturn = mapper.Map<SongDto>(songToCreateEntity);

                return CreatedAtRoute("GetSong",
                    new { artistId, songId = songToCreateEntity.Id },
                    songToReturn);
            }

            var songToPatch = mapper.Map<SongForUpdateDto>(songEntity);
            patchDocument.ApplyTo(songToPatch, ModelState);

            if (!TryValidateModel(songToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(songToPatch, songEntity);
            repository.Commit();

            return NoContent();
        }

        [HttpDelete("{songId}")]
        public IActionResult DeleteSongForArtist(Guid artistId, Guid songId)
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

            repository.DeleteSongForArtist(song);
            repository.Commit();

            return NoContent();
        }

        // Overriding default ValidationProblem to return detailed 
        // validation error defined in Startup class
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}
