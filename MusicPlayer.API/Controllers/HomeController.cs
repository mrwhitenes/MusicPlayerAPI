using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.API.Models;

namespace MusicPlayer.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        [HttpGet(Name = "GetIndex")]
        public IActionResult Index()
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(Url.Link("GetIndex", new { }), 
                "self", "GET"));

            links.Add(new LinkDto(Url.Link("GetArtists", new { }),
                "artists", "GET"));

            links.Add(new LinkDto(Url.Link("CreateArtist", new { }),
                "createArtist", "POST"));

            return Ok(links);
        }
    }
}
