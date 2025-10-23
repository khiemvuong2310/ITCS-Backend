using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDelete", "RoleName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System Administrator with full access to all features", false, "Admin", new DateTime(2025, 10, 23, 14, 54, 42, 982, DateTimeKind.Utc).AddTicks(165) },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Medical Doctor - Can manage patients, treatments, and medical records", false, "Doctor", new DateTime(2025, 10, 23, 14, 54, 42, 982, DateTimeKind.Utc).AddTicks(174) },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Laboratory Technician - Manages lab samples and cryopreservation", false, "LaboratoryTechnician", new DateTime(2025, 10, 23, 14, 54, 42, 982, DateTimeKind.Utc).AddTicks(176) },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Front Desk Receptionist - Manages appointments and customer service", false, "Receptionist", new DateTime(2025, 10, 23, 14, 54, 42, 982, DateTimeKind.Utc).AddTicks(177) },
                    { 5, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Patient/Customer - Can book appointments and view their records", false, "Patient", new DateTime(2025, 10, 23, 14, 54, 42, 982, DateTimeKind.Utc).AddTicks(179) },
                    { 6, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "General User - Basic access to the system", false, "User", new DateTime(2025, 10, 23, 14, 54, 42, 982, DateTimeKind.Utc).AddTicks(180) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
