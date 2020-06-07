using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Entities
{
    public class Song
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public Guid ArtistId { get; set; }
    }
}
