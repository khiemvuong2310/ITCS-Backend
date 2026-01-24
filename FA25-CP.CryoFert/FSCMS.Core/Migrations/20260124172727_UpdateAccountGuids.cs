using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccountGuids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"));

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
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010012"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010013"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010014"));

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
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "AvatarId", "BirthDate", "CreatedAt", "DeletedAt", "Email", "ExpiredRefreshToken", "FirstName", "Gender", "IpAddress", "IsActive", "IsDeleted", "IsVerified", "LastLogin", "LastName", "PasswordHash", "Phone", "RefreshToken", "RoleId", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440000"), "34 Cách Mạng Tháng Tám, Quận 3, TP.HCM", null, new DateOnly(1985, 8, 20), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(584), null, "binh.tran@cryo.com", null, "Trần", false, "10.0.0.12", true, false, true, null, "Thị Bình", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000005", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.binh" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440003"), "145 Trường Chinh, Tân Bình, TP.HCM", null, new DateOnly(1988, 9, 22), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(590), null, "phuong.dang@cryo.com", null, "Đặng", false, "10.0.0.16", true, false, true, null, "Thị Phương", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000015", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.phuong" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440008"), "25 Điện Biên Phủ, Bình Thạnh, TP.HCM", null, new DateOnly(1990, 3, 10), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(594), null, "levancanh1990@gmail.com", null, "Lê", true, "10.0.1.11", true, false, true, null, "Văn Cảnh", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000006", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "canh.le" },
                    { new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"), "89 Hai Bà Trưng, Quận 1, TP.HCM", null, new DateOnly(1996, 7, 9), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(579), null, "ha.le@cryo.com", null, "Lê", false, "192.168.1.30", true, false, true, null, "Thị Thu Hà", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84901234569", null, new Guid("00000000-0000-0000-0000-000000000004"), null, "receptionist_ha" },
                    { new Guid("6ba7b811-9dad-11d1-80b4-00c04fd430c8"), "12 Phan Đăng Lưu, Phú Nhuận, TP.HCM", null, new DateOnly(1980, 5, 15), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(583), null, "an.nguyen@cryo.com", null, "Nguyễn", true, "10.0.0.11", true, false, true, null, "Văn An", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000004", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.an" },
                    { new Guid("6ba7b814-9dad-11d1-80b4-00c04fd430c8"), "56 Hai Bà Trưng, Quận 1, TP.HCM", null, new DateOnly(1978, 2, 14), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(586), null, "cuong.le@cryo.com", null, "Lê", true, "10.0.0.13", true, false, true, null, "Minh Cường", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000012", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.cuong" },
                    { new Guid("6ba7b816-9dad-11d1-80b4-00c04fd430c8"), "90 Nguyễn Huệ, Quận 1, TP.HCM", null, new DateOnly(1975, 6, 8), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(588), null, "em.vo@cryo.com", null, "Võ", true, "10.0.0.15", true, false, true, null, "Hoàng Em", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000014", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.voem" },
                    { new Guid("6ba7b819-9dad-11d1-80b4-00c04fd430c8"), "315 Trần Hưng Đạo, Quận 1, TP.HCM", null, new DateOnly(1979, 12, 18), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(592), null, "hanh.ho@cryo.com", null, "Hồ", false, "10.0.0.18", true, false, true, null, "Thị Hạnh", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000017", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.hanh" },
                    { new Guid("6ba7b81c-9dad-11d1-80b4-00c04fd430c8"), "12 Võ Văn Kiệt, Quận 1, TP.HCM", null, new DateOnly(1988, 11, 5), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(596), null, "hoangvanem88@gmail.com", null, "Hoàng", true, "10.0.1.13", true, false, true, null, "Văn Êm", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000008", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "em.hoang" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d456"), "68 Nguyễn Trãi, Quận 5, TP.HCM", null, new DateOnly(1992, 7, 25), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(595), null, "ptduyen92@gmail.com", null, "Phạm", false, "10.0.1.12", true, false, true, null, "Thị Duyên", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000007", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "duyen.pham" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"), "210 Hoàng Văn Thụ, Tân Bình, TP.HCM", null, new DateOnly(1983, 4, 5), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(591), null, "gia.bui@cryo.com", null, "Bùi", true, "10.0.0.17", true, false, true, null, "Quốc Gia", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000016", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.gia" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), "78 Lý Thường Kiệt, Quận 10, TP.HCM", null, new DateOnly(1982, 11, 30), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(587), null, "dung.pham@cryo.com", null, "Phạm", false, "10.0.0.14", true, false, true, null, "Thị Dung", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000013", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.dung" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "456 Nguyễn Văn Cừ, Quận 5, TP.HCM", null, new DateOnly(1991, 3, 12), new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(576), null, "lan.tran@cryo.com", null, "Trần", false, "192.168.1.20", true, false, true, null, "Thị Mỹ Lan", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84901234568", null, new Guid("00000000-0000-0000-0000-000000000003"), null, "lab_lan" }
                });

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1015));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1022));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1025));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1026));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1028));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1031));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1032));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(1034));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(918));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(921));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(923));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(924));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(965));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(967));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(970));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(971));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(973));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(974));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(975));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(976));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(977));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(978));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(979));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(980));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(981));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(982));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(983));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(985));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(376));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(380));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(381));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(382));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(384));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(385));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(720));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(722));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(723));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(782));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(785));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(786));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(789));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(790));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(792));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(793));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(795));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(796));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(797));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(799));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(800));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(801));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(803));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(804));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(805));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(806));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(808));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(811));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(813));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(814));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(815));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(816));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(846));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(848));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(849));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(852));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(853));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(854));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(856));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(857));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(858));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(861));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(862));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(864));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(867));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(868));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(869));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(870));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(871));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(872));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(873));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(875));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(876));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(877));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(878));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(752));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(754));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(756));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(757));

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "BadgeId", "Biography", "Certificates", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "JoinDate", "LeaveDate", "LicenseNumber", "Specialty", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440000"), "DOC002", null, "Specialist in IVF Procedures", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(628), null, true, false, new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-002", "Obstetrics and Gynecology", null, 10 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440003"), "DOC006", null, "PGT-A/PGT-M Specialist, Genetic Counseling", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(633), null, true, false, new DateTime(2017, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-006", "Reproductive Genetics", null, 8 },
                    { new Guid("6ba7b811-9dad-11d1-80b4-00c04fd430c8"), "DOC001", null, "Board Certified in Reproductive Medicine", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(625), null, true, false, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-001", "Reproductive Endocrinology", null, 15 },
                    { new Guid("6ba7b814-9dad-11d1-80b4-00c04fd430c8"), "DOC003", null, "Expert in Male Infertility and Microsurgery", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(629), null, true, false, new DateTime(2005, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-003", "Andrology", null, 20 },
                    { new Guid("6ba7b816-9dad-11d1-80b4-00c04fd430c8"), "DOC005", null, "Laparoscopic and Hysteroscopic Surgery Expert", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(632), null, true, false, new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-005", "Reproductive Surgery", null, 25 },
                    { new Guid("6ba7b819-9dad-11d1-80b4-00c04fd430c8"), "DOC008", null, "Recurrent Pregnancy Loss and Immunotherapy Specialist", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(635), null, true, false, new DateTime(2007, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-008", "Reproductive Immunology", null, 18 },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"), "DOC007", null, "Oncofertility and Cryopreservation Expert", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(634), null, true, false, new DateTime(2011, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-007", "Fertility Preservation", null, 14 },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), "DOC004", null, "Clinical Embryologist, ICSI Specialist", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(631), null, true, false, new DateTime(2013, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-004", "Embryology", null, 12 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Allergies", "BloodType", "CreatedAt", "DeletedAt", "EmergencyContact", "EmergencyPhone", "Height", "Insurance", "IsActive", "IsDeleted", "MedicalHistory", "NationalID", "Notes", "Occupation", "PatientCode", "UpdatedAt", "Weight" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440008"), null, "A+", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(660), null, "Lê Văn An", "+84900000009", null, null, true, false, null, "079090123456", null, null, "PAT001", null, null },
                    { new Guid("6ba7b81c-9dad-11d1-80b4-00c04fd430c8"), null, "O+", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(665), null, "Hoàng Văn Hùng", "+84900000011", null, null, true, false, null, "079088123458", null, null, "PAT003", null, null },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d456"), null, "B+", new DateTime(2026, 1, 24, 17, 27, 26, 641, DateTimeKind.Utc).AddTicks(664), null, "Phạm Thị Giang", "+84900000010", null, null, true, false, null, "079092123457", null, null, "PAT002", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440003"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b811-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b814-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b816-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b819-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440008"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b81c-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d456"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440003"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440008"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b811-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b814-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b816-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b819-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("6ba7b81c-9dad-11d1-80b4-00c04fd430c8"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d456"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6771));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "AvatarId", "BirthDate", "CreatedAt", "DeletedAt", "Email", "ExpiredRefreshToken", "FirstName", "Gender", "IpAddress", "IsActive", "IsDeleted", "IsVerified", "LastLogin", "LastName", "PasswordHash", "Phone", "RefreshToken", "RoleId", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010002"), "456 Nguyễn Văn Cừ, Quận 5, TP.HCM", null, new DateOnly(1991, 3, 12), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6776), null, "lan.tran@cryo.com", null, "Trần", false, "192.168.1.20", true, false, true, null, "Thị Mỹ Lan", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84901234568", null, new Guid("00000000-0000-0000-0000-000000000003"), null, "lab_lan" },
                    { new Guid("00000000-0000-0000-0000-000000010003"), "89 Hai Bà Trưng, Quận 1, TP.HCM", null, new DateOnly(1996, 7, 9), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6779), null, "ha.le@cryo.com", null, "Lê", false, "192.168.1.30", true, false, true, null, "Thị Thu Hà", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84901234569", null, new Guid("00000000-0000-0000-0000-000000000004"), null, "receptionist_ha" },
                    { new Guid("00000000-0000-0000-0000-000000010004"), "12 Phan Đăng Lưu, Phú Nhuận, TP.HCM", null, new DateOnly(1980, 5, 15), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6781), null, "an.nguyen@cryo.com", null, "Nguyễn", true, "10.0.0.11", true, false, true, null, "Văn An", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000004", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.an" },
                    { new Guid("00000000-0000-0000-0000-000000010005"), "34 Cách Mạng Tháng Tám, Quận 3, TP.HCM", null, new DateOnly(1985, 8, 20), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6783), null, "binh.tran@cryo.com", null, "Trần", false, "10.0.0.12", true, false, true, null, "Thị Bình", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000005", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.binh" },
                    { new Guid("00000000-0000-0000-0000-000000010006"), "56 Hai Bà Trưng, Quận 1, TP.HCM", null, new DateOnly(1978, 2, 14), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6784), null, "cuong.le@cryo.com", null, "Lê", true, "10.0.0.13", true, false, true, null, "Minh Cường", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000012", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.cuong" },
                    { new Guid("00000000-0000-0000-0000-000000010007"), "78 Lý Thường Kiệt, Quận 10, TP.HCM", null, new DateOnly(1982, 11, 30), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6785), null, "dung.pham@cryo.com", null, "Phạm", false, "10.0.0.14", true, false, true, null, "Thị Dung", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000013", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.dung" },
                    { new Guid("00000000-0000-0000-0000-000000010008"), "90 Nguyễn Huệ, Quận 1, TP.HCM", null, new DateOnly(1975, 6, 8), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6787), null, "em.vo@cryo.com", null, "Võ", true, "10.0.0.15", true, false, true, null, "Hoàng Em", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000014", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.voem" },
                    { new Guid("00000000-0000-0000-0000-000000010009"), "145 Trường Chinh, Tân Bình, TP.HCM", null, new DateOnly(1988, 9, 22), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6788), null, "phuong.dang@cryo.com", null, "Đặng", false, "10.0.0.16", true, false, true, null, "Thị Phương", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000015", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.phuong" },
                    { new Guid("00000000-0000-0000-0000-000000010010"), "210 Hoàng Văn Thụ, Tân Bình, TP.HCM", null, new DateOnly(1983, 4, 5), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6790), null, "gia.bui@cryo.com", null, "Bùi", true, "10.0.0.17", true, false, true, null, "Quốc Gia", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000016", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.gia" },
                    { new Guid("00000000-0000-0000-0000-000000010011"), "315 Trần Hưng Đạo, Quận 1, TP.HCM", null, new DateOnly(1979, 12, 18), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6792), null, "hanh.ho@cryo.com", null, "Hồ", false, "10.0.0.18", true, false, true, null, "Thị Hạnh", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000017", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.hanh" },
                    { new Guid("00000000-0000-0000-0000-000000010012"), "25 Điện Biên Phủ, Bình Thạnh, TP.HCM", null, new DateOnly(1990, 3, 10), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6793), null, "levancanh1990@gmail.com", null, "Lê", true, "10.0.1.11", true, false, true, null, "Văn Cảnh", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000006", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "canh.le" },
                    { new Guid("00000000-0000-0000-0000-000000010013"), "68 Nguyễn Trãi, Quận 5, TP.HCM", null, new DateOnly(1992, 7, 25), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6826), null, "ptduyen92@gmail.com", null, "Phạm", false, "10.0.1.12", true, false, true, null, "Thị Duyên", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000007", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "duyen.pham" },
                    { new Guid("00000000-0000-0000-0000-000000010014"), "12 Võ Văn Kiệt, Quận 1, TP.HCM", null, new DateOnly(1988, 11, 5), new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6828), null, "hoangvanem88@gmail.com", null, "Hoàng", true, "10.0.1.13", true, false, true, null, "Văn Êm", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000008", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "em.hoang" }
                });

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7225));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7230));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7234));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7236));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7239));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7242));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7243));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7245));

            migrationBuilder.UpdateData(
                table: "CryoPackages",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7246));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7154));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7158));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7160));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7161));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7163));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7164));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7165));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7166));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7167));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7169));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7171));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000014"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7173));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000015"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7175));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000016"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7176));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000017"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000018"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7178));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000019"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7180));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7181));

            migrationBuilder.UpdateData(
                table: "Medicines",
                keyColumn: "Id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7200));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6571));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6576));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6578));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6579));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6581));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6582));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6937));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6932));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6934));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6936));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000101"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6994));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000102"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7028));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000103"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7030));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000104"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7031));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000105"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7033));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000106"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7035));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000107"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7036));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000108"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7038));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000109"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7039));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000110"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7041));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7042));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000112"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7043));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000113"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7045));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000114"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7046));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000115"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000116"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7049));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000117"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000118"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7051));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000119"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7053));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000120"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7054));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000121"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7056));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000122"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7057));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000123"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7058));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000124"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7059));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000125"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7061));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000126"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7062));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000127"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7063));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000128"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7065));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000129"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7066));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000130"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7067));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000131"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7068));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000132"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7070));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000133"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000134"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7072));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000135"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7073));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000136"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7075));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000137"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7077));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000138"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000139"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7105));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000140"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7107));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000141"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7108));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000142"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7110));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000143"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7111));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000144"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7112));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000145"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7114));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000146"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7115));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000147"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7116));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000148"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7117));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000149"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7118));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(7120));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6964));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6966));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6968));

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6969));

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "BadgeId", "Biography", "Certificates", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "JoinDate", "LeaveDate", "LicenseNumber", "Specialty", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010004"), "DOC001", null, "Board Certified in Reproductive Medicine", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6861), null, true, false, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-001", "Reproductive Endocrinology", null, 15 },
                    { new Guid("00000000-0000-0000-0000-000000010005"), "DOC002", null, "Specialist in IVF Procedures", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6865), null, true, false, new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-002", "Obstetrics and Gynecology", null, 10 },
                    { new Guid("00000000-0000-0000-0000-000000010006"), "DOC003", null, "Expert in Male Infertility and Microsurgery", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6867), null, true, false, new DateTime(2005, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-003", "Andrology", null, 20 },
                    { new Guid("00000000-0000-0000-0000-000000010007"), "DOC004", null, "Clinical Embryologist, ICSI Specialist", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6868), null, true, false, new DateTime(2013, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-004", "Embryology", null, 12 },
                    { new Guid("00000000-0000-0000-0000-000000010008"), "DOC005", null, "Laparoscopic and Hysteroscopic Surgery Expert", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6869), null, true, false, new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-005", "Reproductive Surgery", null, 25 },
                    { new Guid("00000000-0000-0000-0000-000000010009"), "DOC006", null, "PGT-A/PGT-M Specialist, Genetic Counseling", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6871), null, true, false, new DateTime(2017, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-006", "Reproductive Genetics", null, 8 },
                    { new Guid("00000000-0000-0000-0000-000000010010"), "DOC007", null, "Oncofertility and Cryopreservation Expert", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6872), null, true, false, new DateTime(2011, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-007", "Fertility Preservation", null, 14 },
                    { new Guid("00000000-0000-0000-0000-000000010011"), "DOC008", null, "Recurrent Pregnancy Loss and Immunotherapy Specialist", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6873), null, true, false, new DateTime(2007, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-008", "Reproductive Immunology", null, 18 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Allergies", "BloodType", "CreatedAt", "DeletedAt", "EmergencyContact", "EmergencyPhone", "Height", "Insurance", "IsActive", "IsDeleted", "MedicalHistory", "NationalID", "Notes", "Occupation", "PatientCode", "UpdatedAt", "Weight" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010012"), null, "A+", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6901), null, "Lê Văn An", "+84900000009", null, null, true, false, null, "079090123456", null, null, "PAT001", null, null },
                    { new Guid("00000000-0000-0000-0000-000000010013"), null, "B+", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6905), null, "Phạm Thị Giang", "+84900000010", null, null, true, false, null, "079092123457", null, null, "PAT002", null, null },
                    { new Guid("00000000-0000-0000-0000-000000010014"), null, "O+", new DateTime(2026, 1, 24, 17, 8, 29, 472, DateTimeKind.Utc).AddTicks(6907), null, "Hoàng Văn Hùng", "+84900000011", null, null, true, false, null, "079088123458", null, null, "PAT003", null, null }
                });
        }
    }
}
