using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDOB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "Accounts",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { null, new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9008) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { null, new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9013) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { null, new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9015) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1980, 5, 15), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9024) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1985, 8, 20), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9026) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1990, 3, 10), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9035) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1992, 7, 25), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9036) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1988, 11, 5), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9037) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1978, 2, 14), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9027) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1982, 11, 30), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9028) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1975, 6, 8), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1988, 9, 22), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9031) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1983, 4, 5), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9033) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateOnly(1979, 12, 18), new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9034) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9066));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9069));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9070));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9073));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9075));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9076));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9077));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9078));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9296));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9301));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9303));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9306));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9308));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9310));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9106));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9133));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9135));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(8861));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(8865));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(8867));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(8868));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(8869));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9162));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9164));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9165));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9166));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9166));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9167));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9168));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9220));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9223));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9226));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9227));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9229));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9231));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9232));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9234));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9235));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9236));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9237));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9238));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9239));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9241));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9242));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9244));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9245));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9246));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9248));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9267));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9269));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9270));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9192));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9194));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9196));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 1, 14, 11, 25, 11, DateTimeKind.Utc).AddTicks(9197));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Accounts",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { null, new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5580) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { null, new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5585) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { null, new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5588) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5594) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1985, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5598) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1990, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5650) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1992, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5652) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1988, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5653) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1978, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1982, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5601) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1975, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5603) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1988, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5605) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1983, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5647) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                columns: new[] { "BirthDate", "CreatedAt" },
                values: new object[] { new DateTime(1979, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5649) });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5684));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5687));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5691));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5693));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5694));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5695));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5920));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5925));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5927));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5930));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5931));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5720));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5724));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5726));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5424));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5428));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5430));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5432));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5751));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5753));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5754));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5755));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5756));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5757));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5758));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5850));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5854));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5857));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5859));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5861));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5862));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5864));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5865));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5866));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5868));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5869));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5871));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5875));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5876));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5877));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5879));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5880));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5883));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5885));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5789));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5819));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5821));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 30, 13, 23, 0, 293, DateTimeKind.Utc).AddTicks(5823));
        }
    }
}
