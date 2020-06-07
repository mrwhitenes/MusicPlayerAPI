using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Helpers
{
    public static class DateTimeOffSetExtensions
    {
        public static int CalculateAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTimeOffset.UtcNow;
            var age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
            {
                age -= 1;
            }

            return age;
        }
    }
}
