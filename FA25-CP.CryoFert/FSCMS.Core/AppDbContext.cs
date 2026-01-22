using Microsoft.EntityFrameworkCore;
using FSCMS.Core.Entities;
using FSCMS.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ========== Nhóm 1: Core Entities - Quản lý Người dùng & Bệnh nhân ==========
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

        // ========== Nhóm 2: Lịch hẹn & Dịch vụ ==========
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceRequestDetails> ServiceRequestDetails { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        // ========== Nhóm 3: Điều trị & Bệnh án ==========
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<TreatmentIVF> TreatmentIVFs { get; set; }
        public DbSet<TreatmentIUI> TreatmentIUIs { get; set; }
        public DbSet<TreatmentCycle> TreatmentCycles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDoctor> AppointmentDoctors { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        // ========== Nhóm 4: Lab & Kho lưu trữ ==========
        public DbSet<LabSample> LabSamples { get; set; }
        public DbSet<LabSampleEmbryo> LabSampleEmbryos { get; set; }
        public DbSet<LabSampleSperm> LabSampleSperms { get; set; }
        public DbSet<LabSampleOocyte> LabSampleOocytes { get; set; }
        public DbSet<CryoLocation> CryoLocations { get; set; }
        public DbSet<CryoImport> CryoImports { get; set; }
        public DbSet<CryoExport> CryoExports { get; set; }

        // ========== Nhóm 5: Hợp đồng & Gói dịch vụ ==========
        public DbSet<CryoPackage> CryoPackages { get; set; }
        public DbSet<CryoStorageContract> CryoStorageContracts { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<CPSDetail> CPSDetails { get; set; }

        // ========== Nhóm 6: Bảng Phụ trợ ==========
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<HospitalData> HospitalData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================================
            // Nhóm 1: Account, Patient, Doctor, Relationship
            // ========================================

            // Account & Role Relationship
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Account & Patient Relationship (One-to-One)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Patient)
                .WithOne(p => p.Account)
                .HasForeignKey<Patient>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Account & Doctor Relationship (One-to-One)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Doctor)
                .WithOne(d => d.Account)
                .HasForeignKey<Doctor>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship (Patient to Patient Many-to-Many)
            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.Patient1)
                .WithMany(p => p.RelationshipsAsPatient1)
                .HasForeignKey(r => r.Patient1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.Patient2)
                .WithMany(p => p.RelationshipsAsPatient2)
                .HasForeignKey(r => r.Patient2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================
            // Nhóm 2: DoctorSchedule, Slot, Service, ServiceCategory
            // ========================================

            // DoctorSchedule & Doctor Relationship
            modelBuilder.Entity<DoctorSchedule>()
                .HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorSchedules)
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Slot & DoctorSchedule Relationship (Slot 1 - n DoctorSchedules)
            modelBuilder.Entity<DoctorSchedule>()
                .HasOne(ds => ds.Slot)
                .WithMany(s => s.DoctorSchedules)
                .HasForeignKey(ds => ds.SlotId)
                .OnDelete(DeleteBehavior.Restrict);

            // Slot & Appointment Relationship (One-to-Many)
            modelBuilder.Entity<Slot>()
                .HasMany(s => s.Appointments)
                .WithOne(a => a.Slot)
                .HasForeignKey(a => a.SlotId)
                .OnDelete(DeleteBehavior.SetNull);

            // Service & ServiceCategory Relationship
            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceCategory)
                .WithMany(sc => sc.Services)
                .HasForeignKey(s => s.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // ServiceRequest & Appointment Relationship
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Appointment)
                .WithMany()
                .HasForeignKey(sr => sr.AppointmentId)
                .OnDelete(DeleteBehavior.SetNull);

            // ServiceRequestDetails Relationships
            modelBuilder.Entity<ServiceRequestDetails>()
                .HasOne(srd => srd.ServiceRequest)
                .WithMany(sr => sr.ServiceRequestDetails)
                .HasForeignKey(srd => srd.ServiceRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceRequestDetails>()
                .HasOne(srd => srd.Service)
                .WithMany(s => s.ServiceRequestDetails)
                .HasForeignKey(srd => srd.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================
            // Nhóm 3: Treatment, TreatmentCycle, Appointment, MedicalRecord, Prescription
            // ========================================

            // Treatment & Patient Relationship
            modelBuilder.Entity<Treatment>()
                .HasOne(t => t.Patient)
                .WithMany(p => p.Treatments)
                .HasForeignKey(t => t.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Appointment & Patient Relationship (Many-to-One)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Treatment & Doctor Relationship
            modelBuilder.Entity<Treatment>()
                .HasOne(t => t.Doctor)
                .WithMany(d => d.Treatments)
                .HasForeignKey(t => t.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // TreatmentIVF & Treatment Relationship (One-to-One)
            modelBuilder.Entity<TreatmentIVF>()
                .HasOne(tivf => tivf.Treatment)
                .WithOne(t => t.TreatmentIVF)
                .HasForeignKey<TreatmentIVF>(tivf => tivf.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // TreatmentIUI & Treatment Relationship (One-to-One)
            modelBuilder.Entity<TreatmentIUI>()
                .HasOne(tiui => tiui.Treatment)
                .WithOne(t => t.TreatmentIUI)
                .HasForeignKey<TreatmentIUI>(tiui => tiui.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // TreatmentCycle & Treatment Relationship
            modelBuilder.Entity<TreatmentCycle>()
                .HasOne(tc => tc.Treatment)
                .WithMany(t => t.TreatmentCycles)
                .HasForeignKey(tc => tc.TreatmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Appointment & TreatmentCycle Relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.TreatmentCycle)
                .WithMany(tc => tc.Appointments)
                .HasForeignKey(a => a.TreatmentCycleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Appointment & Doctor Relationship (Many-to-Many through AppointmentDoctor)
            modelBuilder.Entity<AppointmentDoctor>()
                .HasOne(ad => ad.Appointment)
                .WithMany(a => a.AppointmentDoctors)
                .HasForeignKey(ad => ad.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppointmentDoctor>()
                .HasOne(ad => ad.Doctor)
                .WithMany(d => d.AppointmentDoctors)
                .HasForeignKey(ad => ad.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint: một Appointment không thể có cùng một Doctor nhiều lần
            modelBuilder.Entity<AppointmentDoctor>()
                .HasIndex(ad => new { ad.AppointmentId, ad.DoctorId })
                .IsUnique();

            // MedicalRecord & Appointment Relationship (Many-to-One)
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Appointment)
                .WithMany(a => a.MedicalRecords)
                .HasForeignKey(mr => mr.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prescription & MedicalRecord Relationship
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.MedicalRecord)
                .WithMany(mr => mr.Prescriptions)
                .HasForeignKey(p => p.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            // PrescriptionDetail Relationships
            modelBuilder.Entity<PrescriptionDetail>()
                .HasOne(pd => pd.Prescription)
                .WithMany(p => p.PrescriptionDetails)
                .HasForeignKey(pd => pd.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrescriptionDetail>()
                .HasOne(pd => pd.Medicine)
                .WithMany(m => m.PrescriptionDetails)
                .HasForeignKey(pd => pd.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================
            // Nhóm 4: LabSample, CryoLocation, CryoImport, CryoExport
            // ========================================

            // LabSample & Patient Relationship
            modelBuilder.Entity<LabSample>()
                .HasOne(ls => ls.Patient)
                .WithMany(p => p.LabSamples)
                .HasForeignKey(ls => ls.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // LabSample & CryoLocation Relationship
            modelBuilder.Entity<LabSample>()
                .HasOne(ls => ls.CryoLocation)
                .WithMany(cl => cl.LabSamples)
                .HasForeignKey(ls => ls.CryoLocationId)
                .OnDelete(DeleteBehavior.SetNull);

            // LabSample inheritance relationships (One-to-One)
            modelBuilder.Entity<LabSampleEmbryo>()
                .HasOne(lse => lse.LabSample)
                .WithOne(ls => ls.LabSampleEmbryo)
                .HasForeignKey<LabSampleEmbryo>(lse => lse.LabSampleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LabSampleSperm>()
                .HasOne(lss => lss.LabSample)
                .WithOne(ls => ls.LabSampleSperm)
                .HasForeignKey<LabSampleSperm>(lss => lss.LabSampleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LabSampleOocyte>()
                .HasOne(lso => lso.LabSample)
                .WithOne(ls => ls.LabSampleOocyte)
                .HasForeignKey<LabSampleOocyte>(lso => lso.LabSampleId)
                .OnDelete(DeleteBehavior.Cascade);

            // CryoImport Relationships
            modelBuilder.Entity<CryoImport>()
                .HasOne(ci => ci.LabSample)
                .WithMany()
                .HasForeignKey(ci => ci.LabSampleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CryoImport>()
                .HasOne(ci => ci.CryoLocation)
                .WithMany(cl => cl.CryoImports)
                .HasForeignKey(ci => ci.CryoLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // CryoExport Relationships
            modelBuilder.Entity<CryoExport>()
                .HasOne(ce => ce.LabSample)
                .WithMany()
                .HasForeignKey(ce => ce.LabSampleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CryoExport>()
                .HasOne(ce => ce.CryoLocation)
                .WithMany(cl => cl.CryoExports)
                .HasForeignKey(ce => ce.CryoLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================
            // Nhóm 5: CryoPackage, CryoStorageContract, Agreement, CPSDetail
            // ========================================

            // CryoStorageContract & Patient Relationship
            modelBuilder.Entity<CryoStorageContract>()
                .HasOne(csc => csc.Patient)
                .WithMany(p => p.CryoStorageContracts)
                .HasForeignKey(csc => csc.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // CryoStorageContract & CryoPackage Relationship
            modelBuilder.Entity<CryoStorageContract>()
                .HasOne(csc => csc.CryoPackage)
                .WithMany(cp => cp.CryoStorageContracts)
                .HasForeignKey(csc => csc.CryoPackageId)
                .OnDelete(DeleteBehavior.Restrict);

            // CPSDetail Relationships (Many-to-Many bridge)
            modelBuilder.Entity<CPSDetail>()
                .HasOne(cpsd => cpsd.CryoStorageContract)
                .WithMany(csc => csc.CPSDetails)
                .HasForeignKey(cpsd => cpsd.CryoStorageContractId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CPSDetail>()
                .HasOne(cpsd => cpsd.LabSample)
                .WithMany(ls => ls.CPSDetails)
                .HasForeignKey(cpsd => cpsd.LabSampleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Agreement Relationships
            modelBuilder.Entity<Agreement>()
                .HasOne(a => a.Treatment)
                .WithMany()
                .HasForeignKey(a => a.TreatmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agreement>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========================================
            // Nhóm 6: Transaction & Media & AuditLog
            // ========================================
            // These are independent tables with logical relationships only
            // No foreign key constraints needed

            // AuditLog & Account Relationship
            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.User)
                .WithMany()
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Notification Relationships
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Patient)
                .WithMany()
                .HasForeignKey(n => n.PatientId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ========================================
            // Seed Data: Roles
            // ========================================
            modelBuilder.Entity<Role>().HasData(
                new Role(new Guid("00000000-0000-0000-0000-000000000001"), "Admin", "ADMIN") { Description = "System administrator" },
                new Role(new Guid("00000000-0000-0000-0000-000000000002"), "Doctor", "DOCTOR") { Description = "Medical doctor" },
                new Role(new Guid("00000000-0000-0000-0000-000000000003"), "Laboratory Technician", "LAB_TECH") { Description = "Lab technician" },
                new Role(new Guid("00000000-0000-0000-0000-000000000004"), "Receptionist", "RECEPTIONIST") { Description = "Front desk staff" },
                new Role(new Guid("00000000-0000-0000-0000-000000000005"), "Patient", "PATIENT") { Description = "Patient user" },
                new Role(new Guid("00000000-0000-0000-0000-000000000006"), "User", "USER") { Description = "General user" }
            );


            // Seed Accounts: Admin, LaboratoryTechnician, Receptionist, Doctor, Patient
            var roleAdminId = new Guid("00000000-0000-0000-0000-000000000001");
            var roleLabId = new Guid("00000000-0000-0000-0000-000000000003");
            var roleReceptionistId = new Guid("00000000-0000-0000-0000-000000000004");
            var roleDoctorId = new Guid("00000000-0000-0000-0000-000000000002");
            var rolePatientId = new Guid("00000000-0000-0000-0000-000000000005");

            // Password for all seed accounts: "12345678"
            // Generated using: BCrypt.Net.BCrypt.HashPassword("12345678")
            const string defaultPwdHash = "$2a$11$.JgDmowGQmD2u2cMhrPnZO4VExs1s7hQIPdTJKcPfPRxKnoFRUO6S";

            // Account IDs
            var adminAccountId = new Guid("00000000-0000-0000-0000-000000010001");
            var labAccountId = new Guid("00000000-0000-0000-0000-000000010002");
            var receptionistAccountId = new Guid("00000000-0000-0000-0000-000000010003");
            var doctor1AccountId = new Guid("00000000-0000-0000-0000-000000010004");
            var doctor2AccountId = new Guid("00000000-0000-0000-0000-000000010005");
            var doctor3AccountId = new Guid("00000000-0000-0000-0000-000000010006");
            var doctor4AccountId = new Guid("00000000-0000-0000-0000-000000010007");
            var doctor5AccountId = new Guid("00000000-0000-0000-0000-000000010008");
            var doctor6AccountId = new Guid("00000000-0000-0000-0000-000000010009");
            var doctor7AccountId = new Guid("00000000-0000-0000-0000-000000010010");
            var doctor8AccountId = new Guid("00000000-0000-0000-0000-000000010011");
            var patient1AccountId = new Guid("00000000-0000-0000-0000-000000010012");
            var patient2AccountId = new Guid("00000000-0000-0000-0000-000000010013");
            var patient3AccountId = new Guid("00000000-0000-0000-0000-000000010014");

            modelBuilder.Entity<Account>().HasData(
                // 1. Quản trị hệ thống (Admin)
                new Account(adminAccountId, "Nguyễn", "Quốc Quản", new DateOnly(1985, 1, 1), "quan.nguyen@cryo.com", "admin", defaultPwdHash, "+84901234567", "192.168.1.10", true, true, true, "123 Lê Lợi, Quận 1, TP.HCM", null) { RoleId = roleAdminId },

                // 2. Kỹ thuật viên phòng Lab (Lab Technician)
                new Account(labAccountId, "Trần", "Thị Mỹ Lan", new DateOnly(1991, 3, 12), "lan.tran@cryo.com", "lab_lan", defaultPwdHash, "+84901234568", "192.168.1.20", false, true, true, "456 Nguyễn Văn Cừ, Quận 5, TP.HCM", null) { RoleId = roleLabId },

                // 3. Lễ tân (Receptionist)
                new Account(receptionistAccountId, "Lê", "Thị Thu Hà", new DateOnly(1996, 7, 9), "ha.le@cryo.com", "receptionist_ha", defaultPwdHash, "+84901234569", "192.168.1.30", false, true, true, "89 Hai Bà Trưng, Quận 1, TP.HCM", null) { RoleId = roleReceptionistId },

                // 4. Bác sĩ (Doctors) - Đổi email/username theo tên thật
                new Account(doctor1AccountId, "Nguyễn", "Văn An", new DateOnly(1980, 5, 15), "an.nguyen@cryo.com", "dr.an", defaultPwdHash, "+84900000004", "10.0.0.11", true, true, true, "12 Phan Đăng Lưu, Phú Nhuận, TP.HCM", null) { RoleId = roleDoctorId },
                new Account(doctor2AccountId, "Trần", "Thị Bình", new DateOnly(1985, 8, 20), "binh.tran@cryo.com", "dr.binh", defaultPwdHash, "+84900000005", "10.0.0.12", false, true, true, "34 Cách Mạng Tháng Tám, Quận 3, TP.HCM", null) { RoleId = roleDoctorId },
                new Account(doctor3AccountId, "Lê", "Minh Cường", new DateOnly(1978, 2, 14), "cuong.le@cryo.com", "dr.cuong", defaultPwdHash, "+84900000012", "10.0.0.13", true, true, true, "56 Hai Bà Trưng, Quận 1, TP.HCM", null) { RoleId = roleDoctorId },
                new Account(doctor4AccountId, "Phạm", "Thị Dung", new DateOnly(1982, 11, 30), "dung.pham@cryo.com", "dr.dung", defaultPwdHash, "+84900000013", "10.0.0.14", false, true, true, "78 Lý Thường Kiệt, Quận 10, TP.HCM", null) { RoleId = roleDoctorId },
                new Account(doctor5AccountId, "Võ", "Hoàng Em", new DateOnly(1975, 6, 8), "em.vo@cryo.com", "dr.voem", defaultPwdHash, "+84900000014", "10.0.0.15", true, true, true, "90 Nguyễn Huệ, Quận 1, TP.HCM", null) { RoleId = roleDoctorId },
                new Account(doctor6AccountId, "Đặng", "Thị Phương", new DateOnly(1988, 9, 22), "phuong.dang@cryo.com", "dr.phuong", defaultPwdHash, "+84900000015", "10.0.0.16", false, true, true, "145 Trường Chinh, Tân Bình, TP.HCM", null) { RoleId = roleDoctorId },
                new Account(doctor7AccountId, "Bùi", "Quốc Gia", new DateOnly(1983, 4, 5), "gia.bui@cryo.com", "dr.gia", defaultPwdHash, "+84900000016", "10.0.0.17", true, true, true, "210 Hoàng Văn Thụ, Tân Bình, TP.HCM", null) { RoleId = roleDoctorId },
                new Account(doctor8AccountId, "Hồ", "Thị Hạnh", new DateOnly(1979, 12, 18), "hanh.ho@cryo.com", "dr.hanh", defaultPwdHash, "+84900000017", "10.0.0.18", false, true, true, "315 Trần Hưng Đạo, Quận 1, TP.HCM", null) { RoleId = roleDoctorId },

                // 5. Bệnh nhân (Patients) - Đổi email sang Gmail để thực tế hơn
                new Account(patient1AccountId, "Lê", "Văn Cảnh", new DateOnly(1990, 3, 10), "levancanh1990@gmail.com", "canh.le", defaultPwdHash, "+84900000006", "10.0.1.11", true, true, true, "25 Điện Biên Phủ, Bình Thạnh, TP.HCM", null) { RoleId = rolePatientId },
                new Account(patient2AccountId, "Phạm", "Thị Duyên", new DateOnly(1992, 7, 25), "ptduyen92@gmail.com", "duyen.pham", defaultPwdHash, "+84900000007", "10.0.1.12", false, true, true, "68 Nguyễn Trãi, Quận 5, TP.HCM", null) { RoleId = rolePatientId },
                new Account(patient3AccountId, "Hoàng", "Văn Êm", new DateOnly(1988, 11, 5), "hoangvanem88@gmail.com", "em.hoang", defaultPwdHash, "+84900000008", "10.0.1.13", true, true, true, "12 Võ Văn Kiệt, Quận 1, TP.HCM", null) { RoleId = rolePatientId }
            );

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor(
                    doctor1AccountId,
                    "DOC001",
                    "Reproductive Endocrinology",
                    15,
                    new DateTime(2010, 1, 1),
                    true
                )
                { LicenseNumber = "LIC-DOC-001", Certificates = "Board Certified in Reproductive Medicine" },
                new Doctor(
                    doctor2AccountId,
                    "DOC002",
                    "Obstetrics and Gynecology",
                    10,
                    new DateTime(2015, 6, 1),
                    true
                )
                { LicenseNumber = "LIC-DOC-002", Certificates = "Specialist in IVF Procedures" },
                new Doctor(
                    doctor3AccountId,
                    "DOC003",
                    "Andrology",
                    20,
                    new DateTime(2005, 3, 15),
                    true
                )
                { LicenseNumber = "LIC-DOC-003", Certificates = "Expert in Male Infertility and Microsurgery" },
                new Doctor(
                    doctor4AccountId,
                    "DOC004",
                    "Embryology",
                    12,
                    new DateTime(2013, 7, 1),
                    true
                )
                { LicenseNumber = "LIC-DOC-004", Certificates = "Clinical Embryologist, ICSI Specialist" },
                new Doctor(
                    doctor5AccountId,
                    "DOC005",
                    "Reproductive Surgery",
                    25,
                    new DateTime(2000, 1, 10),
                    true
                )
                { LicenseNumber = "LIC-DOC-005", Certificates = "Laparoscopic and Hysteroscopic Surgery Expert" },
                new Doctor(
                    doctor6AccountId,
                    "DOC006",
                    "Reproductive Genetics",
                    8,
                    new DateTime(2017, 9, 1),
                    true
                )
                { LicenseNumber = "LIC-DOC-006", Certificates = "PGT-A/PGT-M Specialist, Genetic Counseling" },
                new Doctor(
                    doctor7AccountId,
                    "DOC007",
                    "Fertility Preservation",
                    14,
                    new DateTime(2011, 4, 20),
                    true
                )
                { LicenseNumber = "LIC-DOC-007", Certificates = "Oncofertility and Cryopreservation Expert" },
                new Doctor(
                    doctor8AccountId,
                    "DOC008",
                    "Reproductive Immunology",
                    18,
                    new DateTime(2007, 11, 5),
                    true
                )
                { LicenseNumber = "LIC-DOC-008", Certificates = "Recurrent Pregnancy Loss and Immunotherapy Specialist" }
            );

            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient(
                    patient1AccountId,
                    "PAT001",
                    "079090123456" // Cập nhật CCCD thực tế hơn (12 số)
                )
                { BloodType = "A+", EmergencyContact = "Lê Văn An", EmergencyPhone = "+84900000009" },
                new Patient(
                    patient2AccountId,
                    "PAT002",
                    "079092123457"
                )
                { BloodType = "B+", EmergencyContact = "Phạm Thị Giang", EmergencyPhone = "+84900000010" },
                new Patient(
                    patient3AccountId,
                    "PAT003",
                    "079088123458"
                )
                { BloodType = "O+", EmergencyContact = "Hoàng Văn Hùng", EmergencyPhone = "+84900000011" }
            );

            // ========================================
            // Seed Data: Service Categories & Services
            // (điều chỉnh theo file "Các service.docx")
            // ========================================
            var catLabTests = new Guid("10000000-0000-0000-0000-000000000011"); // Lab tests (xét nghiệm)
            var catImaging = new Guid("10000000-0000-0000-0000-000000000012"); // Imaging (siêu âm, HSG, HSC)
            var catGenetic = new Guid("10000000-0000-0000-0000-000000000013"); // Genetic testing (Karyotype, PGT, CFTR...)
            var catLabProcedures = new Guid("10000000-0000-0000-0000-000000000003"); // Lab procedures (IUI, OPU, ICSI, etc.)

            modelBuilder.Entity<ServiceCategory>().HasData(
                new ServiceCategory(catLabTests, "Laboratory Tests") { Code = "LAB", Description = "Clinical laboratory tests (female/male/post-IVF)", DisplayOrder = 1 },
                new ServiceCategory(catImaging, "Imaging & Diagnostic Procedures") { Code = "IMG", Description = "Ultrasound, HSG, hysteroscopy, other imaging", DisplayOrder = 2 },
                new ServiceCategory(catGenetic, "Genetic Testing") { Code = "GEN", Description = "Karyotype, Thalassemia, CFTR, PGT", DisplayOrder = 3 },
                new ServiceCategory(catLabProcedures, "Laboratory Procedures / IVF-IUI") { Code = "PROC", Description = "IUI, OPU, ICSI, sperm prep, embryo culture", DisplayOrder = 4 }
            );

            // --- Slots giữ nguyên (nếu cần) ---
            modelBuilder.Entity<Slot>().HasData(
                new Slot(new Guid("00000000-0000-0000-0000-000000000001"), new TimeSpan(8, 0, 0), new TimeSpan(10, 0, 0)) { Notes = "Morning Slot 1" },
                new Slot(new Guid("00000000-0000-0000-0000-000000000002"), new TimeSpan(10, 0, 0), new TimeSpan(12, 0, 0)) { Notes = "Morning Slot 2" },
                new Slot(new Guid("00000000-0000-0000-0000-000000000003"), new TimeSpan(13, 0, 0), new TimeSpan(15, 0, 0)) { Notes = "Afternoon Slot 1" },
                new Slot(new Guid("00000000-0000-0000-0000-000000000004"), new TimeSpan(15, 0, 0), new TimeSpan(17, 0, 0)) { Notes = "Afternoon Slot 2" }
            );

            // --- Services (category assignments updated) ---
            modelBuilder.Entity<Service>().HasData(
                // A. Lab test – Nữ: Bộ nội tiết (điền vào LAB)
                new Service(new Guid("20000000-0000-0000-0000-000000000101"), "FSH (female)", 200000m, catLabTests) { Code = "LAB-FSH-F", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000102"), "LH (female)", 200000m, catLabTests) { Code = "LAB-LH-F", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000103"), "Estradiol (E2) (female)", 200000m, catLabTests) { Code = "LAB-E2-F", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000104"), "AMH (female)", 775000m, catLabTests) { Code = "LAB-AMH-F", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000105"), "TSH (female)", 200000m, catLabTests) { Code = "LAB-TSH-F", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000106"), "FT4/FT3 (female)", 200000m, catLabTests) { Code = "LAB-FT-F", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000107"), "Prolactin (female)", 185000m, catLabTests) { Code = "LAB-PRL-F", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000108"), "Progesterone (female)", 200000m, catLabTests) { Code = "LAB-P4-F", Unit = "test" },

                // A. Miễn dịch – bệnh truyền nhiễm -> LAB
                new Service(new Guid("20000000-0000-0000-0000-000000000109"), "HIV screening", 150000m, catLabTests) { Code = "LAB-HIV", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000110"), "HBsAg", 125000m, catLabTests) { Code = "LAB-HBSAG", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000111"), "Anti-HCV", 185000m, catLabTests) { Code = "LAB-HCV", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000112"), "RPR/VDRL (syphilis)", 160000m, catLabTests) { Code = "LAB-RPR", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000113"), "Rubella IgG/IgM", 400000m, catLabTests) { Code = "LAB-RUB", Unit = "panel" },
                new Service(new Guid("20000000-0000-0000-0000-000000000114"), "CMV IgG/IgM", 400000m, catLabTests) { Code = "LAB-CMV", Unit = "panel" },
                new Service(new Guid("20000000-0000-0000-0000-000000000115"), "Chlamydia PCR", 575000m, catLabTests) { Code = "LAB-CHLA-PCR", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000116"), "Gonorrhea PCR", 575000m, catLabTests) { Code = "LAB-GONO-PCR", Unit = "test" },

                // A. Sinh hóa – huyết học -> LAB
                new Service(new Guid("20000000-0000-0000-0000-000000000117"), "Complete blood count (CBC)", 100000m, catLabTests) { Code = "LAB-CBC", Unit = "panel" },
                new Service(new Guid("20000000-0000-0000-0000-000000000118"), "Blood glucose", 65000m, catLabTests) { Code = "LAB-GLU", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000119"), "AST/ALT", 65000m, catLabTests) { Code = "LAB-LFT", Unit = "panel" },
                new Service(new Guid("20000000-0000-0000-0000-000000000120"), "Creatinine/Urea", 65000m, catLabTests) { Code = "LAB-KFT", Unit = "panel" },
                new Service(new Guid("20000000-0000-0000-0000-000000000121"), "Electrolyte panel", 160000m, catLabTests) { Code = "LAB-ELEC", Unit = "panel" },
                new Service(new Guid("20000000-0000-0000-0000-000000000122"), "ABO/Rh blood group", 115000m, catLabTests) { Code = "LAB-ABO", Unit = "test" },

                // B. Nam: Tinh dịch đồ -> LAB
                new Service(new Guid("20000000-0000-0000-0000-000000000123"), "Semen analysis (SA)", 350000m, catLabTests) { Code = "LAB-SA", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000124"), "Semen analysis repeat", 250000m, catLabTests) { Code = "LAB-SA-REP", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000125"), "MAR test", 525000m, catLabTests) { Code = "LAB-MAR", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000126"), "DNA Fragmentation (DFI)", 2500000m, catLabTests) { Code = "LAB-DFI", Unit = "test" },

                // B. Nam: Nội tiết nam -> LAB
                new Service(new Guid("20000000-0000-0000-0000-000000000127"), "FSH (male)", 200000m, catLabTests) { Code = "LAB-FSH-M", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000128"), "LH (male)", 200000m, catLabTests) { Code = "LAB-LH-M", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000129"), "Testosterone (male)", 200000m, catLabTests) { Code = "LAB-TESTO-M", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000130"), "Prolactin (male)", 185000m, catLabTests) { Code = "LAB-PRL-M", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000131"), "TSH (male)", 200000m, catLabTests) { Code = "LAB-TSH-M", Unit = "test" },

                // C. Xét nghiệm di truyền -> chuyển vào GEN
                new Service(new Guid("20000000-0000-0000-0000-000000000132"), "Karyotype", 1350000m, catGenetic) { Code = "LAB-KARYO", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000133"), "Thalassemia test", 950000m, catGenetic) { Code = "LAB-THALA", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000134"), "CFTR (cystic fibrosis)", 3000000m, catGenetic) { Code = "LAB-CFTR", Unit = "test" },

                // PGT-A/M per embryo -> đặt vào Genetic (file liệt kê PGT như xét nghiệm di truyền)
                new Service(new Guid("20000000-0000-0000-0000-000000000135"), "PGT-A/M per embryo", 19000000m, catGenetic) { Code = "LAB-PGT", Unit = "embryo" },

                // D. Lab tests sau IVF/IUI -> LAB
                new Service(new Guid("20000000-0000-0000-0000-000000000136"), "β-hCG", 150000m, catLabTests) { Code = "LAB-BHCG", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000137"), "Progesterone follow-up", 200000m, catLabTests) { Code = "LAB-P4-FU", Unit = "test" },
                new Service(new Guid("20000000-0000-0000-0000-000000000138"), "Estradiol follow-up", 200000m, catLabTests) { Code = "LAB-E2-FU", Unit = "test" },

                // Dịch vụ chẩn đoán hình ảnh -> chuyển sang IMAGING
                new Service(new Guid("20000000-0000-0000-0000-000000000139"), "Transvaginal ultrasound", 225000m, catImaging) { Code = "US-TVS", Unit = "scan" },
                new Service(new Guid("20000000-0000-0000-0000-000000000140"), "Abdominal ultrasound", 200000m, catImaging) { Code = "US-ABD", Unit = "scan" },
                new Service(new Guid("20000000-0000-0000-0000-000000000141"), "Follicular ultrasound", 225000m, catImaging) { Code = "US-FOLL", Unit = "scan" },
                new Service(new Guid("20000000-0000-0000-0000-000000000142"), "HSG (hysterosalpingogram)", 1500000m, catImaging) { Code = "IMG-HSG", Unit = "procedure" },
                new Service(new Guid("20000000-0000-0000-0000-000000000143"), "Diagnostic hysteroscopy", 4500000m, catImaging) { Code = "IMG-HSC", Unit = "procedure" },

                // Thủ thuật / procedures (IUI/OPU/ICSI/...) -> giữ ở LAB PROCEDURES
                new Service(new Guid("20000000-0000-0000-0000-000000000144"), "Sperm collection", 150000m, catLabProcedures) { Code = "LAB-SP-COLL", Unit = "procedure" },
                new Service(new Guid("20000000-0000-0000-0000-000000000145"), "Sperm wash", 650000m, catLabProcedures) { Code = "LAB-SP-WASH", Unit = "procedure" },
                new Service(new Guid("20000000-0000-0000-0000-000000000146"), "IUI procedure", 3500000m, catLabProcedures) { Code = "LAB-IUI", Unit = "procedure" },
                new Service(new Guid("20000000-0000-0000-0000-000000000147"), "OPU (oocyte pickup)", 11500000m, catLabProcedures) { Code = "LAB-OPU", Unit = "procedure" },
                new Service(new Guid("20000000-0000-0000-0000-000000000148"), "ICSI", 9000000m, catLabProcedures) { Code = "LAB-ICSI", Unit = "procedure" },
                new Service(new Guid("20000000-0000-0000-0000-000000000149"), "Embryo culture Day2–Day5", 8500000m, catLabProcedures) { Code = "LAB-EMB-D2D5", Unit = "cycle" },

                // New Appointment Service
                new Service(new Guid("60000000-0000-0000-0000-000000000001"), "Appointment", 100000m, catLabTests) { Code = "SERV-APPOINT", Unit = "service" }
            );


            // Seed Medicines (updated list mapped from clinical regimen table)
            modelBuilder.Entity<Medicine>().HasData(
                // 1. Clomiphene: Thuốc kích trứng dạng uống (thường cho IUI)
                new Medicine(new Guid("40000000-0000-0000-0000-000000000001"), "Clomiphene Citrate", "50 mg/day (max 150 mg/day)", "Oral")
                {
                    Indication = "Ovarian stimulation D2–D6",
                    Notes = "IUI"
                },

                // 2. Letrozole: Thuốc kích trứng dạng uống (ưu tiên cho buồng trứng đa nang - PCOS)
                new Medicine(new Guid("40000000-0000-0000-0000-000000000002"), "Letrozole", "2.5–5 mg/day", "Oral")
                {
                    Indication = "Ovarian stimulation D2–D6, PCOS",
                    Notes = "IUI"
                },

                // 3. Gonal-F / Puregon: Thuốc tiêm chứa FSH để kích thích nang trứng phát triển
                new Medicine(new Guid("40000000-0000-0000-0000-000000000003"), "Gonal-F / Puregon", "75–150 IU/day", "Subcutaneous injection")
                {
                    Indication = "Ovarian stimulation from D2–D3",
                    Notes = "IUI/IVF"
                },

                // 4. Menopur: Thuốc tiêm chứa cả FSH và LH (tinh chế từ nước tiểu)
                new Medicine(new Guid("40000000-0000-0000-0000-000000000004"), "Menopur", "75–150 IU/day", "Subcutaneous injection")
                {
                    Indication = "Ovarian stimulation",
                    Notes = "IUI/IVF"
                },

                // 5. Pergoveris: Thuốc tiêm tái tổ hợp FSH + LH, dùng cho người đáp ứng kém
                new Medicine(new Guid("40000000-0000-0000-0000-000000000005"), "Pergoveris", "150 IU FSH + 75 IU LH/day", "Subcutaneous injection")
                {
                    Indication = "Stimulation for poor responders",
                    Notes = "IVF"
                },

                // 6. Cetrotide: Thuốc ngăn rụng trứng sớm (Antagonist)
                new Medicine(new Guid("40000000-0000-0000-0000-000000000006"), "Cetrotide", "0.25 mg/day", "Subcutaneous injection")
                {
                    Indication = "Prevent premature ovulation from D5",
                    Notes = "IVF"
                },

                // 7. Orgalutran: Thuốc ngăn rụng trứng sớm tương tự Cetrotide
                new Medicine(new Guid("40000000-0000-0000-0000-000000000007"), "Orgalutran", "0.25 mg/day", "Subcutaneous injection")
                {
                    Indication = "Prevent premature ovulation from D5",
                    Notes = "IVF"
                },

                // 8. Ovidrel: Mũi tiêm kích rụng trứng (Trigger shot) tái tổ hợp
                new Medicine(new Guid("40000000-0000-0000-0000-000000000008"), "Ovidrel", "250 mcg single dose", "Subcutaneous injection")
                {
                    Indication = "Trigger when follicle is 18–20mm",
                    Notes = "IUI/IVF"
                },

                // 9. Pregnyl: Mũi tiêm kích rụng trứng (HCG) dạng tiêm bắp
                new Medicine(new Guid("40000000-0000-0000-0000-000000000009"), "Pregnyl", "5000–10000 IU single dose", "Intramuscular injection")
                {
                    Indication = "Trigger when follicle is 18–20mm",
                    Notes = "IUI/IVF"
                },

                // 10. Decapeptyl: Mũi Trigger thay thế HCG để giảm nguy cơ quá kích buồng trứng (OHSS)
                new Medicine(new Guid("40000000-0000-0000-0000-000000000010"), "Decapeptyl 0.1 mg", "0.1 mg single dose", "Subcutaneous injection")
                {
                    Indication = "Trigger to reduce OHSS risk",
                    Notes = "IVF"
                },

                // 11. Progesterone đặt: Hỗ trợ hoàng thể sau chọc hút/chuyển phôi
                new Medicine(new Guid("40000000-0000-0000-0000-000000000011"), "Progesterone Suppositories", "200 mg x 2–3 times/day", "Vaginal")
                {
                    Indication = "Post IUI/OPU/ET support",
                    Notes = "IUI/IVF/FET"
                },

                // 12. Duphaston: Progesterone tổng hợp dạng uống
                new Medicine(new Guid("40000000-0000-0000-0000-000000000012"), "Duphaston", "10 mg x 2–3 times/day", "Oral")
                {
                    Indication = "Luteal phase support",
                    Notes = "IUI/IVF"
                },

                // 13. Crinone 8%: Gel đặt âm đạo hỗ trợ hoàng thể
                new Medicine(new Guid("40000000-0000-0000-0000-000000000013"), "Crinone 8%", "1 applicator/day", "Vaginal")
                {
                    Indication = "Support post-ET",
                    Notes = "IVF/FET"
                },

                // 14. Proluton: Progesterone dạng dầu tiêm bắp
                new Medicine(new Guid("40000000-0000-0000-0000-000000000014"), "Proluton", "50 mg x 2–3 times/week", "Intramuscular injection")
                {
                    Indication = "Luteal support post OPU/ET",
                    Notes = "IVF"
                },

                // 15. Progynova: Estrogen dạng uống để làm dày niêm mạc tử cung
                new Medicine(new Guid("40000000-0000-0000-0000-000000000015"), "Progynova", "2 mg x 2–3 times/day", "Oral")
                {
                    Indication = "Endometrial thickening",
                    Notes = "FET"
                },

                // 16. Estradot: Miếng dán da chứa Estrogen
                new Medicine(new Guid("40000000-0000-0000-0000-000000000016"), "Estradot", "50–100 mcg/patch every 2 days", "Transdermal patch")
                {
                    Indication = "Endometrial thickening",
                    Notes = "FET"
                },

                // 17. CoQ10: Chất chống oxy hóa cải thiện chất lượng tinh trùng/trứng
                new Medicine(new Guid("40000000-0000-0000-0000-000000000017"), "CoQ10", "200–300 mg/day", "Oral")
                {
                    Indication = "Sperm quality improvement",
                    Notes = "Male"
                },

                // 18. Vitamin E: Vitamin chống oxy hóa bảo vệ tinh trùng
                new Medicine(new Guid("40000000-0000-0000-0000-000000000018"), "Vitamin E", "400 IU/day", "Oral")
                {
                    Indication = "Antioxidant for sperm",
                    Notes = "Male"
                },

                // 19. Clomiphene (Nam): Dùng để kích thích cơ thể nam giới tự sinh Testosterone
                new Medicine(new Guid("40000000-0000-0000-0000-000000000019"), "Clomiphene (Male)", "25 mg/day", "Oral")
                {
                    Indication = "Spermatogenesis support",
                    Notes = "Male"
                },

                // 20. HCG (Nam): Tiêm để kích thích tinh hoàn sinh tinh
                new Medicine(new Guid("40000000-0000-0000-0000-000000000020"), "HCG (Male)", "1500 IU x 2–3 times/week", "Injection")
                {
                    Indication = "Stimulate spermatogenesis",
                    Notes = "Male"
                },

                // 21. FSH (Nam): Tiêm phối hợp để tăng số lượng tinh trùng
                new Medicine(new Guid("40000000-0000-0000-0000-000000000021"), "FSH (Male)", "150 IU x 2–3 times/week", "Injection")
                {
                    Indication = "Increase sperm production",
                    Notes = "Male"
                }
            );

            // Seed CryoPackages (derived from storage pricing table)
            modelBuilder.Entity<CryoPackage>().HasData(
                // 1. Trữ đông noãn (trứng) - Gói 1 năm
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000001"), "Oocyte Freezing - 1 Year", 8000000m, 12, 10, SampleType.Oocyte)
                {
                    Description = "Initial fee 8,000,000 VND; storage 8,000,000 VND",
                    Notes = "1-year storage package for up to 10 oocytes"
                },

                // 2. Trữ đông noãn - Gói 3 năm (Tiết kiệm hơn)
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000002"), "Oocyte Freezing - 3 Years", 8000000m, 36, 20, SampleType.Oocyte)
                {
                    Description = "Initial fee 8,000,000 VND; storage 20,000,000 VND",
                    Notes = "Discounted compared to annual renewal"
                },

                // 3. Trữ đông noãn - Gói 5 năm (Dài hạn)
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000003"), "Oocyte Freezing - 5 Years", 8000000m, 60, 30, SampleType.Oocyte)
                {
                    Description = "Initial fee 8,000,000 VND; storage 30,000,000 VND",
                    Notes = "Best value for long-term storage"
                },

                // 4. Trữ đông tinh trùng - Gói 1 năm
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000004"), "Sperm Freezing - 1 Year", 2000000m, 12, 5, SampleType.Sperm)
                {
                    Description = "Initial fee 2,000,000 VND; storage 3,000,000 VND",
                    Notes = "Storage for up to 5 sperm samples"
                },

                // 5. Trữ đông tinh trùng - Gói 3 năm
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000005"), "Sperm Freezing - 3 Years", 2000000m, 36, 10, SampleType.Sperm)
                {
                    Description = "Initial fee 2,000,000 VND; storage 7,000,000 VND",
                    Notes = "Cost-effective multi-year plan"
                },

                // 6. Trữ đông tinh trùng - Gói 5 năm
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000006"), "Sperm Freezing - 5 Years", 2000000m, 60, 15, SampleType.Sperm)
                {
                    Description = "Initial fee 2,000,000 VND; storage 10,000,000 VND",
                    Notes = "Optimal for long-term preservation"
                },

                // 7. Trữ đông phôi - Gói 1 năm (tối đa 6 phôi/cọng)
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000007"), "Embryo Freezing - 1 Year", 10000000m, 12, 6, SampleType.Embryo)
                {
                    Description = "Initial fee 10,000,000 VND; storage 10,000,000 VND",
                    Notes = "Calculated for up to 6 embryos"
                },

                // 8. Trữ đông phôi - Gói 3 năm
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000008"), "Embryo Freezing - 3 Years", 10000000m, 36, 12, SampleType.Embryo)
                {
                    Description = "Initial fee 10,000,000 VND; storage 25,000,000 VND",
                    Notes = "Significant savings"
                },

                // 9. Trữ đông phôi - Gói 5 năm (Ưu tiên bệnh nhân IVF dư nhiều phôi)
                new CryoPackage(new Guid("50000000-0000-0000-0000-000000000009"), "Embryo Freezing - 5 Years", 10000000m, 60, 18, SampleType.Embryo)
                {
                    Description = "Initial fee 10,000,000 VND; storage 35,000,000 VND",
                    Notes = "Long-term plan, priority for IVF patients"
                }
            );
        }
    }
}
