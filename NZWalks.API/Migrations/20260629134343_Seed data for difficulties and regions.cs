using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seeddatafordifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("470a72fd-56c8-447f-bf2e-ea6e5570ab0e"), "Hard" },
                    { new Guid("a0b2fe6c-f29a-4fba-983b-8d6d090260de"), "Medium" },
                    { new Guid("c825c1dd-ff1c-4cf4-bb1a-70537656dd6d"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2c1f4af9-060e-4381-aec9-5bf01615455c"), "HKB", "Hawke's Bay", "https://picsum.photos/seed/hawkesbay/800/600" },
                    { new Guid("345e16f9-4ef7-4dff-a1f5-e347004cd309"), "NTL", "Northland", "https://picsum.photos/seed/northland/800/600" },
                    { new Guid("9c79f272-09bd-44d5-89e1-b75c4e0ea4d2"), "AUK", "Auckland", "https://picsum.photos/seed/auckland/800/600" },
                    { new Guid("afb206a0-44b7-4cf4-bf5e-40f00a8c3a3a"), "BOP", "Bay of Plenty", "https://picsum.photos/seed/bayofplenty/800/600" },
                    { new Guid("c5ef0cd4-8070-4ed5-981e-7aebba0e880f"), "GIS", "Gisborne", "https://picsum.photos/seed/gisborne/800/600" },
                    { new Guid("de5197f3-6b64-4388-b818-95d5f2e291cd"), "WKO", "Waikato", "https://picsum.photos/seed/waikato/800/600" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("470a72fd-56c8-447f-bf2e-ea6e5570ab0e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a0b2fe6c-f29a-4fba-983b-8d6d090260de"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c825c1dd-ff1c-4cf4-bb1a-70537656dd6d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2c1f4af9-060e-4381-aec9-5bf01615455c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("345e16f9-4ef7-4dff-a1f5-e347004cd309"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9c79f272-09bd-44d5-89e1-b75c4e0ea4d2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("afb206a0-44b7-4cf4-bf5e-40f00a8c3a3a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c5ef0cd4-8070-4ed5-981e-7aebba0e880f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("de5197f3-6b64-4388-b818-95d5f2e291cd"));
        }
    }
}
