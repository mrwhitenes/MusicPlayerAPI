using MusicPlayer.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MusicPlayer.API.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, 
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (orderBy == null)
            {
                return source;
            }

            // Generating arry of properties we want to sort by
            var orderByAfterSplit = orderBy.Split(",");

            string orderByString = string.Empty;

            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var trimmedOrderByClause = orderByClause.Trim();

                // order descending = true, order ascending = false
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                // Removing " desc" or " asc" from orderByClause to 
                // get only the property name we want to sort by
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                // Checking if sorting by propertyName is supported
                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Sorting by {propertyName} is not supported.");
                }

                var propertyMappingValue = mappingDictionary[propertyName];

                if (propertyMappingValue == null)
                {
                    throw new ArgumentNullException(nameof(propertyMappingValue));
                }

                foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }
                    
                    orderByString = orderByString +
                        (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
                        + destinationProperty
                        + (orderDescending ? " descending" : " ascending");
                }
            }

            return source.OrderBy(orderByString);
        }
    }
}
