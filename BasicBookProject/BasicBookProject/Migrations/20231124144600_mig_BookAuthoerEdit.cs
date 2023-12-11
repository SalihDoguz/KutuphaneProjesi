using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicBookProject.Migrations
{
    /// <inheritdoc />
    public partial class mig_BookAuthoerEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Yazar",
                table: "Books",
                newName: "Author");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "Yazar");
        }
    }
}
