using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    /* 
     * This class stores complete mapping dictionary
     * with values for a specific type (eg. ArtistDto -> Artist)
     */

    public class PropertyMapping<TSource, TDestination> : IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> mappingDictionary { get; private set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            this.mappingDictionary = mappingDictionary ??
                throw new ArgumentNullException(nameof(mappingDictionary));
        }
    }
}
