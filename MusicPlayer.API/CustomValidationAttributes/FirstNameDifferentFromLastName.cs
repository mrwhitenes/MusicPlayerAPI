using MusicPlayer.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.CustomValidationAttributes
{
    public class FirstNameDifferentFromLastName : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var artist = (ArtistForCreationDto)validationContext.ObjectInstance;

            if (artist.FirstName == artist.LastName)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(ArtistForCreationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
