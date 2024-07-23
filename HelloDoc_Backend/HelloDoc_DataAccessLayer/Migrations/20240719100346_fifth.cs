using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloDoc_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Status",
                value: "New");

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)4,
                column: "Status",
                value: "Conclude");

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)5,
                column: "Status",
                value: "Close");

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)6,
                column: "Status",
                value: "Unpaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)1,
                column: "Status",
                value: "Initial");

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)4,
                column: "Status",
                value: "Closed");

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)5,
                column: "Status",
                value: "Deleted");

            migrationBuilder.UpdateData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)6,
                column: "Status",
                value: "Blocked");
        }
    }
}
