using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2073));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2079));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2080));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2088));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2089));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2136));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "AvatarId", "BirthDate", "CreatedAt", "DeletedAt", "Email", "ExpiredRefreshToken", "FirstName", "Gender", "IpAddress", "IsActive", "IsDeleted", "IsVerified", "LastLogin", "LastName", "PasswordHash", "Phone", "RefreshToken", "RoleId", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010009"), null, null, new DateTime(1978, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2091), null, "doctor3@cryo.com", null, "Le", true, null, true, false, true, null, "Minh C", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000012", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor3" },
                    { new Guid("00000000-0000-0000-0000-000000010010"), null, null, new DateTime(1982, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2092), null, "doctor4@cryo.com", null, "Pham", false, null, true, false, true, null, "Thi D", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000013", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor4" },
                    { new Guid("00000000-0000-0000-0000-000000010011"), null, null, new DateTime(1975, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2093), null, "doctor5@cryo.com", null, "Vo", true, null, true, false, true, null, "Hoang E", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000014", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor5" },
                    { new Guid("00000000-0000-0000-0000-000000010012"), null, null, new DateTime(1988, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2128), null, "doctor6@cryo.com", null, "Dang", false, null, true, false, true, null, "Thi F", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000015", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor6" },
                    { new Guid("00000000-0000-0000-0000-000000010013"), null, null, new DateTime(1983, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2130), null, "doctor7@cryo.com", null, "Bui", true, null, true, false, true, null, "Quoc G", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000016", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor7" },
                    { new Guid("00000000-0000-0000-0000-000000010014"), null, null, new DateTime(1979, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2131), null, "doctor8@cryo.com", null, "Ho", false, null, true, false, true, null, "Thi H", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000017", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor8" }
                });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2171));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2174));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2399));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2401));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2404));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2406));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2429));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2208));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2211));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2213));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(1919));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(1925));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(1927));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(1929));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(1931));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2239));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2242));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2243));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2245));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2246));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2247));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2247));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2330));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2335));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2338));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2339));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2341));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2342));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2344));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2347));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2348));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2350));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2351));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2353));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2354));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2355));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2357));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2358));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2359));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2360));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2362));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2363));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2364));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2365));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2298));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2301));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2302));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2304));

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "BadgeId", "Biography", "Certificates", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "JoinDate", "LeaveDate", "LicenseNumber", "Specialty", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010009"), "DOC003", null, "Expert in Male Infertility and Microsurgery", new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2176), null, true, false, new DateTime(2005, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-003", "Andrology", null, 20 },
                    { new Guid("00000000-0000-0000-0000-000000010010"), "DOC004", null, "Clinical Embryologist, ICSI Specialist", new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2177), null, true, false, new DateTime(2013, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-004", "Embryology", null, 12 },
                    { new Guid("00000000-0000-0000-0000-000000010011"), "DOC005", null, "Laparoscopic and Hysteroscopic Surgery Expert", new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2178), null, true, false, new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-005", "Reproductive Surgery", null, 25 },
                    { new Guid("00000000-0000-0000-0000-000000010012"), "DOC006", null, "PGT-A/PGT-M Specialist, Genetic Counseling", new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2179), null, true, false, new DateTime(2017, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-006", "Reproductive Genetics", null, 8 },
                    { new Guid("00000000-0000-0000-0000-000000010013"), "DOC007", null, "Oncofertility and Cryopreservation Expert", new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2180), null, true, false, new DateTime(2011, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-007", "Fertility Preservation", null, 14 },
                    { new Guid("00000000-0000-0000-0000-000000010014"), "DOC008", null, "Recurrent Pregnancy Loss and Immunotherapy Specialist", new DateTime(2025, 11, 27, 13, 25, 57, 942, DateTimeKind.Utc).AddTicks(2181), null, true, false, new DateTime(2007, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-008", "Reproductive Immunology", null, 18 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010009"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010010"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010011"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4743));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4748));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4750));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4757));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4762));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4764));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4766));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4767));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4833));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4836));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5307));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5311));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5314));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5316));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5318));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5320));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4888));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4890));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4892));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4451));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4457));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4460));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4462));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(4465));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5006));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5007));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5008));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5009));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5012));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5146));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5151));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5154));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5156));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5158));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5160));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5163));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5165));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5167));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5169));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5170));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5174));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5175));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5177));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5179));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5181));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5183));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5184));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5186));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5188));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5190));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5079));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5082));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5084));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 16, 22, 58, 636, DateTimeKind.Utc).AddTicks(5086));
        }
    }
}
