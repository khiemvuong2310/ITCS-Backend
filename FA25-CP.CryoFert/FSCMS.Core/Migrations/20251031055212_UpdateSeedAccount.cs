using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2859));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2866));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2868));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "AvatarId", "BirthDate", "CreatedAt", "DeletedAt", "Email", "ExpiredRefreshToken", "FirstName", "Gender", "IpAddress", "IsActive", "IsDeleted", "IsVerified", "LastLogin", "LastName", "PasswordHash", "Phone", "RefreshToken", "RoleId", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010004"), null, null, new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2921), null, "doctor1@cryo.com", null, "Nguyen", true, null, true, false, true, null, "Van A", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000004", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor1" },
                    { new Guid("00000000-0000-0000-0000-000000010005"), null, null, new DateTime(1985, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2924), null, "doctor2@cryo.com", null, "Tran", false, null, true, false, true, null, "Thi B", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000005", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor2" },
                    { new Guid("00000000-0000-0000-0000-000000010006"), null, null, new DateTime(1990, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2926), null, "patient1@cryo.com", null, "Le", true, null, true, false, true, null, "Van C", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000006", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "patient1" },
                    { new Guid("00000000-0000-0000-0000-000000010007"), null, null, new DateTime(1992, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2927), null, "patient2@cryo.com", null, "Pham", false, null, true, false, true, null, "Thi D", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000007", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "patient2" },
                    { new Guid("00000000-0000-0000-0000-000000010008"), null, null, new DateTime(1988, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2930), null, "patient3@cryo.com", null, "Hoang", true, null, true, false, true, null, "Van E", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000008", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "patient3" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2721));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2731));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2733));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2735));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2736));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3017));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3018));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3020));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3020));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3021));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3022));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3023));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3049));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3051));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3053));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3055));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3057));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3058));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3060));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3061));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3062));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3064));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3113));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3115));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3116));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3117));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3119));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3121));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3122));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3123));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3125));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3126));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3127));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(3129));

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "AccountId", "BadgeId", "Biography", "Certificates", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "JoinDate", "LeaveDate", "LicenseNumber", "Specialty", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000020001"), new Guid("00000000-0000-0000-0000-000000010004"), "DOC001", null, "Board Certified in Reproductive Medicine", new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2956), null, true, false, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-001", "Reproductive Endocrinology", null, 15 },
                    { new Guid("00000000-0000-0000-0000-000000020002"), new Guid("00000000-0000-0000-0000-000000010005"), "DOC002", null, "Specialist in IVF Procedures", new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2961), null, true, false, new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-002", "Obstetrics and Gynecology", null, 10 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "AccountId", "Allergies", "BloodType", "CreatedAt", "DeletedAt", "EmergencyContact", "EmergencyPhone", "Height", "Insurance", "IsActive", "IsDeleted", "MedicalHistory", "NationalID", "Notes", "Occupation", "PatientCode", "UpdatedAt", "Weight" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000030001"), new Guid("00000000-0000-0000-0000-000000010006"), null, "A+", new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2986), null, "Le Van F", "+84900000009", null, null, true, false, null, "001234567890", null, null, "PAT001", null, null },
                    { new Guid("00000000-0000-0000-0000-000000030002"), new Guid("00000000-0000-0000-0000-000000010007"), null, "B+", new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2990), null, "Pham Thi G", "+84900000010", null, null, true, false, null, "001234567891", null, null, "PAT002", null, null },
                    { new Guid("00000000-0000-0000-0000-000000030003"), new Guid("00000000-0000-0000-0000-000000010008"), null, "O+", new DateTime(2025, 10, 31, 12, 52, 9, 430, DateTimeKind.Utc).AddTicks(2992), null, "Hoang Van H", "+84900000011", null, null, true, false, null, "001234567892", null, null, "PAT003", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020001"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020002"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030001"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030002"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030003"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7363));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7366));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7203));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7209));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7211));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7213));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7397));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7399));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7435));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7436));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7437));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7438));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7439));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7466));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7471));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7473));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7476));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7477));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7478));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7479));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7481));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7482));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7483));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7484));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7486));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7488));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7489));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7491));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7492));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7493));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7494));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7496));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7497));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 17, 28, 44, 711, DateTimeKind.Utc).AddTicks(7498));
        }
    }
}
