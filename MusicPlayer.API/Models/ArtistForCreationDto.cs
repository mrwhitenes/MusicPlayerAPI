using MusicPlayer.API.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Models
{
    [FirstNameDifferentFromLastName(
        ErrorMessage = "First name must be different from last name")]
    public class ArtistForCreationDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required]
        public string MainCategory { get; set; }
    }
}
