using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iot.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeviceName",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Lat",
                table: "Devices",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Long",
                table: "Devices",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "DeviceLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "DeviceLogs");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceName",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
