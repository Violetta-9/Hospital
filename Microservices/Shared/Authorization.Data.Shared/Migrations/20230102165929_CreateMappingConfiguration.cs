using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Data.Shared.Migrations
{
    /// <inheritdoc />
    public partial class CreateMappingConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Receptionists_AccountId",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AccountId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "Birtday",
                table: "AspNetUsers",
                newName: "Birthday");

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_AccountId",
                table: "Receptionists",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AccountId",
                table: "Patients",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors",
                column: "AccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Receptionists_AccountId",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AccountId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "AspNetUsers",
                newName: "Birtday");

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_AccountId",
                table: "Receptionists",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AccountId",
                table: "Patients",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors",
                column: "AccountId");
        }
    }
}
