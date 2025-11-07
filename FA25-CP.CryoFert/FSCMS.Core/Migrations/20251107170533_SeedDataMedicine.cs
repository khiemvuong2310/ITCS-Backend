using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 961, DateTimeKind.Utc).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 961, DateTimeKind.Utc).AddTicks(9998));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(7));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(26));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(39));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(47));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(53));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(60));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(310));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(327));

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Contraindication", "CreatedAt", "DeletedAt", "Dosage", "Form", "GenericName", "Indication", "IsActive", "IsDeleted", "Name", "Notes", "SideEffects", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), null, new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(2058), null, "300 IU", "Injection", "Recombinant FSH", "Ovarian stimulation", true, false, "Follitropin alfa", "Pen device", "Headache, abdominal pain", null },
                    { new Guid("40000000-0000-0000-0000-000000000002"), null, new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(2074), null, "5,000 IU", "Injection", "hCG", "Ovulation trigger", true, false, "Chorionic gonadotropin (hCG)", "Store refrigerated", "Injection site pain", null },
                    { new Guid("40000000-0000-0000-0000-000000000003"), null, new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(2082), null, "200 mg", "Capsule", "Progesterone", "Luteal phase support", true, false, "Progesterone", "Taken at bedtime", "Drowsiness", null },
                    { new Guid("40000000-0000-0000-0000-000000000004"), null, new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(2090), null, "2.5 mg", "Tablet", "Letrozole", "Ovulation induction", true, false, "Letrozole", null, "Fatigue, dizziness", null },
                    { new Guid("40000000-0000-0000-0000-000000000005"), "Pregnancy", new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(2097), null, "100 mg", "Tablet", "Doxycycline hyclate", "Infection prophylaxis", true, false, "Doxycycline", null, null, null },
                    { new Guid("40000000-0000-0000-0000-000000000006"), null, new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(2105), null, "2 mg", "Tablet", "Estradiol", "Endometrial preparation", true, false, "Estradiol valerate", null, null, null }
                });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(528));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(545));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 961, DateTimeKind.Utc).AddTicks(9167));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 961, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 961, DateTimeKind.Utc).AddTicks(9210));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 961, DateTimeKind.Utc).AddTicks(9215));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 961, DateTimeKind.Utc).AddTicks(9222));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 961, DateTimeKind.Utc).AddTicks(9228));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(757));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(934));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(939));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(944));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(950));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(955));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1448), 120m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1466), 80m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1478), 60m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1487), 150m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1495), 40m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1503), 1500m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1510), 90m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1517), 1500m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1525), 1200m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1532), 800m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1540), 600m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1547), 120m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1554), 700m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1561), 150m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1570), 200m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1578), 250m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1584), 12000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1592), 3500m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1599), 90m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1606), 20m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1615), 10m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1621), 15m });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1208));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1221));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1228));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 8, 0, 5, 29, 962, DateTimeKind.Utc).AddTicks(1236));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2788));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2794));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2797));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2798));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2848));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2879));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2882));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2901));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2906));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2909));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2663));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2671));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2674));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2676));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2677));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2678));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2930));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2932));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2933));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2934));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2935));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2936));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2937));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2984), 500000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2987), 300000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2989), 400000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2991), 1500000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2992), 600000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2994), 7000000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2995), 900000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3021), 5000000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3023), 8000000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3024), 4000000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3025), 3000000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3027), 1200000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3028), 3500000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3029), 1500000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3030), 1000000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3032), 2500000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3033), 35000000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3034), 12000000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3035), 1200000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3037), 450000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3038), 100000m });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                columns: new[] { "CreatedAt", "Price" },
                values: new object[] { new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(3040), 150000m });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2961));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 22, 25, 45, 930, DateTimeKind.Utc).AddTicks(2962));
        }
    }
}
