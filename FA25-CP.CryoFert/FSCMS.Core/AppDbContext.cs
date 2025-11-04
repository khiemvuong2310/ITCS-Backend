using Microsoft.EntityFrameworkCore;
using FSCMS.Core.Entities;
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
                .HasForeignKey<Patient>(p => p.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // Account & Doctor Relationship (One-to-One)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Doctor)
                .WithOne(d => d.Account)
                .HasForeignKey<Doctor>(d => d.AccountId)
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

            // Slot & DoctorSchedule Relationship
            modelBuilder.Entity<Slot>()
                .HasOne(s => s.DoctorSchedule)
                .WithMany(ds => ds.Slots)
                .HasForeignKey(s => s.DoctorScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Slot & Appointment Relationship (One-to-One)
            modelBuilder.Entity<Slot>()
                .HasOne(s => s.Appointment)
                .WithOne(a => a.Slot)
                .HasForeignKey<Appointment>(a => a.SlotId)
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
                .HasForeignKey<TreatmentIVF>(tivf => tivf.TreatmentId)
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

            // MedicalRecord & Appointment Relationship (One-to-One)
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Appointment)
                .WithOne(a => a.MedicalRecord)
                .HasForeignKey<MedicalRecord>(mr => mr.AppointmentId)
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
            // Nhóm 6: Transaction & Media
            // ========================================
            // These are independent tables with logical relationships only
            // No foreign key constraints needed

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
            var patient1AccountId = new Guid("00000000-0000-0000-0000-000000010006");
            var patient2AccountId = new Guid("00000000-0000-0000-0000-000000010007");
            var patient3AccountId = new Guid("00000000-0000-0000-0000-000000010008");
            
            modelBuilder.Entity<Account>().HasData(
                new Account(adminAccountId, "System", "Admin", null, "admin@cryo.com", "admin", defaultPwdHash, "+84900000001", null, null, true, true, null, null) { RoleId = roleAdminId },
                new Account(labAccountId, "Lab", "Technician", null, "lab@cryo.com", "lab", defaultPwdHash, "+84900000002", null, null, true, true, null, null) { RoleId = roleLabId },
                new Account(receptionistAccountId, "Front", "Receptionist", null, "receptionist@cryo.com", "receptionist", defaultPwdHash, "+84900000003", null, null, true, true, null, null) { RoleId = roleReceptionistId },
                // Doctor accounts
                new Account(doctor1AccountId, "Nguyen", "Van A", new DateTime(1980, 5, 15), "doctor1@cryo.com", "doctor1", defaultPwdHash, "+84900000004", null, true, true, true, null, null) { RoleId = roleDoctorId },
                new Account(doctor2AccountId, "Tran", "Thi B", new DateTime(1985, 8, 20), "doctor2@cryo.com", "doctor2", defaultPwdHash, "+84900000005", null, false, true, true, null, null) { RoleId = roleDoctorId },
                // Patient accounts
                new Account(patient1AccountId, "Le", "Van C", new DateTime(1990, 3, 10), "patient1@cryo.com", "patient1", defaultPwdHash, "+84900000006", null, true, true, true, null, null) { RoleId = rolePatientId },
                new Account(patient2AccountId, "Pham", "Thi D", new DateTime(1992, 7, 25), "patient2@cryo.com", "patient2", defaultPwdHash, "+84900000007", null, false, true, true, null, null) { RoleId = rolePatientId },
                new Account(patient3AccountId, "Hoang", "Van E", new DateTime(1988, 11, 5), "patient3@cryo.com", "patient3", defaultPwdHash, "+84900000008", null, true, true, true, null, null) { RoleId = rolePatientId }
            );

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor(
                    new Guid("00000000-0000-0000-0000-000000020001"),
                    "DOC001",
                    "Reproductive Endocrinology",
                    15,
                    new DateTime(2010, 1, 1),
                    true
                ) { AccountId = doctor1AccountId, LicenseNumber = "LIC-DOC-001", Certificates = "Board Certified in Reproductive Medicine" },
                new Doctor(
                    new Guid("00000000-0000-0000-0000-000000020002"),
                    "DOC002",
                    "Obstetrics and Gynecology",
                    10,
                    new DateTime(2015, 6, 1),
                    true
                ) { AccountId = doctor2AccountId, LicenseNumber = "LIC-DOC-002", Certificates = "Specialist in IVF Procedures" }
            );

            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient(
                    new Guid("00000000-0000-0000-0000-000000030001"),
                    "PAT001",
                    "001234567890"
                ) { AccountId = patient1AccountId, BloodType = "A+", EmergencyContact = "Le Van F", EmergencyPhone = "+84900000009" },
                new Patient(
                    new Guid("00000000-0000-0000-0000-000000030002"),
                    "PAT002",
                    "001234567891"
                ) { AccountId = patient2AccountId, BloodType = "B+", EmergencyContact = "Pham Thi G", EmergencyPhone = "+84900000010" },
                new Patient(
                    new Guid("00000000-0000-0000-0000-000000030003"),
                    "PAT003",
                    "001234567892"
                ) { AccountId = patient3AccountId, BloodType = "O+", EmergencyContact = "Hoang Van H", EmergencyPhone = "+84900000011" }
            );

            // ========================================
            // Seed Data: Service Categories & Services
            // ========================================
            var catConsultation = new Guid("10000000-0000-0000-0000-000000000001");
            var catDiagnostics = new Guid("10000000-0000-0000-0000-000000000002");
            var catLabProcedures = new Guid("10000000-0000-0000-0000-000000000003");
            var catCryoStorage = new Guid("10000000-0000-0000-0000-000000000004");
            var catTreatment = new Guid("10000000-0000-0000-0000-000000000005");
            var catMedication = new Guid("10000000-0000-0000-0000-000000000006");
            var catAdministrative = new Guid("10000000-0000-0000-0000-000000000007");

            //Danh mục dịch vụ cốt lõi của hệ thống (phân nhóm: khám, cận lâm sàng, lab, cryo, thủ thuật, thuốc, hành chính)
            modelBuilder.Entity<ServiceCategory>().HasData(
                new ServiceCategory(catConsultation, "Consultation") { Code = "CONS", Description = "Clinical consultations", DisplayOrder = 1 },
                new ServiceCategory(catDiagnostics, "Diagnostics & Imaging") { Code = "DIAG", Description = "Diagnostic tests and imaging", DisplayOrder = 2 },
                new ServiceCategory(catLabProcedures, "Laboratory Procedures") { Code = "LAB", Description = "Embryology and andrology procedures", DisplayOrder = 3 },
                new ServiceCategory(catCryoStorage, "Cryostorage & Logistics") { Code = "CRYO", Description = "Cryopreservation and storage services", DisplayOrder = 4 },
                new ServiceCategory(catTreatment, "Treatment Procedures") { Code = "TRMT", Description = "IUI/IVF related procedures", DisplayOrder = 5 },
                new ServiceCategory(catMedication, "Medications") { Code = "MED", Description = "Medications and injections", DisplayOrder = 6 },
                new ServiceCategory(catAdministrative, "Administrative & Others") { Code = "ADMIN", Description = "Administrative fees", DisplayOrder = 7 }
            );

            //Các dịch vụ tiêu biểu cho lĩnh vực hỗ trợ sinh sản & cryobank (kèm giá, đơn vị, thời lượng nếu có)
            modelBuilder.Entity<Service>().HasData(
                // Consultation
                // Khám tư vấn ban đầu: bác sĩ khai thác bệnh sử, đánh giá khả năng sinh sản
                new Service(new Guid("20000000-0000-0000-0000-000000000001"), "Initial fertility consultation", 500000m, catConsultation) { Code = "CONS-INIT", Unit = "session", Duration = 30, Description = "First-time visit and clinical assessment" },
                // Khám tái khám: theo dõi tiến triển, điều chỉnh kế hoạch điều trị
                new Service(new Guid("20000000-0000-0000-0000-000000000002"), "Follow-up consultation", 300000m, catConsultation) { Code = "CONS-FUP", Unit = "session", Duration = 20, Description = "Follow-up review and plan" },

                // Diagnostics & Imaging
                // Siêu âm đầu dò âm đạo: đánh giá buồng trứng, tử cung, niêm mạc
                new Service(new Guid("20000000-0000-0000-0000-000000000010"), "Transvaginal ultrasound", 400000m, catDiagnostics) { Code = "US-TVS", Unit = "scan", Duration = 15 },
                // Xét nghiệm nội tiết cơ bản: AMH/FSH/LH/E2/PRL để đánh giá dự trữ buồng trứng và trục nội tiết
                new Service(new Guid("20000000-0000-0000-0000-000000000011"), "Baseline hormone panel (AMH/FSH/LH/E2/PRL)", 1500000m, catDiagnostics) { Code = "LAB-HORM", Unit = "panel" },
                // Tinh dịch đồ: đánh giá số lượng, di động, hình dạng tinh trùng
                new Service(new Guid("20000000-0000-0000-0000-000000000012"), "Semen analysis", 600000m, catDiagnostics) { Code = "SA", Unit = "test" },

                // Laboratory Procedures
                // Chọc hút noãn (OPU): lấy noãn sau kích thích buồng trứng
                new Service(new Guid("20000000-0000-0000-0000-000000000020"), "Oocyte retrieval (OPU)", 7000000m, catLabProcedures) { Code = "OPU", Unit = "procedure" },
                // Chuẩn bị tinh trùng: lọc rửa để sử dụng cho IUI/IVF
                new Service(new Guid("20000000-0000-0000-0000-000000000021"), "Sperm preparation (IUI/IVF)", 900000m, catLabProcedures) { Code = "SP-PREP", Unit = "prep" },
                // Nuôi cấy phôi ngày 1-5: theo dõi và chăm sóc phôi trong labo
                new Service(new Guid("20000000-0000-0000-0000-000000000022"), "Embryo culture (day 1-5)", 5000000m, catLabProcedures) { Code = "EMB-CULT", Unit = "cycle" },
                // ICSI: tiêm tinh trùng vào bào tương noãn hỗ trợ thụ tinh
                new Service(new Guid("20000000-0000-0000-0000-000000000023"), "ICSI", 8000000m, catLabProcedures) { Code = "ICSI", Unit = "procedure" },
                // Chuyển phôi (ET): đưa phôi vào buồng tử cung
                new Service(new Guid("20000000-0000-0000-0000-000000000024"), "Embryo transfer (ET)", 4000000m, catLabProcedures) { Code = "ET", Unit = "procedure" },

                // Cryostorage & Logistics
                // Thuỷ tinh hoá noãn: làm lạnh siêu nhanh để bảo tồn noãn
                new Service(new Guid("20000000-0000-0000-0000-000000000030"), "Oocyte vitrification", 3000000m, catCryoStorage) { Code = "VIT-OOC", Unit = "procedure" },
                // Trữ đông tinh trùng
                new Service(new Guid("20000000-0000-0000-0000-000000000031"), "Sperm cryopreservation", 1200000m, catCryoStorage) { Code = "CRYO-SP", Unit = "procedure" },
                // Thuỷ tinh hoá phôi
                new Service(new Guid("20000000-0000-0000-0000-000000000032"), "Embryo vitrification", 3500000m, catCryoStorage) { Code = "VIT-EMB", Unit = "procedure" },
                // Phí lưu trữ hằng năm mỗi mẫu
                new Service(new Guid("20000000-0000-0000-0000-000000000033"), "Annual storage fee (per specimen)", 1500000m, catCryoStorage) { Code = "STORE-ANNUAL", Unit = "year" },
                // Rã đông mẫu lưu trữ
                new Service(new Guid("20000000-0000-0000-0000-000000000034"), "Specimen thawing", 1000000m, catCryoStorage) { Code = "THAW", Unit = "procedure" },

                // Treatment Procedures
                // Bơm tinh trùng vào buồng tử cung (IUI)
                new Service(new Guid("20000000-0000-0000-0000-000000000040"), "Intrauterine insemination (IUI)", 2500000m, catTreatment) { Code = "IUI", Unit = "cycle" },
                // Chu kỳ thụ tinh ống nghiệm (IVF)
                new Service(new Guid("20000000-0000-0000-0000-000000000041"), "In vitro fertilization (IVF) cycle", 35000000m, catTreatment) { Code = "IVF", Unit = "cycle" },
                // Chuyển phôi đông lạnh (FET)
                new Service(new Guid("20000000-0000-0000-0000-000000000042"), "Frozen embryo transfer (FET)", 12000000m, catTreatment) { Code = "FET", Unit = "cycle" },

                // Medications
                // Bút thuốc kích thích buồng trứng (Gonadotropin)
                new Service(new Guid("20000000-0000-0000-0000-000000000050"), "Gonadotropin stimulation (per pen)", 1200000m, catMedication) { Code = "GONA-PEN", Unit = "pen" },
                // Mũi tiêm kích rụng trứng (hCG trigger)
                new Service(new Guid("20000000-0000-0000-0000-000000000051"), "Trigger injection (hCG)", 450000m, catMedication) { Code = "HCG", Unit = "dose" },

                // Administrative & Others
                // Phí mở hồ sơ bệnh án ban đầu
                new Service(new Guid("20000000-0000-0000-0000-000000000060"), "Medical record creation fee", 100000m, catAdministrative) { Code = "ADMIN-MR", Unit = "case" },
                // Cấp giấy tờ/xác nhận/báo cáo theo yêu cầu
                new Service(new Guid("20000000-0000-0000-0000-000000000061"), "Certificate/Report issuance", 150000m, catAdministrative) { Code = "ADMIN-CERT", Unit = "doc" }
            );
        }
    }
}
