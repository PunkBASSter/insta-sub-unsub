using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class ProtectedInstaUsersUpdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "doctor_lilith");

            migrationBuilder.InsertData(
                table: "InstaUsers",
                columns: new[] { "Id", "Followers", "Following", "FollowingDate", "IsFollower", "Name", "Rank", "Status", "UnfollowingDate" },
                values: new object[,]
                {
                    { 5L, 0, 0, null, true, "err_yep", 0, 3, null },
                    { 6L, 0, 0, null, true, "temapunk", 0, 3, null },
                    { 7L, 0, 0, null, true, "sergesoukonnov", 0, 3, null },
                    { 8L, 0, 0, null, true, "panther_amanita", 0, 3, null },
                    { 9L, 0, 0, null, true, "igor.gord", 0, 3, null },
                    { 10L, 0, 0, null, true, "olga.mikholenko", 0, 3, null },
                    { 11L, 0, 0, null, true, "iriska_sia", 0, 3, null },
                    { 12L, 0, 0, null, true, "oli4kakisskiss", 0, 3, null },
                    { 13L, 0, 0, null, true, "err_please", 0, 3, null },
                    { 14L, 0, 0, null, true, "blefamer", 0, 3, null },
                    { 15L, 0, 0, null, true, "lodkaissad", 0, 3, null },
                    { 16L, 0, 0, null, true, "anastasiya_kun", 0, 3, null },
                    { 17L, 0, 0, null, true, "anna.saulenko", 0, 3, null },
                    { 18L, 0, 0, null, true, "prikhodko5139", 0, 3, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "dr.lilith");
        }
    }
}
