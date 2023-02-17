using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class ProtectedInstaUsersAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InstaUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "InstaUsers",
                columns: new[] { "Id", "Followers", "Following", "FollowingDate", "IsFollower", "Name", "Rank", "Status", "UnfollowingDate" },
                values: new object[,]
                {
                    { 1L, 0, 0, null, true, "hidethetrack123", 0, 3, null },
                    { 2L, 0, 0, null, true, "dr.lilith", 0, 3, null },
                    { 3L, 0, 0, null, true, "dr.imiller", 0, 3, null },
                    { 4L, 0, 0, null, true, "mynameiswhm", 0, 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers",
                column: "Name",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "IsFollower", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers");

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InstaUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
