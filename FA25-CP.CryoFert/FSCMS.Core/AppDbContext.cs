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
                .WithMany()
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
                .WithMany()
                .HasForeignKey(ce => ce.CryoLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================
            // Nhóm 5: CryoPackage, CryoStorageContract, CPSDetail
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

            // ========================================
            // Nhóm 6: Transaction & Media
            // ========================================
            // These are independent tables with logical relationships only
            // No foreign key constraints needed
        }
    }
}
