using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FSCMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
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
                    IsTemplate = table.Column<bool>(type: "tinyint(1)", nullable: false),
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
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
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
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    EntityType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OldValues = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NewValues = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Action = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserAgent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
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
                    RenewFromContractId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
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
                        name: "FK_CryoStorageContracts_CryoStorageContracts_RenewFromContractId",
                        column: x => x.RenewFromContractId,
                        principalTable: "CryoStorageContracts",
                        principalColumn: "Id");
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
                    CanFrozen = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CanFertilize = table.Column<bool>(type: "tinyint(1)", nullable: false),
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
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PatientId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ScheduledTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SentTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReadTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Channel = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RelatedEntityType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RelatedEntityId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsImportant = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notifications_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    ApprovalToken = table.Column<string>(type: "longtext", nullable: true)
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
                    StorageStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
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
                name: "LabSampleOocytes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MaturityStage = table.Column<string>(type: "longtext", nullable: true)
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
                    SignedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SignatureMethod = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SignatureIPAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OTPSentDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
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
                    ExpectedDurationDays = table.Column<int>(type: "int", nullable: false),
                    StepType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
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
                    CurrentStep = table.Column<int>(type: "int", nullable: false),
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
                    CurrentStep = table.Column<int>(type: "int", nullable: false),
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
                name: "LabSampleEmbryos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleOocyteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LabSampleSpermId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    FertilizationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabSampleEmbryos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabSampleEmbryos_LabSampleOocytes_LabSampleOocyteId",
                        column: x => x.LabSampleOocyteId,
                        principalTable: "LabSampleOocytes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabSampleEmbryos_LabSampleSperms_LabSampleSpermId",
                        column: x => x.LabSampleSpermId,
                        principalTable: "LabSampleSperms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabSampleEmbryos_LabSamples_LabSampleId",
                        column: x => x.LabSampleId,
                        principalTable: "LabSamples",
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
                table: "CryoPackages",
                columns: new[] { "Id", "Benefits", "CreatedAt", "DeletedAt", "Description", "DurationMonths", "IncludesInsurance", "InsuranceAmount", "IsActive", "IsDeleted", "MaxSamples", "Notes", "PackageName", "Price", "SampleType", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2574), null, "Initial fee 8,000,000 VND; storage 8,000,000 VND", 12, false, null, true, false, 10, "1-year storage package for up to 10 oocytes", "Oocyte Freezing - 1 Year", 8000000m, 1, null },
                    { new Guid("50000000-0000-0000-0000-000000000002"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2579), null, "Initial fee 8,000,000 VND; storage 20,000,000 VND", 36, false, null, true, false, 20, "Discounted compared to annual renewal", "Oocyte Freezing - 3 Years", 8000000m, 1, null },
                    { new Guid("50000000-0000-0000-0000-000000000003"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2582), null, "Initial fee 8,000,000 VND; storage 30,000,000 VND", 60, false, null, true, false, 30, "Best value for long-term storage", "Oocyte Freezing - 5 Years", 8000000m, 1, null },
                    { new Guid("50000000-0000-0000-0000-000000000004"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2585), null, "Initial fee 2,000,000 VND; storage 3,000,000 VND", 12, false, null, true, false, 5, "Storage for up to 5 sperm samples", "Sperm Freezing - 1 Year", 2000000m, 2, null },
                    { new Guid("50000000-0000-0000-0000-000000000005"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2587), null, "Initial fee 2,000,000 VND; storage 7,000,000 VND", 36, false, null, true, false, 10, "Cost-effective multi-year plan", "Sperm Freezing - 3 Years", 2000000m, 2, null },
                    { new Guid("50000000-0000-0000-0000-000000000006"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2589), null, "Initial fee 2,000,000 VND; storage 10,000,000 VND", 60, false, null, true, false, 15, "Optimal for long-term preservation", "Sperm Freezing - 5 Years", 2000000m, 2, null },
                    { new Guid("50000000-0000-0000-0000-000000000007"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2591), null, "Initial fee 10,000,000 VND; storage 10,000,000 VND", 12, false, null, true, false, 6, "Calculated for up to 6 embryos", "Embryo Freezing - 1 Year", 10000000m, 3, null },
                    { new Guid("50000000-0000-0000-0000-000000000008"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2593), null, "Initial fee 10,000,000 VND; storage 25,000,000 VND", 36, false, null, true, false, 12, "Significant savings", "Embryo Freezing - 3 Years", 10000000m, 3, null },
                    { new Guid("50000000-0000-0000-0000-000000000009"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2595), null, "Initial fee 10,000,000 VND; storage 35,000,000 VND", 60, false, null, true, false, 18, "Long-term plan, priority for IVF patients", "Embryo Freezing - 5 Years", 10000000m, 3, null }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Contraindication", "CreatedAt", "DeletedAt", "Dosage", "Form", "GenericName", "Indication", "IsActive", "IsDeleted", "Name", "Notes", "SideEffects", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2496), null, "50 mg/day (max 150 mg/day)", "Oral", null, "Ovarian stimulation D2–D6", true, false, "Clomiphene Citrate", "IUI", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000002"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2501), null, "2.5–5 mg/day", "Oral", null, "Ovarian stimulation D2–D6, PCOS", true, false, "Letrozole", "IUI", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000003"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2503), null, "75–150 IU/day", "Subcutaneous injection", null, "Ovarian stimulation from D2–D3", true, false, "Gonal-F / Puregon", "IUI/IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000004"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2505), null, "75–150 IU/day", "Subcutaneous injection", null, "Ovarian stimulation", true, false, "Menopur", "IUI/IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000005"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2507), null, "150 IU FSH + 75 IU LH/day", "Subcutaneous injection", null, "Stimulation for poor responders", true, false, "Pergoveris", "IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000006"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2508), null, "0.25 mg/day", "Subcutaneous injection", null, "Prevent premature ovulation from D5", true, false, "Cetrotide", "IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000007"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2509), null, "0.25 mg/day", "Subcutaneous injection", null, "Prevent premature ovulation from D5", true, false, "Orgalutran", "IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000008"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2511), null, "250 mcg single dose", "Subcutaneous injection", null, "Trigger when follicle is 18–20mm", true, false, "Ovidrel", "IUI/IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000009"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2512), null, "5000–10000 IU single dose", "Intramuscular injection", null, "Trigger when follicle is 18–20mm", true, false, "Pregnyl", "IUI/IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000010"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2513), null, "0.1 mg single dose", "Subcutaneous injection", null, "Trigger to reduce OHSS risk", true, false, "Decapeptyl 0.1 mg", "IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000011"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2516), null, "200 mg x 2–3 times/day", "Vaginal", null, "Post IUI/OPU/ET support", true, false, "Progesterone Suppositories", "IUI/IVF/FET", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000012"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2517), null, "10 mg x 2–3 times/day", "Oral", null, "Luteal phase support", true, false, "Duphaston", "IUI/IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000013"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2518), null, "1 applicator/day", "Vaginal", null, "Support post-ET", true, false, "Crinone 8%", "IVF/FET", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000014"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2519), null, "50 mg x 2–3 times/week", "Intramuscular injection", null, "Luteal support post OPU/ET", true, false, "Proluton", "IVF", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000015"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2521), null, "2 mg x 2–3 times/day", "Oral", null, "Endometrial thickening", true, false, "Progynova", "FET", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000016"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2522), null, "50–100 mcg/patch every 2 days", "Transdermal patch", null, "Endometrial thickening", true, false, "Estradot", "FET", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000017"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2523), null, "200–300 mg/day", "Oral", null, "Sperm quality improvement", true, false, "CoQ10", "Male", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000018"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2526), null, "400 IU/day", "Oral", null, "Antioxidant for sperm", true, false, "Vitamin E", "Male", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000019"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2528), null, "25 mg/day", "Oral", null, "Spermatogenesis support", true, false, "Clomiphene (Male)", "Male", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000020"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2530), null, "1500 IU x 2–3 times/week", "Injection", null, "Stimulate spermatogenesis", true, false, "HCG (Male)", "Male", null, null },
                    { new Guid("40000000-0000-0000-0000-000000000021"), null, new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2532), null, "150 IU x 2–3 times/week", "Injection", null, "Increase sperm production", true, false, "FSH (Male)", "Male", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "RoleCode", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1907), null, "System administrator", false, "ADMIN", "Admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1911), null, "Medical doctor", false, "DOCTOR", "Doctor", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1913), null, "Lab technician", false, "LAB_TECH", "Laboratory Technician", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1943), null, "Front desk staff", false, "RECEPTIONIST", "Receptionist", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1945), null, "Patient user", false, "PATIENT", "Patient", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(1947), null, "General user", false, "USER", "User", null }
                });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Description", "DisplayOrder", "IsActive", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000003"), "PROC", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2278), null, "IUI, OPU, ICSI, sperm prep, embryo culture", 4, true, false, "Laboratory Procedures / IVF-IUI", null },
                    { new Guid("10000000-0000-0000-0000-000000000011"), "LAB", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2274), null, "Clinical laboratory tests (female/male/post-IVF)", 1, true, false, "Laboratory Tests", null },
                    { new Guid("10000000-0000-0000-0000-000000000012"), "IMG", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2276), null, "Ultrasound, HSG, hysteroscopy, other imaging", 2, true, false, "Imaging & Diagnostic Procedures", null },
                    { new Guid("10000000-0000-0000-0000-000000000013"), "GEN", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2277), null, "Karyotype, Thalassemia, CFTR, PGT", 3, true, false, "Genetic Testing", null }
                });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "EndTime", "IsDeleted", "Notes", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2306), null, new TimeSpan(0, 10, 0, 0, 0), false, "Morning Slot 1", new TimeSpan(0, 8, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2308), null, new TimeSpan(0, 12, 0, 0, 0), false, "Morning Slot 2", new TimeSpan(0, 10, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2309), null, new TimeSpan(0, 15, 0, 0, 0), false, "Afternoon Slot 1", new TimeSpan(0, 13, 0, 0, 0), null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2311), null, new TimeSpan(0, 17, 0, 0, 0), false, "Afternoon Slot 2", new TimeSpan(0, 15, 0, 0, 0), null }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "AvatarId", "BirthDate", "CreatedAt", "DeletedAt", "Email", "ExpiredRefreshToken", "FirstName", "Gender", "IpAddress", "IsActive", "IsDeleted", "IsVerified", "LastLogin", "LastName", "PasswordHash", "Phone", "RefreshToken", "RoleId", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010001"), "123 Lê Lợi, Quận 1, TP.HCM", null, new DateOnly(1985, 1, 1), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2128), null, "quan.nguyen@cryo.com", null, "Nguyễn", true, "192.168.1.10", true, false, true, null, "Quốc Quản", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84901234567", null, new Guid("00000000-0000-0000-0000-000000000001"), null, "admin" },
                    { new Guid("00000000-0000-0000-0000-000000010002"), "456 Nguyễn Văn Cừ, Quận 5, TP.HCM", null, new DateOnly(1991, 3, 12), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2134), null, "lan.tran@cryo.com", null, "Trần", false, "192.168.1.20", true, false, true, null, "Thị Mỹ Lan", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84901234568", null, new Guid("00000000-0000-0000-0000-000000000003"), null, "lab_lan" },
                    { new Guid("00000000-0000-0000-0000-000000010003"), "89 Hai Bà Trưng, Quận 1, TP.HCM", null, new DateOnly(1996, 7, 9), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2137), null, "ha.le@cryo.com", null, "Lê", false, "192.168.1.30", true, false, true, null, "Thị Thu Hà", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84901234569", null, new Guid("00000000-0000-0000-0000-000000000004"), null, "receptionist_ha" },
                    { new Guid("00000000-0000-0000-0000-000000010004"), "12 Phan Đăng Lưu, Phú Nhuận, TP.HCM", null, new DateOnly(1980, 5, 15), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2138), null, "an.nguyen@cryo.com", null, "Nguyễn", true, "10.0.0.11", true, false, true, null, "Văn An", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000004", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.an" },
                    { new Guid("00000000-0000-0000-0000-000000010005"), "34 Cách Mạng Tháng Tám, Quận 3, TP.HCM", null, new DateOnly(1985, 8, 20), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2140), null, "binh.tran@cryo.com", null, "Trần", false, "10.0.0.12", true, false, true, null, "Thị Bình", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000005", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.binh" },
                    { new Guid("00000000-0000-0000-0000-000000010006"), "56 Hai Bà Trưng, Quận 1, TP.HCM", null, new DateOnly(1978, 2, 14), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2141), null, "cuong.le@cryo.com", null, "Lê", true, "10.0.0.13", true, false, true, null, "Minh Cường", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000012", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.cuong" },
                    { new Guid("00000000-0000-0000-0000-000000010007"), "78 Lý Thường Kiệt, Quận 10, TP.HCM", null, new DateOnly(1982, 11, 30), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2142), null, "dung.pham@cryo.com", null, "Phạm", false, "10.0.0.14", true, false, true, null, "Thị Dung", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000013", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.dung" },
                    { new Guid("00000000-0000-0000-0000-000000010008"), "90 Nguyễn Huệ, Quận 1, TP.HCM", null, new DateOnly(1975, 6, 8), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2144), null, "em.vo@cryo.com", null, "Võ", true, "10.0.0.15", true, false, true, null, "Hoàng Em", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000014", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.voem" },
                    { new Guid("00000000-0000-0000-0000-000000010009"), "145 Trường Chinh, Tân Bình, TP.HCM", null, new DateOnly(1988, 9, 22), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2145), null, "phuong.dang@cryo.com", null, "Đặng", false, "10.0.0.16", true, false, true, null, "Thị Phương", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000015", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.phuong" },
                    { new Guid("00000000-0000-0000-0000-000000010010"), "210 Hoàng Văn Thụ, Tân Bình, TP.HCM", null, new DateOnly(1983, 4, 5), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2146), null, "gia.bui@cryo.com", null, "Bùi", true, "10.0.0.17", true, false, true, null, "Quốc Gia", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000016", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.gia" },
                    { new Guid("00000000-0000-0000-0000-000000010011"), "315 Trần Hưng Đạo, Quận 1, TP.HCM", null, new DateOnly(1979, 12, 18), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2148), null, "hanh.ho@cryo.com", null, "Hồ", false, "10.0.0.18", true, false, true, null, "Thị Hạnh", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000017", null, new Guid("00000000-0000-0000-0000-000000000002"), null, "dr.hanh" },
                    { new Guid("00000000-0000-0000-0000-000000010012"), "25 Điện Biên Phủ, Bình Thạnh, TP.HCM", null, new DateOnly(1990, 3, 10), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2149), null, "levancanh1990@gmail.com", null, "Lê", true, "10.0.1.11", true, false, true, null, "Văn Cảnh", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000006", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "canh.le" },
                    { new Guid("00000000-0000-0000-0000-000000010013"), "68 Nguyễn Trãi, Quận 5, TP.HCM", null, new DateOnly(1992, 7, 25), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2150), null, "ptduyen92@gmail.com", null, "Phạm", false, "10.0.1.12", true, false, true, null, "Thị Duyên", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000007", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "duyen.pham" },
                    { new Guid("00000000-0000-0000-0000-000000010014"), "12 Võ Văn Kiệt, Quận 1, TP.HCM", null, new DateOnly(1988, 11, 5), new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2151), null, "hoangvanem88@gmail.com", null, "Hoàng", true, "10.0.1.13", true, false, true, null, "Văn Êm", "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S", "+84900000008", null, new Guid("00000000-0000-0000-0000-000000000005"), null, "em.hoang" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "Description", "Duration", "IsActive", "IsDeleted", "Name", "Notes", "Price", "ServiceCategoryId", "Unit", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000101"), "LAB-FSH-F", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2336), null, null, null, true, false, "FSH (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000102"), "LAB-LH-F", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2339), null, null, null, true, false, "LH (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000103"), "LAB-E2-F", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2341), null, null, null, true, false, "Estradiol (E2) (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000104"), "LAB-AMH-F", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2342), null, null, null, true, false, "AMH (female)", null, 775000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000105"), "LAB-TSH-F", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2344), null, null, null, true, false, "TSH (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000106"), "LAB-FT-F", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2345), null, null, null, true, false, "FT4/FT3 (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000107"), "LAB-PRL-F", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2347), null, null, null, true, false, "Prolactin (female)", null, 185000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000108"), "LAB-P4-F", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2349), null, null, null, true, false, "Progesterone (female)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000109"), "LAB-HIV", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2350), null, null, null, true, false, "HIV screening", null, 150000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000110"), "LAB-HBSAG", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2351), null, null, null, true, false, "HBsAg", null, 125000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000111"), "LAB-HCV", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2353), null, null, null, true, false, "Anti-HCV", null, 185000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000112"), "LAB-RPR", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2354), null, null, null, true, false, "RPR/VDRL (syphilis)", null, 160000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000113"), "LAB-RUB", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2355), null, null, null, true, false, "Rubella IgG/IgM", null, 400000m, new Guid("10000000-0000-0000-0000-000000000011"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000114"), "LAB-CMV", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2357), null, null, null, true, false, "CMV IgG/IgM", null, 400000m, new Guid("10000000-0000-0000-0000-000000000011"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000115"), "LAB-CHLA-PCR", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2358), null, null, null, true, false, "Chlamydia PCR", null, 575000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000116"), "LAB-GONO-PCR", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2360), null, null, null, true, false, "Gonorrhea PCR", null, 575000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000117"), "LAB-CBC", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2361), null, null, null, true, false, "Complete blood count (CBC)", null, 100000m, new Guid("10000000-0000-0000-0000-000000000011"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000118"), "LAB-GLU", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2362), null, null, null, true, false, "Blood glucose", null, 65000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000119"), "LAB-LFT", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2392), null, null, null, true, false, "AST/ALT", null, 65000m, new Guid("10000000-0000-0000-0000-000000000011"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000120"), "LAB-KFT", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2394), null, null, null, true, false, "Creatinine/Urea", null, 65000m, new Guid("10000000-0000-0000-0000-000000000011"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000121"), "LAB-ELEC", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2395), null, null, null, true, false, "Electrolyte panel", null, 160000m, new Guid("10000000-0000-0000-0000-000000000011"), "panel", null },
                    { new Guid("20000000-0000-0000-0000-000000000122"), "LAB-ABO", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2397), null, null, null, true, false, "ABO/Rh blood group", null, 115000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000123"), "LAB-SA", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2398), null, null, null, true, false, "Semen analysis (SA)", null, 350000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000124"), "LAB-SA-REP", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2399), null, null, null, true, false, "Semen analysis repeat", null, 250000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000125"), "LAB-MAR", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2400), null, null, null, true, false, "MAR test", null, 525000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000126"), "LAB-DFI", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2402), null, null, null, true, false, "DNA Fragmentation (DFI)", null, 2500000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000127"), "LAB-FSH-M", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2403), null, null, null, true, false, "FSH (male)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000128"), "LAB-LH-M", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2404), null, null, null, true, false, "LH (male)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000129"), "LAB-TESTO-M", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2406), null, null, null, true, false, "Testosterone (male)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000130"), "LAB-PRL-M", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2407), null, null, null, true, false, "Prolactin (male)", null, 185000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000131"), "LAB-TSH-M", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2408), null, null, null, true, false, "TSH (male)", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000132"), "LAB-KARYO", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2409), null, null, null, true, false, "Karyotype", null, 1350000m, new Guid("10000000-0000-0000-0000-000000000013"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000133"), "LAB-THALA", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2411), null, null, null, true, false, "Thalassemia test", null, 950000m, new Guid("10000000-0000-0000-0000-000000000013"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000134"), "LAB-CFTR", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2412), null, null, null, true, false, "CFTR (cystic fibrosis)", null, 3000000m, new Guid("10000000-0000-0000-0000-000000000013"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000135"), "LAB-PGT", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2414), null, null, null, true, false, "PGT-A/M per embryo", null, 19000000m, new Guid("10000000-0000-0000-0000-000000000013"), "embryo", null },
                    { new Guid("20000000-0000-0000-0000-000000000136"), "LAB-BHCG", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2415), null, null, null, true, false, "β-hCG", null, 150000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000137"), "LAB-P4-FU", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2416), null, null, null, true, false, "Progesterone follow-up", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000138"), "LAB-E2-FU", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2417), null, null, null, true, false, "Estradiol follow-up", null, 200000m, new Guid("10000000-0000-0000-0000-000000000011"), "test", null },
                    { new Guid("20000000-0000-0000-0000-000000000139"), "US-TVS", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2418), null, null, null, true, false, "Transvaginal ultrasound", null, 225000m, new Guid("10000000-0000-0000-0000-000000000012"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000140"), "US-ABD", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2419), null, null, null, true, false, "Abdominal ultrasound", null, 200000m, new Guid("10000000-0000-0000-0000-000000000012"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000141"), "US-FOLL", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2420), null, null, null, true, false, "Follicular ultrasound", null, 225000m, new Guid("10000000-0000-0000-0000-000000000012"), "scan", null },
                    { new Guid("20000000-0000-0000-0000-000000000142"), "IMG-HSG", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2421), null, null, null, true, false, "HSG (hysterosalpingogram)", null, 1500000m, new Guid("10000000-0000-0000-0000-000000000012"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000143"), "IMG-HSC", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2422), null, null, null, true, false, "Diagnostic hysteroscopy", null, 4500000m, new Guid("10000000-0000-0000-0000-000000000012"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000144"), "LAB-SP-COLL", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2424), null, null, null, true, false, "Sperm collection", null, 150000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000145"), "LAB-SP-WASH", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2425), null, null, null, true, false, "Sperm wash", null, 650000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000146"), "LAB-IUI", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2426), null, null, null, true, false, "IUI procedure", null, 3500000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000147"), "LAB-OPU", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2427), null, null, null, true, false, "OPU (oocyte pickup)", null, 11500000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000148"), "LAB-ICSI", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2428), null, null, null, true, false, "ICSI", null, 9000000m, new Guid("10000000-0000-0000-0000-000000000003"), "procedure", null },
                    { new Guid("20000000-0000-0000-0000-000000000149"), "LAB-EMB-D2D5", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2429), null, null, null, true, false, "Embryo culture Day2–Day5", null, 8500000m, new Guid("10000000-0000-0000-0000-000000000003"), "cycle", null }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "BadgeId", "Biography", "Certificates", "CreatedAt", "DeletedAt", "IsActive", "IsDeleted", "JoinDate", "LeaveDate", "LicenseNumber", "Specialty", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010004"), "DOC001", null, "Board Certified in Reproductive Medicine", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2183), null, true, false, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-001", "Reproductive Endocrinology", null, 15 },
                    { new Guid("00000000-0000-0000-0000-000000010005"), "DOC002", null, "Specialist in IVF Procedures", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2186), null, true, false, new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-002", "Obstetrics and Gynecology", null, 10 },
                    { new Guid("00000000-0000-0000-0000-000000010006"), "DOC003", null, "Expert in Male Infertility and Microsurgery", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2188), null, true, false, new DateTime(2005, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-003", "Andrology", null, 20 },
                    { new Guid("00000000-0000-0000-0000-000000010007"), "DOC004", null, "Clinical Embryologist, ICSI Specialist", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2189), null, true, false, new DateTime(2013, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-004", "Embryology", null, 12 },
                    { new Guid("00000000-0000-0000-0000-000000010008"), "DOC005", null, "Laparoscopic and Hysteroscopic Surgery Expert", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2191), null, true, false, new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-005", "Reproductive Surgery", null, 25 },
                    { new Guid("00000000-0000-0000-0000-000000010009"), "DOC006", null, "PGT-A/PGT-M Specialist, Genetic Counseling", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2192), null, true, false, new DateTime(2017, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-006", "Reproductive Genetics", null, 8 },
                    { new Guid("00000000-0000-0000-0000-000000010010"), "DOC007", null, "Oncofertility and Cryopreservation Expert", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2193), null, true, false, new DateTime(2011, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-007", "Fertility Preservation", null, 14 },
                    { new Guid("00000000-0000-0000-0000-000000010011"), "DOC008", null, "Recurrent Pregnancy Loss and Immunotherapy Specialist", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2194), null, true, false, new DateTime(2007, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LIC-DOC-008", "Reproductive Immunology", null, 18 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Allergies", "BloodType", "CreatedAt", "DeletedAt", "EmergencyContact", "EmergencyPhone", "Height", "Insurance", "IsActive", "IsDeleted", "MedicalHistory", "NationalID", "Notes", "Occupation", "PatientCode", "UpdatedAt", "Weight" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000010012"), null, "A+", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2246), null, "Lê Văn F", "+84900000009", null, null, true, false, null, "079090123456", null, null, "PAT001", null, null },
                    { new Guid("00000000-0000-0000-0000-000000010013"), null, "B+", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2249), null, "Phạm Thị G", "+84900000010", null, null, true, false, null, "079092123457", null, null, "PAT002", null, null },
                    { new Guid("00000000-0000-0000-0000-000000010014"), null, "O+", new DateTime(2026, 1, 21, 14, 27, 4, 916, DateTimeKind.Utc).AddTicks(2250), null, "Hoàng Văn H", "+84900000011", null, null, true, false, null, "079088123458", null, null, "PAT003", null, null }
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
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

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
                name: "IX_CryoStorageContracts_RenewFromContractId",
                table: "CryoStorageContracts",
                column: "RenewFromContractId");

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
                name: "IX_LabSampleEmbryos_LabSampleOocyteId",
                table: "LabSampleEmbryos",
                column: "LabSampleOocyteId");

            migrationBuilder.CreateIndex(
                name: "IX_LabSampleEmbryos_LabSampleSpermId",
                table: "LabSampleEmbryos",
                column: "LabSampleSpermId");

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
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PatientId",
                table: "Notifications",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

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
                name: "AuditLogs");

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
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Notifications");

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
                name: "LabSampleOocytes");

            migrationBuilder.DropTable(
                name: "LabSampleSperms");

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
                name: "LabSamples");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "CryoLocations");

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
