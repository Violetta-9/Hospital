using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Authorization.Data.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DocumentationId",
                table: "Offices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DocumentationId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Documentations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    ContainerName = table.Column<string>(type: "text", nullable: true),
                    RowCreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastRowModificationTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offices_DocumentationId",
                table: "Offices",
                column: "DocumentationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DocumentationId",
                table: "AspNetUsers",
                column: "DocumentationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Documentations_DocumentationId",
                table: "AspNetUsers",
                column: "DocumentationId",
                principalTable: "Documentations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Documentations_DocumentationId",
                table: "Offices",
                column: "DocumentationId",
                principalTable: "Documentations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Documentations_DocumentationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Documentations_DocumentationId",
                table: "Offices");

            migrationBuilder.DropTable(
                name: "Documentations");

            migrationBuilder.DropIndex(
                name: "IX_Offices_DocumentationId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DocumentationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DocumentationId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "DocumentationId",
                table: "AspNetUsers");
        }
    }
}
