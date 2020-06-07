using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.API.Migrations
{
    public partial class AddedSampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("143057ab-a2db-42f7-8089-5088a2084801"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1916, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1940, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1972, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1958, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1901, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("143057ab-a2db-42f7-8089-5088a2084801"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"),
                column: "DateOfBirth",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
