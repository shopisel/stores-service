using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoresService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar", nullable: false),
                    name = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stores_name",
                table: "stores",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stores");
        }
    }
}
