using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class ProtectedInstaUsersUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InstaUsers",
                columns: new[] { "Id", "Followers", "Following", "FollowingDate", "IsFollower", "Name", "Rank", "Status", "UnfollowingDate" },
                values: new object[,]
                {
                    { 19L, 0, 0, null, true, "saulenkosvetlana", 0, 3, null },
                    { 20L, 0, 0, null, true, "ira_knows_best", 0, 3, null },
                    { 21L, 0, 0, null, true, "meltali_handmade", 0, 3, null },
                    { 22L, 0, 0, null, true, "dr.ksusha_pro_edu", 0, 3, null },
                    { 23L, 0, 0, null, true, "lydok87", 0, 3, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L);
        }
    }
}
