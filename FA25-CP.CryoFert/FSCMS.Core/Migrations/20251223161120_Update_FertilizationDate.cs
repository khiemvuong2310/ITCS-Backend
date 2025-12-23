using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class Update_FertilizationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FertilizationDate",
                table: "LabSampleEmbryos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7165));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7175));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7180));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7203));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7206));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7181));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7183));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7194));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7196));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7200));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7202));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7932));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7936));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7939));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7943));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7945));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7947));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7950));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7257));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7263));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7266));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7268));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7274));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7783));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7788));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7790));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7793));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7795));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7797));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7802));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7804));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7806));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7807));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7811));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7867));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7870));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7872));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7874));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7875));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7313));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(6904));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(6907));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(6908));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(6910));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(6911));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7375));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7373));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7374));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7470));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7474));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7476));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7478));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7480));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7482));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7485));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7488));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7493));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7496));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7498));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7500));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7502));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7504));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7506));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7511));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7513));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7515));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7518));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7522));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7524));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7525));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7527));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7529));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7531));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7533));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7536));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7538));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7540));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7663));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7667));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7669));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7671));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7673));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7675));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7678));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7683));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7689));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7691));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7693));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7695));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7697));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7699));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7701));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7416));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7428));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 16, 11, 19, 498, DateTimeKind.Utc).AddTicks(7430));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FertilizationDate",
                table: "LabSampleEmbryos");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8398));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8406));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8421));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8424));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8437));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8439));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8445));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8426));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8429));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8431));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8434));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8435));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9106));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9110));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9113));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9116));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9119));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9121));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9124));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9126));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9128));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8496));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8500));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8503));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8505));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8507));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8509));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8511));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8513));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9006));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9011));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9013));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9015));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9017));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9018));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9021));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9029));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9031));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9033));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9038));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9040));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9042));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9043));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9045));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9047));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9048));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9050));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9052));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(9054));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8556));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8561));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8563));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8161));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8168));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8170));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8172));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8173));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8175));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8662));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8818));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8823));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8826));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8827));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8829));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8831));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8837));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8839));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8841));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8843));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8845));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8847));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8852));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8857));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8862));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8865));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8866));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8869));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8872));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8875));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8877));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8887));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8889));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8892));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8894));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8896));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8898));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8900));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8902));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8905));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8906));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8910));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8914));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8917));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8919));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8921));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8923));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8925));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8926));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8928));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8932));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8934));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8936));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8763));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8769));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8772));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 23, 15, 0, 9, 483, DateTimeKind.Utc).AddTicks(8777));
        }
    }
}
