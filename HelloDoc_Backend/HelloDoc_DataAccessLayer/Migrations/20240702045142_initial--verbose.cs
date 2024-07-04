using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelloDoc_DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class initialverbose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BloodGroup = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false),
                    Role = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatus",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Role = table.Column<byte>(type: "smallint", maxLength: 16, nullable: false),
                    OTP = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    OtpExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Zip = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Avatar = table.Column<byte[]>(type: "bytea", nullable: true),
                    Status = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Genders_Gender",
                        column: x => x.Gender,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_Role",
                        column: x => x.Role,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_UserStatus_Status",
                        column: x => x.Status,
                        principalTable: "UserStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    EmergencyContactName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    EmergencyContactNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    MedicalHistory = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Allergies = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CurrentMedications = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    BloodTypeId = table.Column<int>(type: "integer", nullable: true),
                    Document = table.Column<byte[]>(type: "bytea", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientDetails_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProviderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    MedicalLicense = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NpiNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BusinessName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BusinessWebsite = table.Column<string>(type: "text", nullable: true),
                    Document = table.Column<byte[]>(type: "bytea", nullable: true),
                    AdminNotes = table.Column<string>(type: "text", nullable: true),
                    ContractorAgreement = table.Column<bool>(type: "boolean", nullable: false),
                    ContractorDocument = table.Column<byte[]>(type: "bytea", nullable: true),
                    BackgroundCheck = table.Column<bool>(type: "boolean", nullable: false),
                    BackgroundCheckDocument = table.Column<byte[]>(type: "bytea", nullable: true),
                    HipaaCompliance = table.Column<bool>(type: "boolean", nullable: false),
                    HipaaComplianceDocument = table.Column<byte[]>(type: "bytea", nullable: true),
                    NonDisclosureAgreement = table.Column<bool>(type: "boolean", nullable: false),
                    NonDisclosureDocument = table.Column<byte[]>(type: "bytea", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BloodTypes",
                columns: new[] { "Id", "BloodGroup" },
                values: new object[,]
                {
                    { 1, "A+" },
                    { 2, "A-" },
                    { 3, "B+" },
                    { 4, "B-" },
                    { 5, "AB+" },
                    { 6, "AB-" },
                    { 7, "O+" },
                    { 8, "O-" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { (byte)1, "Admin" },
                    { (byte)2, "Patient" },
                    { (byte)3, "Provider" }
                });

            migrationBuilder.InsertData(
                table: "UserStatus",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { (byte)1, "Initial" },
                    { (byte)2, "Pending" },
                    { (byte)3, "Active" },
                    { (byte)4, "Closed" },
                    { (byte)5, "Deleted" },
                    { (byte)6, "Blocked" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientDetails_BloodTypeId",
                table: "PatientDetails",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDetails_UserId",
                table: "PatientDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderDetails_UserId",
                table: "ProviderDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Gender",
                table: "Users",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Status",
                table: "Users",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientDetails");

            migrationBuilder.DropTable(
                name: "ProviderDetails");

            migrationBuilder.DropTable(
                name: "BloodTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserStatus");
        }
    }
}
