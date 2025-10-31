using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TecWebFest.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stages_FestivalId_Name",
                table: "Stages");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_FestivalId",
                table: "Stages",
                column: "FestivalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stages_FestivalId",
                table: "Stages");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_FestivalId_Name",
                table: "Stages",
                columns: new[] { "FestivalId", "Name" },
                unique: true);
        }
    }
}
