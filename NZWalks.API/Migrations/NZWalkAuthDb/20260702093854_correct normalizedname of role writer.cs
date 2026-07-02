using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations.NZWalkAuthDb
{
    /// <inheritdoc />
    public partial class correctnormalizednameofrolewriter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58559420-55f6-45c3-bc31-cef16e35b8a3",
                column: "NormalizedName",
                value: "WRITER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58559420-55f6-45c3-bc31-cef16e35b8a3",
                column: "NormalizedName",
                value: "WRITER");
        }
    }
}
