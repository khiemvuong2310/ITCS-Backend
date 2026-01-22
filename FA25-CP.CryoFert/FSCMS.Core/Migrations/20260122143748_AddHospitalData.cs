using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddHospitalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HospitalData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Value = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalData", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1844));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1853));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1856));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1857));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1860));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1862));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1863));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1866));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1873));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1874));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1875));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1878));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1879));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2316));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2319));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2321));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2324));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2328));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2329));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2331));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1913));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1918));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1921));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1924));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1927));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1971));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2254));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2258));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2260));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2261));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2262));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2263));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2265));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2266));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2267));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2268));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2271));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2273));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2274));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2275));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2276));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2278));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2279));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2280));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2281));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2282));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2283));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                columns: new[] { "CreatedAt", "EmergencyContact" },
                values: new object[] { new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1996), "Lê Văn An" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                columns: new[] { "CreatedAt", "EmergencyContact" },
                values: new object[] { new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2000), "Phạm Thị Giang" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                columns: new[] { "CreatedAt", "EmergencyContact" },
                values: new object[] { new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2002), "Hoàng Văn Hùng" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1681));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1686));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1687));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1689));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1691));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(1692));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2029));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2024));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2026));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2085));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2089));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2091));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2092));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2094));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2095));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2097));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2098));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2099));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2101));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2102));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2104));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2105));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2106));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2107));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2145));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2147));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2152));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2153));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2155));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2156));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2158));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2159));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2161));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2162));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2164));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2165));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2166));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2167));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2168));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2171));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2172));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2173));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2174));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2175));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2176));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2177));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2179));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2180));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2181));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2182));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2183));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2184));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2186));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2187));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2054));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2056));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2058));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 22, 14, 37, 45, 173, DateTimeKind.Utc).AddTicks(2059));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalData");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2128));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2134));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2137));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2138));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2140));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2141));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2142));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2144));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2145));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2150));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2574));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2579));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2582));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2585));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2587));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2589));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2591));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2593));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2595));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2183));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2186));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2188));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2189));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2191));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2192));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2193));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2194));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2496));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2501));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2503));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2505));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2507));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2508));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2509));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2511));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2512));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2513));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2516));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2517));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2518));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2519));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2521));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2523));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2526));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2530));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                columns: new[] { "CreatedAt", "EmergencyContact" },
                values: new object[] { new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2246), "Lê Văn F" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                columns: new[] { "CreatedAt", "EmergencyContact" },
                values: new object[] { new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2249), "Phạm Thị G" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                columns: new[] { "CreatedAt", "EmergencyContact" },
                values: new object[] { new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2250), "Hoàng Văn H" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1907));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1911));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1913));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1943));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1945));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2278));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2274));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2276));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2277));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2336));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2339));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2341));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2342));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2344));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2345));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2347));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2349));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2350));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2351));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2353));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2354));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2355));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2357));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2358));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2360));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2361));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2362));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2392));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2394));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2397));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2398));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2400));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2402));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2403));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2404));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2406));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2407));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2408));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2409));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2411));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2412));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2414));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2415));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2416));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2417));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2418));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2419));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2420));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2421));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2422));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2424));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2425));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2426));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2427));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2428));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2429));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2306));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2308));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2309));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2311));
        }
    }
}
