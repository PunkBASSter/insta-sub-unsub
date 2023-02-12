using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AlteredInstaUserfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FollowingDate",
                table: "InstaUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UnfollowingDate",
                table: "InstaUsers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowingDate",
                table: "InstaUsers");

            migrationBuilder.DropColumn(
                name: "UnfollowingDate",
                table: "InstaUsers");
        }
    }
}
