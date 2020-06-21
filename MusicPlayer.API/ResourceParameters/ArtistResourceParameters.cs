using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.ResourceParameters
{
    public class ArtistResourceParameters
    {
        private const int maxPageSize = 10;
        public string MainCategory { get; set; }
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 2;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

        public string OrderBy { get; set; } = "Name";
        public string Fields { get; set; }
    }
}
