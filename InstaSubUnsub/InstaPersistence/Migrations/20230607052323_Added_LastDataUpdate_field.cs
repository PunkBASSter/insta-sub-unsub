using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_LastDataUpdate_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastDataUpdate",
                table: "InstaUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 4L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 5L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 6L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 7L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 8L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 9L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 10L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 11L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 12L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 13L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 14L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 15L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 16L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 17L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 18L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 19L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 20L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 21L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 22L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.UpdateData(
                table: "InstaUsers",
                keyColumn: "Id",
                keyValue: 23L,
                column: "LastDataUpdate",
                value: null);

            migrationBuilder.Sql(
                "UPDATE \"InstaUsers\" SET \"LastDataUpdate\" = '2023-06-01 00:00:00+00' WHERE \"Status\" BETWEEN 4 and 6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDataUpdate",
                table: "InstaUsers");
        }
    }
}
