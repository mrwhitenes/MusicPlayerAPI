using MusicPlayer.API.DbContexts;
using MusicPlayer.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.API.Services
{
    public class MusicPlayerRepository : IMusicPlayerRepository
    {
        private readonly MusicPlayerDbContext context;

        public MusicPlayerRepository(MusicPlayerDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Artist> GetArtists()
        {
            return context.Artists.ToList();
        }
    }
}
