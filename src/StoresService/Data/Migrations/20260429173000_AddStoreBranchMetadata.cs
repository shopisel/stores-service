using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoresService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreBranchMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "stores",
                type: "varchar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "brand",
                table: "stores",
                type: "varchar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "stores",
                type: "varchar",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_stores_brand",
                table: "stores",
                column: "brand");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_stores_brand",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "address",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "brand",
                table: "stores");

            migrationBuilder.DropColumn(
                name: "city",
                table: "stores");
        }
    }
}
