using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.API.Migrations
{
    public partial class ChangedMainCategoryTypeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MainCategory",
                table: "Artists",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("143057ab-a2db-42f7-8089-5088a2084801"),
                column: "MainCategory",
                value: "Rock");

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"),
                column: "MainCategory",
                value: "Rock");

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"),
                column: "MainCategory",
                value: "Rap");

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"),
                column: "MainCategory",
                value: "Pop");

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"),
                column: "MainCategory",
                value: "Jazz");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MainCategory",
                table: "Artists",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("143057ab-a2db-42f7-8089-5088a2084801"),
                column: "MainCategory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("331513d4-79b6-4dbf-af21-d7b6488074b5"),
                column: "MainCategory",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("d82b6d90-77e7-4106-b469-434229cd3aeb"),
                column: "MainCategory",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("f6d8ab46-72c0-4c73-a2ea-e962e948f3d9"),
                column: "MainCategory",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: new Guid("fa9b5704-c2c1-46ef-84b1-bd6bbe6f46b6"),
                column: "MainCategory",
                value: 3);
        }
    }
}
