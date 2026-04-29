using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoresService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "latitude",
                table: "stores",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "longitude",
                table: "stores",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "stores");
        }
    }
}
