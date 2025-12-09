using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewMedicine_CryoPackages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1423));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1430));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1432));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1442));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1445));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1454));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1455));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1456));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1448));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1449));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1450));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1452));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1453));

            migrationBuilder.InsertData(
                table: "CryoPackages",
                columns: new[] { "Id", "Benefits", "CreatedAt", "DeletedAt", "Description", "DurationMonths", "IncludesInsurance", "InsuranceAmount", "IsActive", "IsDeleted", "MaxSamples", "Notes", "PackageName", "Price", "SampleType", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1845), null, "Initial fee 8,000,000 VND; storage 8,000,000 VND", 12, false, null, true, false, 10, "1-year storage package for up to 10 oocytes", "Oocyte Freezing - 1 Year", 8000000m, 1, null },
                    { new Guid("50000000-0000-0000-0000-000000000002"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1848), null, "Initial fee 8,000,000 VND; storage 20,000,000 VND", 36, false, null, true, false, 20, "Discounted compared to annual renewal", "Oocyte Freezing - 3 Years", 8000000m, 1, null },
                    { new Guid("50000000-0000-0000-0000-000000000003"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1850), null, "Initial fee 8,000,000 VND; storage 30,000,000 VND", 60, false, null, true, false, 30, "Best value for long-term storage", "Oocyte Freezing - 5 Years", 8000000m, 1, null },
                    { new Guid("50000000-0000-0000-0000-000000000004"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1852), null, "Initial fee 2,000,000 VND; storage 3,000,000 VND", 12, false, null, true, false, 5, "Storage for up to 5 sperm samples", "Sperm Freezing - 1 Year", 2000000m, 2, null },
                    { new Guid("50000000-0000-0000-0000-000000000005"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1855), null, "Initial fee 2,000,000 VND; storage 7,000,000 VND", 36, false, null, true, false, 10, "Cost-effective multi-year plan", "Sperm Freezing - 3 Years", 2000000m, 2, null },
                    { new Guid("50000000-0000-0000-0000-000000000006"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1857), null, "Initial fee 2,000,000 VND; storage 10,000,000 VND", 60, false, null, true, false, 15, "Optimal for long-term preservation", "Sperm Freezing - 5 Years", 2000000m, 2, null },
                    { new Guid("50000000-0000-0000-0000-000000000007"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1860), null, "Initial fee 10,000,000 VND; storage 10,000,000 VND", 12, false, null, true, false, 6, "Calculated for up to 6 embryos", "Embryo Freezing - 1 Year", 10000000m, 3, null },
                    { new Guid("50000000-0000-0000-0000-000000000008"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1865), null, "Initial fee 10,000,000 VND; storage 25,000,000 VND", 36, false, null, true, false, 12, "Significant savings", "Embryo Freezing - 3 Years", 10000000m, 3, null },
                    { new Guid("50000000-0000-0000-0000-000000000009"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1904), null, "Initial fee 10,000,000 VND; storage 35,000,000 VND", 60, false, null, true, false, 18, "Long-term plan, priority for IVF patients", "Embryo Freezing - 5 Years", 10000000m, 3, null }
                });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1493));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1495));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1496));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1497));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1500));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1501));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes", "SideEffects" },
                values: new object[] { new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1793), "50 mg/day (max 150 mg/day)", "Oral", null, "Ovarian stimulation D2–D6", "Clomiphene Citrate", "IUI", null });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes", "SideEffects" },
                values: new object[] { new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1795), "2.5–5 mg/day", "Oral", null, "Ovarian stimulation D2–D6, PCOS", "Letrozole", "IUI", null });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes", "SideEffects" },
                values: new object[] { new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1798), "75–150 IU/day", "Subcutaneous injection", null, "Ovarian stimulation from D2–D3", "Gonal-F / Puregon", "IUI/IVF", null });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes", "SideEffects" },
                values: new object[] { new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1799), "75–150 IU/day", "Subcutaneous injection", null, "Ovarian stimulation", "Menopur", "IUI/IVF", null });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                columns: new[] { "Contraindication", "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes" },
                values: new object[] { null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1800), "150 IU FSH + 75 IU LH/day", "Subcutaneous injection", null, "Stimulation for poor responders", "Pergoveris", "IVF" });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes" },
                values: new object[] { new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1802), "0.25 mg/day", "Subcutaneous injection", null, "Prevent premature ovulation from D5", "Cetrotide", "IVF" });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Contraindication", "CreatedAt", "DeletedAt", "Dosage", "Form", "GenericName", "Indication", "IsActive", "IsDeleted", "Name", "Notes", "SideEffects", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000007"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1803), null, "0.25 mg/day", "Subcutaneous injection", null, "Prevent premature ovulation from D5", true, false, "Orgalutran", "IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000008"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1804), null, "250 mcg single dose", "Subcutaneous injection", null, "Trigger when follicle is 18–20mm", true, false, "Ovidrel", "IUI/IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000009"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1805), null, "5000–10000 IU single dose", "Intramuscular injection", null, "Trigger when follicle is 18–20mm", true, false, "Pregnyl", "IUI/IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000010"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1806), null, "0.1 mg single dose", "Subcutaneous injection", null, "Trigger to reduce OHSS risk", true, false, "Decapeptyl 0.1 mg", "IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000011"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1807), null, "200 mg x 2–3 times/day", "Vaginal", null, "Post IUI/OPU/ET support", true, false, "Progesterone Suppositories", "IUI/IVF/FET", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000012"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1808), null, "10 mg x 2–3 times/day", "Oral", null, "Luteal phase support", true, false, "Duphaston", "IUI/IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000013"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1809), null, "1 applicator/day", "Vaginal", null, "Support post-ET", true, false, "Crinone 8%", "IVF/FET", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000014"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1810), null, "50 mg x 2–3 times/week", "Intramuscular injection", null, "Luteal support post OPU/ET", true, false, "Proluton", "IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000015"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1812), null, "2 mg x 2–3 times/day", "Oral", null, "Endometrial thickening", true, false, "Progynova", "FET", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000016"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1813), null, "50–100 mcg/patch every 2 days", "Transdermal patch", null, "Endometrial thickening", true, false, "Estradot", "FET", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000017"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1814), null, "200–300 mg/day", "Oral", null, "Sperm quality improvement", true, false, "CoQ10", "Male", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000018"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1815), null, "400 IU/day", "Oral", null, "Antioxidant for sperm", true, false, "Vitamin E", "Male", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000019"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1816), null, "25 mg/day", "Oral", null, "Spermatogenesis support", true, false, "Clomiphene (Male)", "Male", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000020"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1817), null, "1500 IU x 2–3 times/week", "Injection", null, "Stimulate spermatogenesis", true, false, "HCG (Male)", "Male", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000021"), null, new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1818), null, "150 IU x 2–3 times/week", "Injection", null, "Increase sperm production", true, false, "FSH (Male)", "Male", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1553));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1561));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1291));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1297));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1298));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1300));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1301));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1302));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1583));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1585));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1586));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1588));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1589));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1646));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1652));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1653));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1655));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1656));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1657));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1659));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1661));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1662));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1664));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1665));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1666));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1667));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1695));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1698));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1699));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1702));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1703));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1704));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1706));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1708));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1709));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1710));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1712));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1713));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1716));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1718));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1719));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1720));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1722));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1723));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1725));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1726));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1727));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1728));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1729));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1732));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1733));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1734));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1736));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1737));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1618));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1622));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 14, 46, 13, 83, DateTimeKind.Utc).AddTicks(1623));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"));

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
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes", "SideEffects" },
                values: new object[] { new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4918), "300 IU", "Injection", "Recombinant FSH", "Ovarian stimulation", "Follitropin alfa", "Pen device", "Headache, abdominal pain" });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes", "SideEffects" },
                values: new object[] { new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4923), "5,000 IU", "Injection", "hCG", "Ovulation trigger", "Chorionic gonadotropin (hCG)", "Store refrigerated", "Injection site pain" });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes", "SideEffects" },
                values: new object[] { new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4925), "200 mg", "Capsule", "Progesterone", "Luteal phase support", "Progesterone", "Taken at bedtime", "Drowsiness" });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes", "SideEffects" },
                values: new object[] { new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4927), "2.5 mg", "Tablet", "Letrozole", "Ovulation induction", "Letrozole", null, "Fatigue, dizziness" });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                columns: new[] { "Contraindication", "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes" },
                values: new object[] { "Pregnancy", new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4929), "100 mg", "Tablet", "Doxycycline hyclate", "Infection prophylaxis", "Doxycycline", null });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "Dosage", "Form", "GenericName", "Indication", "Name", "Notes" },
                values: new object[] { new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4931), "2 mg", "Tablet", "Estradiol", "Endometrial preparation", "Estradiol valerate", null });

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

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4743));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4746));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4748));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4749));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4756));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4758));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4759));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4760));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4763));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4764));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4765));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4767));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4768));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4769));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4799));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4801));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4803));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4804));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4806));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4807));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4809));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4811));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4812));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4813));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4815));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4816));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4817));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4818));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4819));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4822));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4823));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4824));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4825));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4826));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4827));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4829));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4831));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4832));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4833));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4834));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4835));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4836));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4838));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4839));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4840));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4841));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4842));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 13, 9, 9, 108, DateTimeKind.Utc).AddTicks(4843));

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
    }
}
