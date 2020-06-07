using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.API.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "FirstName", "LastName", "MainCategory" },
                values: new object[,]
                {
                    { new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"), "Michael", "Jackson", 1 },
                    { new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"), "Marshall", "Mathers", 4 },
                    { new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"), "Luis", "Armstrong", 3 },
                    { new Guid("143057ab-a2db-42f7-8089-5088a2084801"), "Elvis", "Presley", 2 },
                    { new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"), "The", "Beatles", 2 }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "ArtistId", "Title" },
                values: new object[,]
                {
                    { new Guid("e25ae3ef-8db4-4e52-8191-667019237502"), new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"), "Smooth Criminal" },
                    { new Guid("bd65e69b-7e97-4fc8-a180-2c86f50d86cd"), new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"), "They don't care about us" },
                    { new Guid("d7aa4e17-4013-4961-816d-b810edf2c7bf"), new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"), "Rap God" },
                    { new Guid("43028a7f-05dc-44fb-80e9-25a542bc0fd3"), new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"), "What a wonderful world" },
                    { new Guid("63a8ffe7-4aa7-465b-bb2d-3168ee58ba54"), new Guid("143057ab-a2db-42f7-8089-5088a2084801"), "Little less conversation" },
                    { new Guid("92724b8e-4426-46c2-a7cf-8868e0489292"), new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"), "Hey Jude" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("43028a7f-05dc-44fb-80e9-25a542bc0fd3"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("63a8ffe7-4aa7-465b-bb2d-3168ee58ba54"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("92724b8e-4426-46c2-a7cf-8868e0489292"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("bd65e69b-7e97-4fc8-a180-2c86f50d86cd"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("d7aa4e17-4013-4961-816d-b810edf2c7bf"));

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("e25ae3ef-8db4-4e52-8191-667019237502"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("143057ab-a2db-42f7-8089-5088a2084801"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"));

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"));
        }
    }
}
