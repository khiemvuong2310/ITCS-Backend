using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataRoleService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "RoleCode", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6495), null, "System administrator", false, "ADMIN", "Admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6500), null, "Medical doctor", false, "DOCTOR", "Doctor", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6501), null, "Lab technician", false, "LAB_TECH", "Laboratory Technician", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6503), null, "Front desk staff", false, "RECEPTIONIST", "Receptionist", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6504), null, "Patient user", false, "PATIENT", "Patient", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6505), null, "General user", false, "USER", "User", null }
                });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Description", "DisplayOrder", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "CONS", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6687), null, "Clinical consultations", 1, true, false, "Consultation", null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "DIAG", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6690), null, "Diagnostic tests and imaging", 2, true, false, "Diagnostics & Imaging", null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "LAB", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6692), null, "Embryology and andrology procedures", 3, true, false, "Laboratory Procedures", null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "CRYO", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6693), null, "Cryopreservation and storage services", 4, true, false, "Cryostorage & Logistics", null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "TRMT", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6694), null, "IUI/IVF related procedures", 5, true, false, "Treatment Procedures", null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "MED", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6695), null, "Medications and injections", 6, true, false, "Medications", null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "ADMIN", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6696), null, "Administrative fees", 7, true, false, "Administrative & Others", null }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Description", "Duration", "IsActive", "IsDeleted", "Name", "Notes", "Price", "ServiceCategoryId", "Unit", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "CONS-INIT", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6726), null, "First-time visit and clinical assessment", 30, true, false, "Initial fertility consultation", null, 500000m, new Guid("10000000-0000-0000-0000-000000000001"), "session", null },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "CONS-FUP", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6731), null, "Follow-up review and plan", 20, true, false, "Follow-up consultation", null, 300000m, new Guid("10000000-0000-0000-0000-000000000001"), "session", null },
                    { new Guid("20000000-0000-0000-0000-000000000010"), "US-TVS", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6733), null, null, 15, true, false, "Transvaginal ultrasound", null, 400000m, new Guid("10000000-0000-0000-0000-000000000002"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000011"), "LAB-HORM", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6735), null, null, null, true, false, "Baseline hormone panel (AMH/FSH/LH/E2/PRL)", null, 1500000m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000012"), "SA", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6737), null, null, null, true, false, "Semen analysis", null, 600000m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000020"), "OPU", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6739), null, null, null, true, false, "Oocyte retrieval (OPU)", null, 7000000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000021"), "SP-PREP", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6740), null, null, null, true, false, "Sperm preparation (IUI/IVF)", null, 900000m, new Guid("10000000-0000-0000-0000-000000000003"), "prep", null },
                    { new Guid("20000000-0000-0000-0000-000000000022"), "EMB-CULT", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6771), null, null, null, true, false, "Embryo culture (day 1-5)", null, 5000000m, new Guid("10000000-0000-0000-0000-000000000003"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000023"), "ICSI", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6772), null, null, null, true, false, "ICSI", null, 8000000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000024"), "ET", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6774), null, null, null, true, false, "Embryo transfer (ET)", null, 4000000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000030"), "VIT-OOC", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6775), null, null, null, true, false, "Oocyte vitrification", null, 3000000m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000031"), "CRYO-SP", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6777), null, null, null, true, false, "Sperm cryopreservation", null, 1200000m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000032"), "VIT-EMB", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6778), null, null, null, true, false, "Embryo vitrification", null, 3500000m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000033"), "STORE-ANNUAL", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6779), null, null, null, true, false, "Annual storage fee (per specimen)", null, 1500000m, new Guid("10000000-0000-0000-0000-000000000004"), "year", null },
                    { new Guid("20000000-0000-0000-0000-000000000034"), "THAW", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6780), null, null, null, true, false, "Specimen thawing", null, 1000000m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000040"), "IUI", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6782), null, null, null, true, false, "Intrauterine insemination (IUI)", null, 2500000m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000041"), "IVF", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6783), null, null, null, true, false, "In vitro fertilization (IVF) cycle", null, 35000000m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000042"), "FET", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6784), null, null, null, true, false, "Frozen embryo transfer (FET)", null, 12000000m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000050"), "GONA-PEN", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6786), null, null, null, true, false, "Gonadotropin stimulation (per pen)", null, 1200000m, new Guid("10000000-0000-0000-0000-000000000006"), "pen", null },
                    { new Guid("20000000-0000-0000-0000-000000000051"), "HCG", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6787), null, null, null, true, false, "Trigger injection (hCG)", null, 450000m, new Guid("10000000-0000-0000-0000-000000000006"), "dose", null },
                    { new Guid("20000000-0000-0000-0000-000000000060"), "ADMIN-MR", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6788), null, null, null, true, false, "Medical record creation fee", null, 100000m, new Guid("10000000-0000-0000-0000-000000000007"), "case", null },
                    { new Guid("20000000-0000-0000-0000-000000000061"), "ADMIN-CERT", new DateTime(2025, 10, 28, 7, 39, 11, 736, DateTimeKind.Utc).AddTicks(6789), null, null, null, true, false, "Certificate/Report issuance", null, 150000m, new Guid("10000000-0000-0000-0000-000000000007"), "doc", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

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

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));
        }
    }
}
