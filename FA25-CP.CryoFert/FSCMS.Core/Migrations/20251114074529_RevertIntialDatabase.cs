using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class RevertIntialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CryoLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SampleType = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    SampleCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryoLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryoLocations_CryoLocations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CryoLocations",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CryoPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PackageName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DurationMonths = table.Column<int>(type: "int", nullable: false),
                    MaxSamples = table.Column<int>(type: "int", nullable: false),
                    SampleType = table.Column<int>(type: "int", nullable: false),
                    IncludesInsurance = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InsuranceAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Benefits = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryoPackages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalFileName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileExtension = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MimeType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RelatedEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RelatedEntityType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tags = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UploadDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UploadedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UploadedByUserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsPublic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ThumbnailPath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorageLocation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CloudUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GenericName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dosage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Form = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Indication = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contraindication = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SideEffects = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TransactionCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Currency = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TransactionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentGateway = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankTranNo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReferenceNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RelatedEntityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RelatedEntityType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CardNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CardType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProcessedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Gender = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiredRefreshToken = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AvatarId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServiceCategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_ServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BadgeId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Specialty = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Certificates = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LicenseNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Biography = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JoinDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LeaveDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NationalID = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmergencyContact = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmergencyPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Insurance = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Occupation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalHistory = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Allergies = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Height = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DoctorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SlotId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WorkDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Location = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CryoStorageContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ContractNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsAutoRenew = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SignedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SignedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CryoPackageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryoStorageContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryoStorageContracts_CryoPackages_CryoPackageId",
                        column: x => x.CryoPackageId,
                        principalTable: "CryoPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CryoStorageContracts_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LabSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CryoLocationId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SampleCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SampleType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StorageDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Quality = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabSamples_CryoLocations_CryoLocationId",
                        column: x => x.CryoLocationId,
                        principalTable: "CryoLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_LabSamples_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Patient1Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Patient2Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RelationshipType = table.Column<int>(type: "int", nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RespondedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RespondedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RejectionReason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relationships_Patients_Patient1Id",
                        column: x => x.Patient1Id,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relationships_Patients_Patient2Id",
                        column: x => x.Patient2Id,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DoctorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TreatmentName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TreatmentType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Diagnosis = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Goals = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstimatedCost = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ActualCost = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Treatments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CPSDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CryoStorageContractId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StorageStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StorageEndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MonthlyFee = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPSDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CPSDetails_CryoStorageContracts_CryoStorageContractId",
                        column: x => x.CryoStorageContractId,
                        principalTable: "CryoStorageContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CPSDetails_LabSamples_LabSampleId",
                        column: x => x.LabSampleId,
                        principalTable: "LabSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CryoExports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CryoLocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExportDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExportedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    WitnessedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Destination = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsThawed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ThawingDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ThawingResult = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryoExports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryoExports_CryoLocations_CryoLocationId",
                        column: x => x.CryoLocationId,
                        principalTable: "CryoLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CryoExports_LabSamples_LabSampleId",
                        column: x => x.LabSampleId,
                        principalTable: "LabSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CryoImports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CryoLocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ImportDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ImportedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    WitnessedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Temperature = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryoImports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryoImports_CryoLocations_CryoLocationId",
                        column: x => x.CryoLocationId,
                        principalTable: "CryoLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CryoImports_LabSamples_LabSampleId",
                        column: x => x.LabSampleId,
                        principalTable: "LabSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LabSampleEmbryos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DayOfDevelopment = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CellCount = table.Column<int>(type: "int", nullable: true),
                    Morphology = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsBiopsied = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPGTTested = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PGTResult = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FertilizationMethod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSampleEmbryos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabSampleEmbryos_LabSamples_LabSampleId",
                        column: x => x.LabSampleId,
                        principalTable: "LabSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LabSampleOocytes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MaturityStage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quality = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsMature = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RetrievalDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CumulusCells = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CytoplasmAppearance = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsVitrified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    VitrificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSampleOocytes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabSampleOocytes_LabSamples_LabSampleId",
                        column: x => x.LabSampleId,
                        principalTable: "LabSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LabSampleSperms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Volume = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Concentration = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Motility = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ProgressiveMotility = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Morphology = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    PH = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Viscosity = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Liquefaction = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalSpermCount = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSampleSperms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabSampleSperms_LabSamples_LabSampleId",
                        column: x => x.LabSampleId,
                        principalTable: "LabSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AgreementCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TreatmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SignedByPatient = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SignedByDoctor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FileUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agreements_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agreements_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TreatmentCycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TreatmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CycleName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CycleNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Protocol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cost = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentCycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentCycles_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TreatmentIUIs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Protocol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Medications = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Monitoring = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OvulationTriggerDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InseminationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MotileSpermCount = table.Column<int>(type: "int", nullable: true),
                    NumberOfAttempts = table.Column<int>(type: "int", nullable: true),
                    Outcome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentIUIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentIUIs_Treatments_Id",
                        column: x => x.Id,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TreatmentIVFs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Protocol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StimulationStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OocyteRetrievalDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FertilizationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TransferDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OocytesRetrieved = table.Column<int>(type: "int", nullable: true),
                    OocytesMature = table.Column<int>(type: "int", nullable: true),
                    OocytesFertilized = table.Column<int>(type: "int", nullable: true),
                    EmbryosCultured = table.Column<int>(type: "int", nullable: true),
                    EmbryosTransferred = table.Column<int>(type: "int", nullable: true),
                    EmbryosCryopreserved = table.Column<int>(type: "int", nullable: true),
                    EmbryosFrozen = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Outcome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsedICSI = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Complications = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentIVFs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentIVFs_Treatments_Id",
                        column: x => x.Id,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TreatmentCycleId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SlotId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instructions = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CheckInTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CheckOutTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsReminderSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Appointments_TreatmentCycles_TreatmentCycleId",
                        column: x => x.TreatmentCycleId,
                        principalTable: "TreatmentCycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AppointmentDoctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AppointmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DoctorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentDoctors_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentDoctors_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AppointmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChiefComplaint = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    History = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhysicalExamination = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diagnosis = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TreatmentPlan = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FollowUpInstructions = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VitalSigns = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LabResults = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagingResults = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AppointmentId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RequestDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApprovedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MedicalRecordId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PrescriptionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Diagnosis = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instructions = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsFilled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FilledDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ServiceRequestDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ServiceRequestId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ServiceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequestDetails_ServiceRequests_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRequestDetails_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrescriptionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PrescriptionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MedicineId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Dosage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Frequency = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionDetails_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrescriptionDetails_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Contraindication", "CreatedAt", "DeletedAt", "Dosage", "Form", "GenericName", "Indication", "IsActive", "IsDeleted", "Name", "Notes", "SideEffects", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), null, new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9421), null, "300 IU", "Injection", "Recombinant FSH", "Ovarian stimulation", true, false, "Follitropin alfa", "Pen device", "Headache, abdominal pain", null },
                    { new Guid("40000000-0000-0000-0000-000000000002"), null, new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9425), null, "5,000 IU", "Injection", "hCG", "Ovulation trigger", true, false, "Chorionic gonadotropin (hCG)", "Store refrigerated", "Injection site pain", null },
                    { new Guid("40000000-0000-0000-0000-000000000003"), null, new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9427), null, "200 mg", "Capsule", "Progesterone", "Luteal phase support", true, false, "Progesterone", "Taken at bedtime", "Drowsiness", null },
                    { new Guid("40000000-0000-0000-0000-000000000004"), null, new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9428), null, "2.5 mg", "Tablet", "Letrozole", "Ovulation induction", true, false, "Letrozole", null, "Fatigue, dizziness", null },
                    { new Guid("40000000-0000-0000-0000-000000000005"), "Pregnancy", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9430), null, "100 mg", "Tablet", "Doxycycline hyclate", "Infection prophylaxis", true, false, "Doxycycline", null, null, null },
                    { new Guid("40000000-0000-0000-0000-000000000006"), null, new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9432), null, "2 mg", "Tablet", "Estradiol", "Endometrial preparation", true, false, "Estradiol valerate", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "RoleCode", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8982), null, "System administrator", false, "ADMIN", "Admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8992), null, "Medical doctor", false, "DOCTOR", "Doctor", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8994), null, "Lab technician", false, "LAB_TECH", "Laboratory Technician", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8995), null, "Front desk staff", false, "RECEPTIONIST", "Receptionist", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8996), null, "Patient user", false, "PATIENT", "Patient", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(8997), null, "General user", false, "USER", "User", null }
                });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Description", "DisplayOrder", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "CONS", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9268), null, "Clinical consultations", 1, true, false, "Consultation", null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "DIAG", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9270), null, "Diagnostic tests and imaging", 2, true, false, "Diagnostics & Imaging", null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "LAB", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9271), null, "Embryology and andrology procedures", 3, true, false, "Laboratory Procedures", null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "CRYO", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9272), null, "Cryopreservation and storage services", 4, true, false, "Cryostorage & Logistics", null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "TRMT", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9273), null, "IUI/IVF related procedures", 5, true, false, "Treatment Procedures", null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "MED", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9274), null, "Medications and injections", 6, true, false, "Medications", null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "ADMIN", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9275), null, "Administrative fees", 7, true, false, "Administrative & Others", null }
                });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "EndTime", "IsDeleted", "Notes", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9300), null, new TimeSpan(0, 10, 0, 0, 0), false, "Morning Slot 1", new TimeSpan(0, 8, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9303), null, new TimeSpan(0, 12, 0, 0, 0), false, "Morning Slot 2", new TimeSpan(0, 10, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9304), null, new TimeSpan(0, 15, 0, 0, 0), false, "Afternoon Slot 1", new TimeSpan(0, 13, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9306), null, new TimeSpan(0, 17, 0, 0, 0), false, "Afternoon Slot 2", new TimeSpan(0, 15, 0, 0, 0), null }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "AvatarId", "BirthDate", "CreatedAt", "DeletedAt", "Email", "ExpiredRefreshToken", "FirstName", "Gender", "IpAddress", "IsActive", "IsDeleted", "IsVerified", "LastLogin", "LastName", "PasswordHash", "Phone", "RefreshToken", "RoleId", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010001"), null, null, null, new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9131), null, "admin@cryo.com", null, "System", null, null, true, false, true, null, "Admin", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000001", null, new Guid("00000000-0000-0000-0000-000000000001"), null, "admin" },
                    { new Guid("00000000-0000-0000-0000-000000010002"), null, null, null, new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9136), null, "lab@cryo.com", null, "Lab", null, null, true, false, true, null, "Technician", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000002", null, new Guid("00000000-0000-0000-0000-000000000003"), null, "lab" },
                    { new Guid("00000000-0000-0000-0000-000000010003"), null, null, null, new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9137), null, "receptionist@cryo.com", null, "Front", null, null, true, false, true, null, "Receptionist", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000003", null, new Guid("00000000-0000-0000-0000-000000000004"), null, "receptionist" },
                    { new Guid("00000000-0000-0000-0000-000000010004"), null, null, new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9175), null, "doctor1@cryo.com", null, "Nguyen", true, null, true, false, true, null, "Van A", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000004", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor1" },
                    { new Guid("00000000-0000-0000-0000-000000010005"), null, null, new DateTime(1985, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9177), null, "doctor2@cryo.com", null, "Tran", false, null, true, false, true, null, "Thi B", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000005", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "doctor2" },
                    { new Guid("00000000-0000-0000-0000-000000010006"), null, null, new DateTime(1990, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9179), null, "patient1@cryo.com", null, "Le", true, null, true, false, true, null, "Van C", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000006", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "patient1" },
                    { new Guid("00000000-0000-0000-0000-000000010007"), null, null, new DateTime(1992, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9181), null, "patient2@cryo.com", null, "Pham", false, null, true, false, true, null, "Thi D", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000007", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "patient2" },
                    { new Guid("00000000-0000-0000-0000-000000010008"), null, null, new DateTime(1988, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9182), null, "patient3@cryo.com", null, "Hoang", true, null, true, false, true, null, "Van E", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000008", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "patient3" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Description", "Duration", "IsActive", "IsDeleted", "Name", "Notes", "Price", "ServiceCategoryId", "Unit", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "CONS-INIT", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9331), null, "First-time visit and clinical assessment", 30, true, false, "Initial fertility consultation", null, 120m, new Guid("10000000-0000-0000-0000-000000000001"), "session", null },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "CONS-FUP", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9336), null, "Follow-up review and plan", 20, true, false, "Follow-up consultation", null, 80m, new Guid("10000000-0000-0000-0000-000000000001"), "session", null },
                    { new Guid("20000000-0000-0000-0000-000000000010"), "US-TVS", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9338), null, null, 15, true, false, "Transvaginal ultrasound", null, 60m, new Guid("10000000-0000-0000-0000-000000000002"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000011"), "LAB-HORM", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9340), null, null, null, true, false, "Baseline hormone panel (AMH/FSH/LH/E2/PRL)", null, 150m, new Guid("10000000-0000-0000-0000-000000000002"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000012"), "SA", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9367), null, null, null, true, false, "Semen analysis", null, 40m, new Guid("10000000-0000-0000-0000-000000000002"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000020"), "OPU", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9369), null, null, null, true, false, "Oocyte retrieval (OPU)", null, 1500m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000021"), "SP-PREP", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9370), null, null, null, true, false, "Sperm preparation (IUI/IVF)", null, 90m, new Guid("10000000-0000-0000-0000-000000000003"), "prep", null },
                    { new Guid("20000000-0000-0000-0000-000000000022"), "EMB-CULT", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9371), null, null, null, true, false, "Embryo culture (day 1-5)", null, 1500m, new Guid("10000000-0000-0000-0000-000000000003"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000023"), "ICSI", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9373), null, null, null, true, false, "ICSI", null, 1200m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000024"), "ET", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9374), null, null, null, true, false, "Embryo transfer (ET)", null, 800m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000030"), "VIT-OOC", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9375), null, null, null, true, false, "Oocyte vitrification", null, 600m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000031"), "CRYO-SP", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9377), null, null, null, true, false, "Sperm cryopreservation", null, 120m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000032"), "VIT-EMB", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9378), null, null, null, true, false, "Embryo vitrification", null, 700m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000033"), "STORE-ANNUAL", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9379), null, null, null, true, false, "Annual storage fee (per specimen)", null, 150m, new Guid("10000000-0000-0000-0000-000000000004"), "year", null },
                    { new Guid("20000000-0000-0000-0000-000000000034"), "THAW", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9380), null, null, null, true, false, "Specimen thawing", null, 200m, new Guid("10000000-0000-0000-0000-000000000004"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000040"), "IUI", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9382), null, null, null, true, false, "Intrauterine insemination (IUI)", null, 250m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000041"), "IVF", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9385), null, null, null, true, false, "In vitro fertilization (IVF) cycle", null, 12000m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000042"), "FET", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9386), null, null, null, true, false, "Frozen embryo transfer (FET)", null, 3500m, new Guid("10000000-0000-0000-0000-000000000005"), "cycle", null },
                    { new Guid("20000000-0000-0000-0000-000000000050"), "GONA-PEN", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9387), null, null, null, true, false, "Gonadotropin stimulation (per pen)", null, 90m, new Guid("10000000-0000-0000-0000-000000000006"), "pen", null },
                    { new Guid("20000000-0000-0000-0000-000000000051"), "HCG", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9389), null, null, null, true, false, "Trigger injection (hCG)", null, 20m, new Guid("10000000-0000-0000-0000-000000000006"), "dose", null },
                    { new Guid("20000000-0000-0000-0000-000000000060"), "ADMIN-MR", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9390), null, null, null, true, false, "Medical record creation fee", null, 10m, new Guid("10000000-0000-0000-0000-000000000007"), "case", null },
                    { new Guid("20000000-0000-0000-0000-000000000061"), "ADMIN-CERT", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9391), null, null, null, true, false, "Certificate/Report issuance", null, 15m, new Guid("10000000-0000-0000-0000-000000000007"), "doc", null }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "BadgeId", "Biography", "Certificates", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "JoinDate", "LeaveDate", "LicenseNumber", "Specialty", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010004"), "DOC001", null, "Board Certified in Reproductive Medicine", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9213), null, true, false, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-001", "Reproductive Endocrinology", null, 15 },
                    { new Guid("00000000-0000-0000-0000-000000010005"), "DOC002", null, "Specialist in IVF Procedures", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9218), null, true, false, new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-002", "Obstetrics and Gynecology", null, 10 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Allergies", "BloodType", "CreatedAt", "DeletedAt", "EmergencyContact", "EmergencyPhone", "Height", "Insurance", "IsActive", "IsDeleted", "MedicalHistory", "NationalID", "Notes", "Occupation", "PatientCode", "UpdatedAt", "Weight" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010006"), null, "A+", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9239), null, "Le Van F", "+84900000009", null, null, true, false, null, "001234567890", null, null, "PAT001", null, null },
                    { new Guid("00000000-0000-0000-0000-000000010007"), null, "B+", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9242), null, "Pham Thi G", "+84900000010", null, null, true, false, null, "001234567891", null, null, "PAT002", null, null },
                    { new Guid("00000000-0000-0000-0000-000000010008"), null, "O+", new DateTime(2025, 11, 14, 14, 45, 26, 376, DateTimeKind.Utc).AddTicks(9243), null, "Hoang Van H", "+84900000011", null, null, true, false, null, "001234567892", null, null, "PAT003", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_PatientId",
                table: "Agreements",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_TreatmentId",
                table: "Agreements",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDoctors_AppointmentId_DoctorId",
                table: "AppointmentDoctors",
                columns: new[] { "AppointmentId", "DoctorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDoctors_DoctorId",
                table: "AppointmentDoctors",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SlotId",
                table: "Appointments",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TreatmentCycleId",
                table: "Appointments",
                column: "TreatmentCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_CPSDetails_CryoStorageContractId",
                table: "CPSDetails",
                column: "CryoStorageContractId");

            migrationBuilder.CreateIndex(
                name: "IX_CPSDetails_LabSampleId",
                table: "CPSDetails",
                column: "LabSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_CryoExports_CryoLocationId",
                table: "CryoExports",
                column: "CryoLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CryoExports_LabSampleId",
                table: "CryoExports",
                column: "LabSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_CryoImports_CryoLocationId",
                table: "CryoImports",
                column: "CryoLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CryoImports_LabSampleId",
                table: "CryoImports",
                column: "LabSampleId");

            migrationBuilder.CreateIndex(
                name: "IX_CryoLocations_ParentId",
                table: "CryoLocations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CryoStorageContracts_CryoPackageId",
                table: "CryoStorageContracts",
                column: "CryoPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_CryoStorageContracts_PatientId",
                table: "CryoStorageContracts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId",
                table: "DoctorSchedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_SlotId",
                table: "DoctorSchedules",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_LabSampleEmbryos_LabSampleId",
                table: "LabSampleEmbryos",
                column: "LabSampleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabSampleOocytes_LabSampleId",
                table: "LabSampleOocytes",
                column: "LabSampleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabSamples_CryoLocationId",
                table: "LabSamples",
                column: "CryoLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LabSamples_PatientId",
                table: "LabSamples",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabSampleSperms_LabSampleId",
                table: "LabSampleSperms",
                column: "LabSampleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_AppointmentId",
                table: "MedicalRecords",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetails_MedicineId",
                table: "PrescriptionDetails",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionDetails_PrescriptionId",
                table: "PrescriptionDetails",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicalRecordId",
                table: "Prescriptions",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_Patient1Id",
                table: "Relationships",
                column: "Patient1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_Patient2Id",
                table: "Relationships",
                column: "Patient2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestDetails_ServiceId",
                table: "ServiceRequestDetails",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequestDetails_ServiceRequestId",
                table: "ServiceRequestDetails",
                column: "ServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_AppointmentId",
                table: "ServiceRequests",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCategoryId",
                table: "Services",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentCycles_TreatmentId",
                table: "TreatmentCycles",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_DoctorId",
                table: "Treatments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PatientId",
                table: "Treatments",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "AppointmentDoctors");

            migrationBuilder.DropTable(
                name: "CPSDetails");

            migrationBuilder.DropTable(
                name: "CryoExports");

            migrationBuilder.DropTable(
                name: "CryoImports");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "LabSampleEmbryos");

            migrationBuilder.DropTable(
                name: "LabSampleOocytes");

            migrationBuilder.DropTable(
                name: "LabSampleSperms");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "PrescriptionDetails");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "ServiceRequestDetails");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TreatmentIUIs");

            migrationBuilder.DropTable(
                name: "TreatmentIVFs");

            migrationBuilder.DropTable(
                name: "CryoStorageContracts");

            migrationBuilder.DropTable(
                name: "LabSamples");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "CryoPackages");

            migrationBuilder.DropTable(
                name: "CryoLocations");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "TreatmentCycles");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
