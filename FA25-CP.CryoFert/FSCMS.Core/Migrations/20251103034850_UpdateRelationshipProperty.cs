using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Relationships",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Relationships",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "RequestedBy",
                table: "Relationships",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "Relationships",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RespondedBy",
                table: "Relationships",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Relationships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8095));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8102));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8108));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8110));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8111));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8113));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8114));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8142));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8147));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8166));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8171));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8174));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(7932));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(7943));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(7946));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8196));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8198));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8199));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8200));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8201));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8202));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8203));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8262));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8266));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8268));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8270));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8272));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8273));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8275));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8276));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8277));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8279));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8281));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8282));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8284));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8285));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8286));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8287));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8288));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8290));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8291));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8292));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 3, 10, 48, 48, 149, DateTimeKind.Utc).AddTicks(8293));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "RequestedBy",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "RespondedBy",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Relationships");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7608));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7610));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7616));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7620));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7621));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7622));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7623));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7751));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7759));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7760));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7250));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7258));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7261));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7262));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7264));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7786));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7788));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7789));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7790));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7791));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7792));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7793));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7852));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7855));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7858));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7862));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7863));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7864));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7866));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7867));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7868));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7869));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7870));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7871));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7872));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7874));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7876));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 2, 0, 23, 20, 775, DateTimeKind.Utc).AddTicks(7881));
        }
    }
}
