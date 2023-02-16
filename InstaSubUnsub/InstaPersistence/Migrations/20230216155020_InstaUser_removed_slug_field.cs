using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class InstaUserremovedslugfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "InstaUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "InstaUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
