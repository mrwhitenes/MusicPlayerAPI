using Microsoft.EntityFrameworkCore;
using MusicPlayer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.DbContexts
{
    public class MusicPlayerDbContext : DbContext
    {
        public MusicPlayerDbContext(DbContextOptions<MusicPlayerDbContext> options)
            : base(options)
        {

        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adding sample data for testing purposes.
            modelBuilder.Entity<Artist>().HasData(
                new Artist()
                {
                    Id = Guid.Parse("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"),
                    FirstName = "Michael",
                    LastName = "Jackson",
                    DateOfBirth = new DateTime(1958, 8, 29),
                    MainCategory = MainCategories.Pop
                },
                new Artist()
                {
                    Id = Guid.Parse("d82b6d90-77e7-4106-b469-434229cd3aeb"),
                    FirstName = "Marshall",
                    LastName = "Mathers",
                    DateOfBirth = new DateTime(1972, 10, 17),
                    MainCategory = MainCategories.Rap
                },
                new Artist()
                {
                    Id = Guid.Parse("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"),
                    FirstName = "Luis",
                    LastName = "Armstrong",
                    DateOfBirth = new DateTime(1901, 8, 4),
                    MainCategory = MainCategories.Jazz
                },
                new Artist()
                {
                    Id = Guid.Parse("143057ab-a2db-42f7-8089-5088a2084801"),
                    FirstName = "Elvis",
                    LastName = "Presley",
                    DateOfBirth = new DateTime(1916, 4, 10),
                    MainCategory = MainCategories.Rock
                },
                new Artist()
                {
                    Id = Guid.Parse("331513d4-79b6-4dbf-af21-d7b6488074b5"),
                    FirstName = "The",
                    LastName = "Beatles",
                    DateOfBirth = new DateTime(1940, 10, 9),
                    MainCategory = MainCategories.Rock
                }
                );

            modelBuilder.Entity<Song>().HasData(
                new Song()
                {
                    Id = Guid.Parse("e25ae3ef-8db4-4e52-8191-667019237502"),
                    Title = "Smooth Criminal",
                    Description = "Some description text",
                    ArtistId = Guid.Parse("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9")
                },
                new Song()
                {
                    Id = Guid.Parse("d7aa4e17-4013-4961-816d-b810edf2c7bf"),
                    Title = "Rap God",
                    Description = "Some description text",
                    ArtistId = Guid.Parse("d82b6d90-77e7-4106-b469-434229cd3aeb")
                },
                new Song()
                {
                    Id = Guid.Parse("43028a7f-05dc-44fb-80e9-25a542bc0fd3"),
                    Title = "What a wonderful world",
                    Description = "Some description text",
                    ArtistId = Guid.Parse("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6")
                },
                new Song()
                {
                    Id = Guid.Parse("63a8ffe7-4aa7-465b-bb2d-3168ee58ba54"),
                    Title = "Little less conversation",
                    Description = "Some description text",
                    ArtistId = Guid.Parse("143057ab-a2db-42f7-8089-5088a2084801")
                },
                new Song()
                {
                    Id = Guid.Parse("bd65e69b-7e97-4fc8-a180-2c86f50d86cd"),
                    Title = "They don't care about us",
                    Description = "Some description text",
                    ArtistId = Guid.Parse("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9")
                },
                new Song()
                {
                    Id = Guid.Parse("92724b8e-4426-46c2-a7cf-8868e0489292"),
                    Title = "Hey Jude",
                    Description = "Some description text",
                    ArtistId = Guid.Parse("331513d4-79b6-4dbf-af21-d7b6488074b5")
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
