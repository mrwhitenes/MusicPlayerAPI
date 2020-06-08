using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Entities
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required]
        public MainCategories MainCategory { get; set; }

        public ICollection<Song> Songs { get; set; }
            = new List<Song>();
    }
}
