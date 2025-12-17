using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class MaturityStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaturityStage",
                table: "LabSampleOocytes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6908));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6917));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6921));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6924));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6926));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6941));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6943));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6945));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6928));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6930));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6932));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6934));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6937));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6939));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9122));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9129));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9132));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9135));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9138));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9143));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9146));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9149));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7027));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7033));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7035));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7037));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7039));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7045));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7048));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8956));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8968));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8974));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8978));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8980));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8983));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8987));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8990));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8992));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8993));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8995));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8998));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9000));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9002));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9004));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9006));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9009));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(9011));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7100));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6072));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6078));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6081));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6084));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6085));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(6087));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7710));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7703));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7706));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7708));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7814));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7819));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7821));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7826));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7829));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7832));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7834));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7836));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7839));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7841));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7843));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7846));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7848));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7850));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7853));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7856));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7858));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7863));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7867));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8071));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8075));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8082));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8084));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8094));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8096));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8103));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8105));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8107));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8110));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8112));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8117));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8119));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8121));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8123));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8125));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8127));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(8129));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7759));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7763));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7766));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 17, 14, 50, 58, 167, DateTimeKind.Utc).AddTicks(7768));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LabSampleOocytes",
                keyColumn: "MaturityStage",
                keyValue: null,
                column: "MaturityStage",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MaturityStage",
                table: "LabSampleOocytes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6397));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6405));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6408));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6410));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6414));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6563));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6565));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6568));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6416));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6424));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6426));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6560));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7358));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7361));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7364));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7367));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7437));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7440));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7442));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6625));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6632));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6634));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6636));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6715));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6717));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6719));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6721));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7259));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7267));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7274));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7275));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7277));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7279));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7280));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7282));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7285));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7287));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7288));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7290));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7294));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7297));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7299));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7300));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6773));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6781));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6783));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6087));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6094));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6097));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6099));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6102));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6830));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6823));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6828));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6928));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6935));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6938));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6941));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6943));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6946));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6948));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6950));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6953));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6955));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6957));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6960));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7033));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7036));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7038));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7040));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7042));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7044));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7049));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7051));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7053));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7055));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7057));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7060));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7062));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7064));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7066));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7068));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7069));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7073));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7076));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7079));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7082));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7085));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7087));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7089));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7091));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7093));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7095));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7097));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7099));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7102));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7104));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7106));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(7174));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6876));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6880));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6882));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 16, 17, 45, 43, 7, DateTimeKind.Utc).AddTicks(6884));
        }
    }
}
