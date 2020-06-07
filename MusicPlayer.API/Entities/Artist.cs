using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Entities
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }

        public MainCategories MainCategory { get; set; }

        public ICollection<Song> Songs { get; set; } 
    }
}
