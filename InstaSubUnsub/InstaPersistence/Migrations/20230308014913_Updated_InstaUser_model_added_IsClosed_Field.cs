using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class Updated_InstaUser_model_added_IsClosed_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "InstaUsers",
                type: "boolean",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L,
                column: "IsClosed",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L,
                column: "IsClosed",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers",
                column: "Name",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "IsFollower", "Status", "Rank", "LastPostDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "InstaUsers");

            migrationBuilder.CreateIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers",
                column: "Name",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "IsFollower", "Status", "Rank" });
        }
    }
}
