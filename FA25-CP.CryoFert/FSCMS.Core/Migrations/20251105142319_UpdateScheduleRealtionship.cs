using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleRealtionship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_DoctorSchedules_DoctorScheduleId",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_DoctorScheduleId",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "DoctorScheduleId",
                table: "Slots");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "WorkDate",
                table: "DoctorSchedules",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<Guid>(
                name: "SlotId",
                table: "DoctorSchedules",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9178));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9186));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9187));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9194));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9196));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9197));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9225));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9227));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9258));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9281));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9286));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9289));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9043));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9045));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9046));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9047));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9048));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9315));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9317));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9318));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9319));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9320));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9321));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9322));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9381));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9387));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9389));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9392));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9395));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9424));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9426));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9427));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9428));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9430));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9431));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9433));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9435));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9436));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9437));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9438));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9439));

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "EndTime", "IsBooked", "IsDeleted", "Notes", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9350), null, new TimeSpan(0, 10, 0, 0, 0), false, false, "Morning Slot 1", new TimeSpan(0, 8, 0, 0, 0), null },
                    { new Guid("30000000-0000-0000-0000-000000000002"), new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9352), null, new TimeSpan(0, 12, 0, 0, 0), false, false, "Morning Slot 2", new TimeSpan(0, 10, 0, 0, 0), null },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9353), null, new TimeSpan(0, 15, 0, 0, 0), false, false, "Afternoon Slot 1", new TimeSpan(0, 13, 0, 0, 0), null },
                    { new Guid("30000000-0000-0000-0000-000000000004"), new DateTime(2025, 11, 5, 21, 23, 17, 243, DateTimeKind.Utc).AddTicks(9355), null, new TimeSpan(0, 17, 0, 0, 0), false, false, "Afternoon Slot 2", new TimeSpan(0, 15, 0, 0, 0), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_SlotId",
                table: "DoctorSchedules",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Slots_SlotId",
                table: "DoctorSchedules",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Slots_SlotId",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_SlotId",
                table: "DoctorSchedules");

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
                name: "SlotId",
                table: "DoctorSchedules");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorScheduleId",
                table: "Slots",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkDate",
                table: "DoctorSchedules",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1699));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1706));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1708));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1715));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1718));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1720));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1722));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000010008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000020002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1759));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1780));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1822));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000030003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1824));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1548));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1562));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1563));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1564));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1565));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1942));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1948));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1949));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1950));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1951));

            migrationBuilder.UpdateData(
                table: "ServiceCategories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1952));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(1995));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2000));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2002));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2004));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2007));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000020"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2008));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000021"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2010));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000022"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2011));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000023"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2012));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000024"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000030"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2015));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000031"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2016));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000032"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000033"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2021));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000034"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2023));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000040"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2024));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000041"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2026));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000042"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000050"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2029));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000051"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000060"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2031));

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000061"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 14, 33, 1, 843, DateTimeKind.Utc).AddTicks(2032));

            migrationBuilder.CreateIndex(
                name: "IX_Slots_DoctorScheduleId",
                table: "Slots",
                column: "DoctorScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_DoctorSchedules_DoctorScheduleId",
                table: "Slots",
                column: "DoctorScheduleId",
                principalTable: "DoctorSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
