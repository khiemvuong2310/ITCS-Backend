using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class Update_Embryo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LabSampleOocyteId",
                table: "LabSampleEmbryos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LabSampleSpermId",
                table: "LabSampleEmbryos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6792));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6804));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6806));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6815));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6817));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6820));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6822));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6824));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7020));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7025));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7619));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7626));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7632));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7634));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7637));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7067));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7159));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6359));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6370));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6373));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6375));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6377));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7226));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7227));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7229));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7231));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7234));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7351));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7359));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7361));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7364));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7366));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7368));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7371));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7373));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7375));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7378));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7380));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7383));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7387));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7390));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7393));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7395));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7398));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7286));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 15, 20, 59, 47, 186, DateTimeKind.Utc).AddTicks(7298));

            migrationBuilder.CreateIndex(
                name: "IX_LabSampleEmbryos_LabSampleOocyteId",
                table: "LabSampleEmbryos",
                column: "LabSampleOocyteId");

            migrationBuilder.CreateIndex(
                name: "IX_LabSampleEmbryos_LabSampleSpermId",
                table: "LabSampleEmbryos",
                column: "LabSampleSpermId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabSampleEmbryos_LabSampleOocytes_LabSampleOocyteId",
                table: "LabSampleEmbryos",
                column: "LabSampleOocyteId",
                principalTable: "LabSampleOocytes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LabSampleEmbryos_LabSampleSperms_LabSampleSpermId",
                table: "LabSampleEmbryos",
                column: "LabSampleSpermId",
                principalTable: "LabSampleSperms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabSampleEmbryos_LabSampleOocytes_LabSampleOocyteId",
                table: "LabSampleEmbryos");

            migrationBuilder.DropForeignKey(
                name: "FK_LabSampleEmbryos_LabSampleSperms_LabSampleSpermId",
                table: "LabSampleEmbryos");

            migrationBuilder.DropIndex(
                name: "IX_LabSampleEmbryos_LabSampleOocyteId",
                table: "LabSampleEmbryos");

            migrationBuilder.DropIndex(
                name: "IX_LabSampleEmbryos_LabSampleSpermId",
                table: "LabSampleEmbryos");

            migrationBuilder.DropColumn(
                name: "LabSampleOocyteId",
                table: "LabSampleEmbryos");

            migrationBuilder.DropColumn(
                name: "LabSampleSpermId",
                table: "LabSampleEmbryos");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9137));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9175));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9177));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9179));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9181));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9182));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9213));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9218));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9421));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9425));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9427));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9428));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9430));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9239));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9242));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9243));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8982));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8992));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8994));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8995));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8997));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9268));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9270));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9271));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9272));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9273));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9274));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9275));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9331));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9336));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9338));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9340));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9367));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9369));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9371));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9373));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9374));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9375));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9378));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9379));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9386));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9387));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9389));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9303));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9304));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9306));
        }
    }
}
