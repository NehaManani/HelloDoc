using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloDoc_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class nineth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserStatus",
                columns: new[] { "Id", "Status" },
                values: new object[] { (byte)7, "Block" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserStatus",
                keyColumn: "Id",
                keyValue: (byte)7);
        }
    }
}
