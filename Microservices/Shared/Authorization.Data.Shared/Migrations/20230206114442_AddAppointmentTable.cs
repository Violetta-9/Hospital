using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Authorization.Data.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Documentations_DocumentationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Documentations_DocumentationId",
                table: "Offices");

            migrationBuilder.DropTable(
                name: "Documentations");

            migrationBuilder.RenameColumn(
                name: "DocumentationId",
                table: "Offices",
                newName: "PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_Offices_DocumentationId",
                table: "Offices",
                newName: "IX_Offices_PhotoId");

            migrationBuilder.RenameColumn(
                name: "DocumentationId",
                table: "AspNetUsers",
                newName: "PhotoId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DocumentationId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PhotoId");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RowCreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastRowModificationTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
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
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Photos_PhotoId",
                table: "Offices",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Photos_PhotoId",
                table: "Offices");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.RenameColumn(
                name: "PhotoId",
                table: "Offices",
                newName: "DocumentationId");

            migrationBuilder.RenameIndex(
                name: "IX_Offices_PhotoId",
                table: "Offices",
                newName: "IX_Offices_DocumentationId");

            migrationBuilder.RenameColumn(
                name: "PhotoId",
                table: "AspNetUsers",
                newName: "DocumentationId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DocumentationId");

            migrationBuilder.CreateTable(
                name: "Documentations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerName = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    LastRowModificationTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    RowCreatedTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentations", x => x.Id);
                });

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
    }
}
