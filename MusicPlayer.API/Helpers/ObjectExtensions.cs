using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MusicPlayer.API.Helpers
{
    public static class ObjectExtensions
    {
        // Extension method used for shaping returned data.

        // Extension method for Object<TSource> that returns new
        // ExpandoObject containing only the properties of TSource
        // object defined in fields parameter.

        public static ExpandoObject ShapeData<TSource>(
            this TSource source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var dataShapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertiesInfos = typeof(TSource)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var propertyInfo in propertiesInfos)
                {
                    ((IDictionary<string, object>)dataShapedObject)
                        .Add(
                            propertyInfo.Name,
                            propertyInfo.GetValue(source)
                        );
                }

                return dataShapedObject;
            }

            var fieldsSplitted = fields.Split(',');

            foreach (var field in fieldsSplitted)
            {
                var propertyName = field.Trim();

                var propertyInfo = typeof(TSource)
                    .GetProperty(propertyName, BindingFlags.IgnoreCase |
                    BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new ArgumentNullException($"Property {propertyName}" +
                        $" was not found on {typeof(TSource)}");
                }

                ((IDictionary<string, object>)dataShapedObject)
                        .Add(
                            propertyInfo.Name,
                            propertyInfo.GetValue(source)
                        );
            }

            return dataShapedObject;
        }
    }
}
