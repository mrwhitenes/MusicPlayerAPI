using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    public class PropertyCheckerService : IPropertyCheckerService
    {
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsSplitted = fields.Split(',');

            foreach (var field in fieldsSplitted)
            {
                var propertyName = field.Trim();

                var propertyInfo = typeof(T).GetProperty(
                    propertyName, BindingFlags.IgnoreCase |
                    BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
