using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Data.Shared.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Appointments",
                newName: "DateTime");

            migrationBuilder.AddColumn<long>(
                name: "OfficeId",
                table: "Appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SpecializationId",
                table: "Appointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OfficeId",
                table: "Appointments",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SpecializationId",
                table: "Appointments",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Offices_OfficeId",
                table: "Appointments",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Specializations_SpecializationId",
                table: "Appointments",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Offices_OfficeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Specializations_SpecializationId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_OfficeId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_SpecializationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Appointments",
                newName: "Date");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Appointments",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
