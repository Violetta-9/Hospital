using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Data.Shared.Migrations
{
    /// <inheritdoc />
    public partial class SeedServiceCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO ""ServiceCategories"" (""Title"",""TimeSlotSize"", ""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('analyses',1,NOW(),NOW())");
            migrationBuilder.Sql(@"INSERT INTO ""ServiceCategories"" (""Title"",""TimeSlotSize"", ""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('consultation',1,NOW(),NOW())");
            migrationBuilder.Sql(@"INSERT INTO ""ServiceCategories"" (""Title"",""TimeSlotSize"", ""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('diagnostics',1.5,NOW(),NOW())");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
