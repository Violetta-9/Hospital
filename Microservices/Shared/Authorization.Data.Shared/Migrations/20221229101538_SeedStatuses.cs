using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authorization.Data.Shared.Migrations
{
    /// <inheritdoc />
    public partial class SeedStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO ""Statuses"" (""Title"",""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('At work',NOW(),NOW())");
            migrationBuilder.Sql(@"INSERT INTO ""Statuses"" (""Title"",""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('On vacation',NOW(),NOW())");
            migrationBuilder.Sql(@"INSERT INTO ""Statuses"" (""Title"",""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('Sick Day',NOW(),NOW())");
            migrationBuilder.Sql(@"INSERT INTO ""Statuses"" (""Title"",""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('Sick Leave',NOW(),NOW())");
            migrationBuilder.Sql(@"INSERT INTO ""Statuses"" (""Title"",""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('Self-isolation',NOW(),NOW())");
            migrationBuilder.Sql(@"INSERT INTO ""Statuses"" (""Title"",""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('Leave without pay',NOW(),NOW())");
            migrationBuilder.Sql(@"INSERT INTO ""Statuses"" (""Title"",""RowCreatedTimestamp"",""LastRowModificationTimestamp"") VALUES('Inactive',NOW(),NOW())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
