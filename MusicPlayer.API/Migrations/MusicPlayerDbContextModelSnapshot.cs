﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicPlayer.API.DbContexts;

namespace MusicPlayer.API.Migrations
{
    [DbContext(typeof(MusicPlayerDbContext))]
    partial class MusicPlayerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicPlayer.API.Entities.Artist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("MainCategory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"),
                            DateOfBirth = new DateTimeOffset(new DateTime(1958, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            FirstName = "Michael",
                            LastName = "Jackson",
                            MainCategory = 1
                        },
                        new
                        {
                            Id = new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"),
                            DateOfBirth = new DateTimeOffset(new DateTime(1972, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            FirstName = "Marshall",
                            LastName = "Mathers",
                            MainCategory = 4
                        },
                        new
                        {
                            Id = new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"),
                            DateOfBirth = new DateTimeOffset(new DateTime(1901, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            FirstName = "Luis",
                            LastName = "Armstrong",
                            MainCategory = 3
                        },
                        new
                        {
                            Id = new Guid("143057ab-a2db-42f7-8089-5088a2084801"),
                            DateOfBirth = new DateTimeOffset(new DateTime(1916, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            FirstName = "Elvis",
                            LastName = "Presley",
                            MainCategory = 2
                        },
                        new
                        {
                            Id = new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"),
                            DateOfBirth = new DateTimeOffset(new DateTime(1940, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            FirstName = "The",
                            LastName = "Beatles",
                            MainCategory = 2
                        });
                });

            modelBuilder.Entity("MusicPlayer.API.Entities.Song", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Songs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e25ae3ef-8db4-4e52-8191-667019237502"),
                            ArtistId = new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"),
                            Description = "Some description text",
                            Title = "Smooth Criminal"
                        },
                        new
                        {
                            Id = new Guid("d7aa4e17-4013-4961-816d-b810edf2c7bf"),
                            ArtistId = new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"),
                            Description = "Some description text",
                            Title = "Rap God"
                        },
                        new
                        {
                            Id = new Guid("43028a7f-05dc-44fb-80e9-25a542bc0fd3"),
                            ArtistId = new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"),
                            Description = "Some description text",
                            Title = "What a wonderful world"
                        },
                        new
                        {
                            Id = new Guid("63a8ffe7-4aa7-465b-bb2d-3168ee58ba54"),
                            ArtistId = new Guid("143057ab-a2db-42f7-8089-5088a2084801"),
                            Description = "Some description text",
                            Title = "Little less conversation"
                        },
                        new
                        {
                            Id = new Guid("bd65e69b-7e97-4fc8-a180-2c86f50d86cd"),
                            ArtistId = new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"),
                            Description = "Some description text",
                            Title = "They don't care about us"
                        },
                        new
                        {
                            Id = new Guid("92724b8e-4426-46c2-a7cf-8868e0489292"),
                            ArtistId = new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"),
                            Description = "Some description text",
                            Title = "Hey Jude"
                        });
                });

            modelBuilder.Entity("MusicPlayer.API.Entities.Song", b =>
                {
                    b.HasOne("MusicPlayer.API.Entities.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
