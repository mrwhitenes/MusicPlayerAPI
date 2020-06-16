using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.API.Migrations
{
    public partial class SampleSongDescriptionsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("43028a7f-05dc-44fb-80e9-25a542bc0fd3"),
                column: "Description",
                value: "Some description text");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("63a8ffe7-4aa7-465b-bb2d-3168ee58ba54"),
                column: "Description",
                value: "Some description text");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("92724b8e-4426-46c2-a7cf-8868e0489292"),
                column: "Description",
                value: "Some description text");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("bd65e69b-7e97-4fc8-a180-2c86f50d86cd"),
                column: "Description",
                value: "Some description text");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("d7aa4e17-4013-4961-816d-b810edf2c7bf"),
                column: "Description",
                value: "Some description text");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("e25ae3ef-8db4-4e52-8191-667019237502"),
                column: "Description",
                value: "Some description text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("43028a7f-05dc-44fb-80e9-25a542bc0fd3"),
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("63a8ffe7-4aa7-465b-bb2d-3168ee58ba54"),
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("92724b8e-4426-46c2-a7cf-8868e0489292"),
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("bd65e69b-7e97-4fc8-a180-2c86f50d86cd"),
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("d7aa4e17-4013-4961-816d-b810edf2c7bf"),
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: new Guid("e25ae3ef-8db4-4e52-8191-667019237502"),
                column: "Description",
                value: null);
        }
    }
}
