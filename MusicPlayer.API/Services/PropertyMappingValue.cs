using System;
using System.Collections.Generic;

namespace MusicPlayer.API.Services
{
    /*
     * This class stores list of destination (entity)
     * properties we are going to map to.
     */

    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; set; }
        public bool Revert { get; private set; }
        public PropertyMappingValue(IEnumerable<string> destinationProperties,
            bool revert = false)
        {
            DestinationProperties = destinationProperties ??
                throw new ArgumentNullException(nameof(destinationProperties));
            Revert = revert;
        }
    }
}
