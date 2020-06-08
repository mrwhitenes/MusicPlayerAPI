using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Entities
{
    public class Song
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public Guid ArtistId { get; set; }
    }
}
