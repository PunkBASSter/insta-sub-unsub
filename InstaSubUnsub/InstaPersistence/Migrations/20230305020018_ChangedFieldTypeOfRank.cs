using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFieldTypeOfRank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rank",
                table: "InstaUsers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Rank",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Rank",
                value: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rank",
                table: "InstaUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Rank",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Rank",
                value: null);
        }
    }
}
