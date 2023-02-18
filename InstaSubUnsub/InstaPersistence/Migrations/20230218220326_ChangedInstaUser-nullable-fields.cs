using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedInstaUsernullablefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsFollower",
                table: "InstaUsers",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<int>(
                name: "Following",
                table: "InstaUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Followers",
                table: "InstaUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsFollower",
                table: "InstaUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Following",
                table: "InstaUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Followers",
                table: "InstaUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "Followers", "Following" },
                values: new object[] { 0, 0 });
        }
    }
}
