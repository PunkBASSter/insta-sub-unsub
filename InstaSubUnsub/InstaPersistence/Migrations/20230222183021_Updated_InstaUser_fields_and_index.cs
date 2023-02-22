using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInstaUserfieldsandindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers");

            migrationBuilder.AddColumn<int>(
                name: "FollowersNum",
                table: "InstaUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FollowingsNum",
                table: "InstaUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "FollowersNum", "FollowingsNum" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers",
                column: "Name",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "IsFollower", "Status", "Rank" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers");

            migrationBuilder.DropColumn(
                name: "FollowersNum",
                table: "InstaUsers");

            migrationBuilder.DropColumn(
                name: "FollowingsNum",
                table: "InstaUsers");

            migrationBuilder.CreateIndex(
                name: "IX_InstaUsers_Name",
                table: "InstaUsers",
                column: "Name",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "IsFollower", "Status" });
        }
    }
}
