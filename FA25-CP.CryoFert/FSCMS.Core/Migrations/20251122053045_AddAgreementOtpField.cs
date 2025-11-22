using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddAgreementOtpField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OTPSentDate",
                table: "Agreements",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatureIPAddress",
                table: "Agreements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SignatureMethod",
                table: "Agreements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "SignedDate",
                table: "Agreements",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1487));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1493));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1494));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1501));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1504));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1505));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1506));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1508));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1559));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1563));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1762));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1767));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1768));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1771));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1774));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1585));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1588));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1370));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1374));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1375));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1377));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1378));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1379));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1614));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1616));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1617));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1618));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1619));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1621));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1679));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1680));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1682));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1684));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1685));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1687));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1688));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1689));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1690));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1691));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1692));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1722));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1725));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1726));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1727));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1729));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1731));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1732));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1734));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1645));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1648));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1649));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 22, 5, 30, 43, 29, DateTimeKind.Utc).AddTicks(1650));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTPSentDate",
                table: "Agreements");

            migrationBuilder.DropColumn(
                name: "SignatureIPAddress",
                table: "Agreements");

            migrationBuilder.DropColumn(
                name: "SignatureMethod",
                table: "Agreements");

            migrationBuilder.DropColumn(
                name: "SignedDate",
                table: "Agreements");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3907));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3912));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3914));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3921));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3923));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3925));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3957));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3959));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3986));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3989));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4197));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4200));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4202));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4203));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4205));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4207));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4013));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4016));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4018));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3761));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3765));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3767));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3768));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3769));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4044));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4046));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4047));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4048));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4049));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4049));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4109));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4113));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4115));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4117));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4119));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4120));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4122));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4123));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4142));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4144));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4145));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4150));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4152));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4153));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4155));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4156));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4159));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4160));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4075));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4077));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4079));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 21, 16, 45, 9, 484, DateTimeKind.Utc).AddTicks(4080));
        }
    }
}
