using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationShipSharePk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Accounts_AccountId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Accounts_AccountId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentIUIs_Treatments_TreatmentId",
                table: "TreatmentIUIs");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentIVFs_Treatments_TreatmentId",
                table: "TreatmentIVFs");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentIVFs_TreatmentId",
                table: "TreatmentIVFs");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentIUIs_TreatmentId",
                table: "TreatmentIUIs");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AccountId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors");

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
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"));

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "TreatmentIVFs");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "TreatmentIUIs");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Doctors");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7299));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7307));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7313));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "BadgeId", "Biography", "Certificates", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "JoinDate", "LeaveDate", "LicenseNumber", "Specialty", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010004"), "DOC001", null, "Board Certified in Reproductive Medicine", new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7346), null, true, false, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-001", "Reproductive Endocrinology", null, 15 },
                    { new Guid("00000000-0000-0000-0000-000000010005"), "DOC002", null, "Specialist in IVF Procedures", new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7350), null, true, false, new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-002", "Obstetrics and Gynecology", null, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7575));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7580));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7585));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7587));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7612));

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Allergies", "BloodType", "CreatedAt", "DeletedAt", "EmergencyContact", "EmergencyPhone", "Height", "Insurance", "IsActive", "IsDeleted", "MedicalHistory", "NationalID", "Notes", "Occupation", "PatientCode", "UpdatedAt", "Weight" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010006"), null, "A+", new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7373), null, "Le Van F", "+84900000009", null, null, true, false, null, "001234567890", null, null, "PAT001", null, null },
                    { new Guid("00000000-0000-0000-0000-000000010007"), null, "B+", new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7377), null, "Pham Thi G", "+84900000010", null, null, true, false, null, "001234567891", null, null, "PAT002", null, null },
                    { new Guid("00000000-0000-0000-0000-000000010008"), null, "O+", new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7379), null, "Hoang Van H", "+84900000011", null, null, true, false, null, "001234567892", null, null, "PAT003", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7145));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7153));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7154));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7156));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7157));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7159));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7412));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7414));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7415));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7416));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7417));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7418));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7502));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7507));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7512));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7516));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7518));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7522));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7524));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7525));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7527));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7528));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7531));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7533));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7534));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7535));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7537));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7538));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7540));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7541));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7542));

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "EndTime", "IsDeleted", "Notes", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7469), null, new TimeSpan(0, 10, 0, 0, 0), false, "Morning Slot 1", new TimeSpan(0, 8, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7472), null, new TimeSpan(0, 12, 0, 0, 0), false, "Morning Slot 2", new TimeSpan(0, 10, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7473), null, new TimeSpan(0, 15, 0, 0, 0), false, "Afternoon Slot 1", new TimeSpan(0, 13, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2025, 11, 12, 19, 39, 55, 947, DateTimeKind.Utc).AddTicks(7475), null, new TimeSpan(0, 17, 0, 0, 0), false, "Afternoon Slot 2", new TimeSpan(0, 15, 0, 0, 0), null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Accounts_Id",
                table: "Doctors",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Accounts_Id",
                table: "Patients",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentIUIs_Treatments_Id",
                table: "TreatmentIUIs",
                column: "Id",
                principalTable: "Treatments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentIVFs_Treatments_Id",
                table: "TreatmentIVFs",
                column: "Id",
                principalTable: "Treatments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Accounts_Id",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Accounts_Id",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentIUIs_Treatments_Id",
                table: "TreatmentIUIs");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentIVFs_Treatments_Id",
                table: "TreatmentIVFs");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.AddColumn<Guid>(
                name: "TreatmentId",
                table: "TreatmentIVFs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "TreatmentId",
                table: "TreatmentIUIs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Patients",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Doctors",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2632));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2637));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2638));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2645));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2647));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2649));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2650));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2654));

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "AccountId", "BadgeId", "Biography", "Certificates", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "JoinDate", "LeaveDate", "LicenseNumber", "Specialty", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000020001"), new Guid("00000000-0000-0000-0000-000000010004"), "DOC001", null, "Board Certified in Reproductive Medicine", new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2698), null, true, false, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-001", "Reproductive Endocrinology", null, 15 },
                    { new Guid("00000000-0000-0000-0000-000000020002"), new Guid("00000000-0000-0000-0000-000000010005"), "DOC002", null, "Specialist in IVF Procedures", new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2702), null, true, false, new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-002", "Obstetrics and Gynecology", null, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2911));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2916));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2918));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2919));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2921));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2922));

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "AccountId", "Allergies", "BloodType", "CreatedAt", "DeletedAt", "EmergencyContact", "EmergencyPhone", "Height", "Insurance", "IsActive", "IsDeleted", "MedicalHistory", "NationalID", "Notes", "Occupation", "PatientCode", "UpdatedAt", "Weight" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000030001"), new Guid("00000000-0000-0000-0000-000000010006"), null, "A+", new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2723), null, "Le Van F", "+84900000009", null, null, true, false, null, "001234567890", null, null, "PAT001", null, null },
                    { new Guid("00000000-0000-0000-0000-000000030002"), new Guid("00000000-0000-0000-0000-000000010007"), null, "B+", new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2727), null, "Pham Thi G", "+84900000010", null, null, true, false, null, "001234567891", null, null, "PAT002", null, null },
                    { new Guid("00000000-0000-0000-0000-000000030003"), new Guid("00000000-0000-0000-0000-000000010008"), null, "O+", new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2729), null, "Hoang Van H", "+84900000011", null, null, true, false, null, "001234567892", null, null, "PAT003", null, null }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2512));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2519));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2521));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2524));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2525));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2751));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2753));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2754));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2755));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2756));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2757));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2758));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2811));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2816));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2817));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2824));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2862));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2864));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2865));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2867));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2868));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2869));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2871));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2872));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2874));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2876));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2877));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2879));

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "EndTime", "IsDeleted", "Notes", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2783), null, new TimeSpan(0, 10, 0, 0, 0), false, "Morning Slot 1", new TimeSpan(0, 8, 0, 0, 0), null },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2785), null, new TimeSpan(0, 12, 0, 0, 0), false, "Morning Slot 2", new TimeSpan(0, 10, 0, 0, 0), null },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2786), null, new TimeSpan(0, 15, 0, 0, 0), false, "Afternoon Slot 1", new TimeSpan(0, 13, 0, 0, 0), null },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2025, 11, 10, 23, 15, 47, 230, DateTimeKind.Utc).AddTicks(2787), null, new TimeSpan(0, 17, 0, 0, 0), false, "Afternoon Slot 2", new TimeSpan(0, 15, 0, 0, 0), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentIVFs_TreatmentId",
                table: "TreatmentIVFs",
                column: "TreatmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentIUIs_TreatmentId",
                table: "TreatmentIUIs",
                column: "TreatmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AccountId",
                table: "Patients",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AccountId",
                table: "Doctors",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Accounts_AccountId",
                table: "Doctors",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Accounts_AccountId",
                table: "Patients",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentIUIs_Treatments_TreatmentId",
                table: "TreatmentIUIs",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentIVFs_Treatments_TreatmentId",
                table: "TreatmentIVFs",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
