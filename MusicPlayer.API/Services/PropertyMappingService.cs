using MusicPlayer.API.Entities;
using MusicPlayer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    /* 
     * Service for holding custom mappings between 
     * outer facing ring and database properties
     */

    public class PropertyMappingService : IPropertyMappingService
    {
        // Definition for mapping artists properties
        private Dictionary<string, PropertyMappingValue> artistPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>{ "Id" }) },
                { "MainCategory", new PropertyMappingValue(new List<string>{ "MainCategory" }) },
                { "Name", new PropertyMappingValue(new List<string>{ "FirstName", "LastName" }) },
                { "Age", new PropertyMappingValue(new List<string>{ "DateOfBirth" }, true) }
            };

        // This list holds custom mappings 
        private IList<IPropertyMapping> propertyMappings =
            new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            propertyMappings.Add(new PropertyMapping<ArtistDto, Artist>(artistPropertyMapping));
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var splitedFields = fields.Split(",");

            foreach (var field in splitedFields)
            {
                var trimmedField = field.Trim();

                // Extracting only a property name from field
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>()
        {
            var searchedMapping = propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (searchedMapping.Count() == 1)
            {
                return searchedMapping.First().mappingDictionary;
            }

            throw new Exception($"Unable to find maching property mapping " +
                $"for <{typeof(TSource)}, {typeof(TDestination)}>");
        }
    }
}
