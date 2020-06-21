using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MusicPlayer.API.Helpers
{
    public static class IEnumerableExtensions
    {
        // Extension method used for shaping returned data.

        // Extension method for IEnumerable<T> that returns new object
        // of type IEnumerable<ExpandoObject> containing only the
        // properties of TSource object included in fields parameter.

        public static IEnumerable<ExpandoObject> ShapeData<TSource>(
            this IEnumerable<TSource> source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // This list will hold ExpandoObjects
            var expandoObjectList = new List<ExpandoObject>();

            // This list will hold properties of returned objects
            var propertyInfoList = new List<PropertyInfo>();

            // If no fields were specified
            if (string.IsNullOrWhiteSpace(fields))
            {
                // Returned objects should have all public properties of TSource
                var propertyInfos = typeof(TSource)
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            // Else returned objects should have only properties 
            // defined in fields parameter
            else
            {
                var fieldsList = fields.Split(",");

                foreach (var field in fieldsList)
                {
                    var propertyName = field.Trim();

                    // Getting PropertyInfo from propertyName
                    var propertyInfo = typeof(TSource)
                        .GetProperty(propertyName, BindingFlags.IgnoreCase |
                        BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property {propertyName} was not " +
                            $"found on {typeof(TSource)}");
                    }

                    propertyInfoList.Add(propertyInfo);
                }
            }

            // We have properties we want to return in propertyInfoList.
            // Now we need to get their values from TSource object and
            // create new ExpandoObjects with those values.

            foreach (TSource sourceObject in source)
            {
                var dataShapedObject = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    ((IDictionary<string, object>)dataShapedObject)
                        .Add(
                            propertyInfo.Name, 
                            propertyInfo.GetValue(sourceObject)
                        ); 
                }

                expandoObjectList.Add(dataShapedObject);
            }

            return expandoObjectList;
        }
    }
}
