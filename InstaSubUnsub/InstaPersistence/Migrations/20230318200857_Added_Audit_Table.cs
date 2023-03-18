using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InstaPersistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Audit_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobName = table.Column<string>(type: "text", nullable: false),
                    AccountName = table.Column<string>(type: "text", nullable: false),
                    LimitPerIteration = table.Column<int>(type: "integer", nullable: false),
                    ProcessedNumber = table.Column<int>(type: "integer", nullable: false),
                    ExecutionStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExecutionEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ErrorInfo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAudit", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobAudit");
        }
    }
}
