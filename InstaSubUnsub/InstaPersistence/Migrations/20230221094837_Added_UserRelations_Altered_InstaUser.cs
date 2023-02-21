using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserRelationsAlteredInstaUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Followers",
                table: "InstaUsers");

            migrationBuilder.DropColumn(
                name: "Following",
                table: "InstaUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InstaUsers",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "HasRussianText",
                table: "InstaUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPostDate",
                table: "InstaUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserRelations",
                columns: table => new
                {
                    FollowerId = table.Column<long>(type: "bigint", nullable: false),
                    FolloweeId = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRelations", x => new { x.FollowerId, x.FolloweeId });
                    table.ForeignKey(
                        name: "FK_UserRelations_InstaUsers_FolloweeId",
                        column: x => x.FolloweeId,
                        principalTable: "InstaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRelations_InstaUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "InstaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "HasRussianText", "LastPostDate" },
                values: new object[] { false, null });

            migrationBuilder.CreateIndex(
                name: "IX_UserRelations_FolloweeId",
                table: "UserRelations",
                column: "FolloweeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRelations");

            migrationBuilder.DropColumn(
                name: "HasRussianText",
                table: "InstaUsers");

            migrationBuilder.DropColumn(
                name: "LastPostDate",
                table: "InstaUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InstaUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "Followers",
                table: "InstaUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Following",
                table: "InstaUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { null, null });
        }
    }
}
