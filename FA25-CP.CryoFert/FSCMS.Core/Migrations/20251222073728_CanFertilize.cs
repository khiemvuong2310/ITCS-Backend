using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class CanFertilize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanFertilize",
                table: "LabSamples",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4413));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4423));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4427));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4429));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4432));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4522));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4524));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4526));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4434));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4512));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4515));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4516));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4518));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5233));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5236));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5239));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5242));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5247));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5249));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5251));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4582));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4587));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4589));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4591));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4593));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4595));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4597));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4598));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5088));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5093));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5095));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5097));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5099));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5101));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5102));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5104));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5106));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5107));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5109));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5110));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5159));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5161));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5164));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5166));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5169));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5171));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5173));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5174));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4641));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4644));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4647));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4192));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4199));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4202));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4203));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4206));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4208));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4699));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4694));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4697));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4698));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4861));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4865));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4867));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4870));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4872));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4878));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4881));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4884));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4886));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4888));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4890));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4892));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4894));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4898));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4900));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4902));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4903));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4906));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4907));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4909));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4911));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4913));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4915));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4916));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4918));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4920));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4922));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4925));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4974));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4979));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4981));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4983));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4984));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4986));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4988));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4990));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4992));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4994));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4995));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4997));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4999));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5001));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5002));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5005));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5007));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(5008));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4811));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4816));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 22, 7, 37, 25, 895, DateTimeKind.Utc).AddTicks(4818));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanFertilize",
                table: "LabSamples");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2067));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2075));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2078));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2080));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2082));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2131));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2085));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2087));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2088));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2091));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2093));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2129));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2891));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2895));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2898));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2901));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2906));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2908));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2911));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2915));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2192));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2197));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2199));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2201));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2203));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2206));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2208));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2210));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2741));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2748));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2752));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2755));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2757));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2759));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2760));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2762));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2764));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2765));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2767));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2769));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2771));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2774));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2827));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2830));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2832));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2256));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2260));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2262));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(1774));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(1780));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(1782));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(1784));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(1785));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(1787));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2314));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2307));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2310));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2312));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2489));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2494));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2497));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2499));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2501));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2503));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2505));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2507));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2509));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2511));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2513));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2515));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2518));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2525));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2528));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2530));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2532));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2534));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2536));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2538));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2540));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2542));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2544));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2546));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2548));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2550));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2552));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2554));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2556));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2558));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2560));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2562));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2625));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2629));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2631));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2633));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2635));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2637));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2639));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2640));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2643));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2646));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2647));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2649));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2651));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2653));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2655));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2355));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2360));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2362));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 19, 14, 35, 53, 308, DateTimeKind.Utc).AddTicks(2364));
        }
    }
}
