using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4507));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4512));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4514));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4522));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4524));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4533));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4534));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4535));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4526));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4528));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4529));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4531));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4532));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4571));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4574));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4576));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4577));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4579));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4580));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4581));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4582));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4918));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4923));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4925));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4927));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4929));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4931));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4637));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4641));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4643));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4337));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4373));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4375));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4377));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4378));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4380));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4671));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4673));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4674));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4675));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4676));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4677));

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Description", "Duration", "IsActive", "IsDeleted", "Name", "Notes", "Price", "ServiceCategoryId", "Unit", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000101"), "LAB-FSH-F", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4743), null, null, null, true, false, "FSH (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000102"), "LAB-LH-F", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4746), null, null, null, true, false, "LH (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000103"), "LAB-E2-F", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4748), null, null, null, true, false, "Estradiol (E2) (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000104"), "LAB-AMH-F", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4749), null, null, null, true, false, "AMH (female)", null, 775000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000105"), "LAB-TSH-F", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4756), null, null, null, true, false, "TSH (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000106"), "LAB-FT-F", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4758), null, null, null, true, false, "FT4/FT3 (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000107"), "LAB-PRL-F", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4759), null, null, null, true, false, "Prolactin (female)", null, 185000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000108"), "LAB-P4-F", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4760), null, null, null, true, false, "Progesterone (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000109"), "LAB-HIV", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4763), null, null, null, true, false, "HIV screening", null, 150000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000110"), "LAB-HBSAG", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4764), null, null, null, true, false, "HBsAg", null, 125000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000111"), "LAB-HCV", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4765), null, null, null, true, false, "Anti-HCV", null, 185000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000112"), "LAB-RPR", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4767), null, null, null, true, false, "RPR/VDRL (syphilis)", null, 160000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000113"), "LAB-RUB", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4768), null, null, null, true, false, "Rubella IgG/IgM", null, 400000m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000114"), "LAB-CMV", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4769), null, null, null, true, false, "CMV IgG/IgM", null, 400000m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000115"), "LAB-CHLA-PCR", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4799), null, null, null, true, false, "Chlamydia PCR", null, 575000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000116"), "LAB-GONO-PCR", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4801), null, null, null, true, false, "Gonorrhea PCR", null, 575000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000117"), "LAB-CBC", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4803), null, null, null, true, false, "Complete blood count (CBC)", null, 100000m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000118"), "LAB-GLU", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4804), null, null, null, true, false, "Blood glucose", null, 65000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000119"), "LAB-LFT", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4806), null, null, null, true, false, "AST/ALT", null, 65000m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000120"), "LAB-KFT", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4807), null, null, null, true, false, "Creatinine/Urea", null, 65000m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000121"), "LAB-ELEC", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4809), null, null, null, true, false, "Electrolyte panel", null, 160000m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000122"), "LAB-ABO", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4811), null, null, null, true, false, "ABO/Rh blood group", null, 115000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000123"), "LAB-SA", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4812), null, null, null, true, false, "Semen analysis (SA)", null, 350000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000124"), "LAB-SA-REP", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4813), null, null, null, true, false, "Semen analysis repeat", null, 250000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000125"), "LAB-MAR", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4815), null, null, null, true, false, "MAR test", null, 525000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000126"), "LAB-DFI", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4816), null, null, null, true, false, "DNA Fragmentation (DFI)", null, 2500000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000127"), "LAB-FSH-M", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4817), null, null, null, true, false, "FSH (male)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000128"), "LAB-LH-M", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4818), null, null, null, true, false, "LH (male)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000129"), "LAB-TESTO-M", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4819), null, null, null, true, false, "Testosterone (male)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000130"), "LAB-PRL-M", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4822), null, null, null, true, false, "Prolactin (male)", null, 185000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000131"), "LAB-TSH-M", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4823), null, null, null, true, false, "TSH (male)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000132"), "LAB-KARYO", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4824), null, null, null, true, false, "Karyotype", null, 1350000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000133"), "LAB-THALA", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4825), null, null, null, true, false, "Thalassemia test", null, 950000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000134"), "LAB-CFTR", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4826), null, null, null, true, false, "CFTR (cystic fibrosis)", null, 3000000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000135"), "LAB-PGT", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4827), null, null, null, true, false, "PGT-A/M per embryo", null, 19000000m, new Guid("10000000-0000-0000-0000-000000000003"), "embryo", null },
                    { new Guid("20000000-0000-0000-0000-000000000136"), "LAB-BHCG", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4829), null, null, null, true, false, "β-hCG", null, 150000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000137"), "LAB-P4-FU", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4830), null, null, null, true, false, "Progesterone follow-up", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000138"), "LAB-E2-FU", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4831), null, null, null, true, false, "Estradiol follow-up", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000139"), "US-TVS", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4832), null, null, null, true, false, "Transvaginal ultrasound", null, 225000m, new Guid("10000000-0000-0000-0000-000000000002"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000140"), "US-ABD", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4833), null, null, null, true, false, "Abdominal ultrasound", null, 200000m, new Guid("10000000-0000-0000-0000-000000000002"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000141"), "US-FOLL", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4834), null, null, null, true, false, "Follicular ultrasound", null, 225000m, new Guid("10000000-0000-0000-0000-000000000002"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000142"), "IMG-HSG", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4835), null, null, null, true, false, "HSG (hysterosalpingogram)", null, 1500000m, new Guid("10000000-0000-0000-0000-000000000002"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000143"), "IMG-HSC", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4836), null, null, null, true, false, "Diagnostic hysteroscopy", null, 4500000m, new Guid("10000000-0000-0000-0000-000000000002"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000144"), "LAB-SP-COLL", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4838), null, null, null, true, false, "Sperm collection", null, 150000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000145"), "LAB-SP-WASH", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4839), null, null, null, true, false, "Sperm wash", null, 650000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000146"), "LAB-IUI", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4840), null, null, null, true, false, "IUI procedure", null, 3500000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000147"), "LAB-OPU", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4841), null, null, null, true, false, "OPU (oocyte pickup)", null, 11500000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000148"), "LAB-ICSI", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4842), null, null, null, true, false, "ICSI", null, 9000000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000149"), "LAB-EMB-D2D5", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4843), null, null, null, true, false, "Embryo culture Day2–Day5", null, 8500000m, new Guid("10000000-0000-0000-0000-000000000003"), "cycle", null }
                });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4711));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4714));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4716));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4717));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7142));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7149));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7150));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7189));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7192));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7202));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7204));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7205));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7193));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7195));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7196));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7198));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7199));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7200));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7235));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7238));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7240));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7241));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7242));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7243));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7246));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7480));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7485));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7488));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7491));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7493));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7268));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7274));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(6992));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(6998));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(6999));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7001));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7003));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7004));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7297));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7299));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7326));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7327));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7329));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7330));

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Description", "Duration", "IsActive", "IsDeleted", "Name", "Notes", "Price", "ServiceCategoryId", "Unit", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "CONS-INIT", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7390), null, "First-time visit and clinical assessment", 30, true, false, "Initial fertility consultation", null, 120m, new Guid("10000000-0000-0000-0000-000000000001"), "session", null },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "CONS-FUP", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7394), null, "Follow-up review and plan", 20, true, false, "Follow-up consultation", null, 80m, new Guid("10000000-0000-0000-0000-000000000001"), "session", null },
                    { new Guid("20000000-0000-0000-0000-000000000010"), "US-TVS", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7396), null, null, 15, true, false, "Transvaginal ultrasound", null, 60m, new Guid("10000000-0000-0000-0000-000000000002"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000011"), "LAB-HORM", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7398), null, null, null, true, false, "Baseline hormone panel (AMH/FSH/LH/E2/PRL)", null, 150m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000012"), "SA", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7400), null, null, null, true, false, "Semen analysis", null, 40m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000020"), "OPU", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7401), null, null, null, true, false, "Oocyte retrieval (OPU)", null, 1500m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000021"), "SP-PREP", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7403), null, null, null, true, false, "Sperm preparation (IUI/IVF)", null, 90m, new Guid("10000000-0000-0000-0000-000000000003"), "prep", null },
                    { new Guid("20000000-0000-0000-0000-000000000022"), "EMB-CULT", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7405), null, null, null, true, false, "Embryo culture (day 1-5)", null, 1500m, new Guid("10000000-0000-0000-0000-000000000003"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000023"), "ICSI", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7406), null, null, null, true, false, "ICSI", null, 1200m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000024"), "ET", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7407), null, null, null, true, false, "Embryo transfer (ET)", null, 800m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000030"), "VIT-OOC", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7408), null, null, null, true, false, "Oocyte vitrification", null, 600m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000031"), "CRYO-SP", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7409), null, null, null, true, false, "Sperm cryopreservation", null, 120m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000032"), "VIT-EMB", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7410), null, null, null, true, false, "Embryo vitrification", null, 700m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000033"), "STORE-ANNUAL", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7412), null, null, null, true, false, "Annual storage fee (per specimen)", null, 150m, new Guid("10000000-0000-0000-0000-000000000004"), "year", null },
                    { new Guid("20000000-0000-0000-0000-000000000034"), "THAW", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7413), null, null, null, true, false, "Specimen thawing", null, 200m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000040"), "IUI", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7414), null, null, null, true, false, "Intrauterine insemination (IUI)", null, 250m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000041"), "IVF", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7415), null, null, null, true, false, "In vitro fertilization (IVF) cycle", null, 12000m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000042"), "FET", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7417), null, null, null, true, false, "Frozen embryo transfer (FET)", null, 3500m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000050"), "GONA-PEN", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7418), null, null, null, true, false, "Gonadotropin stimulation (per pen)", null, 90m, new Guid("10000000-0000-0000-0000-000000000006"), "pen", null },
                    { new Guid("20000000-0000-0000-0000-000000000051"), "HCG", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7419), null, null, null, true, false, "Trigger injection (hCG)", null, 20m, new Guid("10000000-0000-0000-0000-000000000006"), "dose", null },
                    { new Guid("20000000-0000-0000-0000-000000000060"), "ADMIN-MR", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7420), null, null, null, true, false, "Medical record creation fee", null, 10m, new Guid("10000000-0000-0000-0000-000000000007"), "case", null },
                    { new Guid("20000000-0000-0000-0000-000000000061"), "ADMIN-CERT", new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7421), null, null, null, true, false, "Certificate/Report issuance", null, 15m, new Guid("10000000-0000-0000-0000-000000000007"), "doc", null }
                });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7360));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7361));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 17, 59, 37, 713, DateTimeKind.Utc).AddTicks(7363));
        }
    }
}
