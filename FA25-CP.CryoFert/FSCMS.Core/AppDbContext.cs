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

        // Core Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        
        // Treatment Management
        public DbSet<TreatmentCycle> TreatmentCycles { get; set; }
        public DbSet<IVFCycle> IVFCycles { get; set; }
        public DbSet<IUICycle> IUICycles { get; set; }
        public DbSet<TreatmentTimeline> TreatmentTimelines { get; set; }
        public DbSet<CycleMonitoring> CycleMonitorings { get; set; }
        
        // Cryobank Management
        public DbSet<CryobankTank> CryobankTanks { get; set; }
        public DbSet<CryobankPosition> CryobankPositions { get; set; }
        public DbSet<Specimen> Specimens { get; set; }
        
        // Quality Assessments
        public DbSet<OocyteAssessment> OocyteAssessments { get; set; }
        public DbSet<SpermAnalysis> SpermAnalyses { get; set; }
        public DbSet<EmbryoAssessment> EmbryoAssessments { get; set; }
        public DbSet<PGTResult> PGTResults { get; set; }
        
        // Service Management
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<ServicePackage> ServicePackages { get; set; }
        public DbSet<ServicePackageItem> ServicePackageItems { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        
        // Appointment & Scheduling
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        
        // Communication & Feedback
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<FeedbackResponse> FeedbackResponses { get; set; }
        
        // Documentation & Legal
        public DbSet<ConsentForm> ConsentForms { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Content> Contents { get; set; }
        
        // System Management
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<TestType> TestTypes { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Commitment> Commitments { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<EmbryoTransfer> EmbryoTransfers { get; set; }
        public DbSet<Cryopreservation> Cryopreservations { get; set; }
        public DbSet<Thawing> Thawings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User & Role Relationships
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient & User Relationship
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor & User Relationship
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.DoctorProfile)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Treatment Cycle Relationships
            modelBuilder.Entity<TreatmentCycle>()
                .HasOne(tc => tc.Patient)
                .WithMany(p => p.TreatmentCycles)
                .HasForeignKey(tc => tc.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TreatmentCycle>()
                .HasOne(tc => tc.Doctor)
                .WithMany()
                .HasForeignKey(tc => tc.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // IVF & IUI Cycle Relationships
            modelBuilder.Entity<IVFCycle>()
                .HasOne(ivc => ivc.TreatmentCycle)
                .WithOne(tc => tc.IVFCycle)
                .HasForeignKey<IVFCycle>(ivc => ivc.TreatmentCycleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IUICycle>()
                .HasOne(iuc => iuc.TreatmentCycle)
                .WithOne(tc => tc.IUICycle)
                .HasForeignKey<IUICycle>(iuc => iuc.TreatmentCycleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cryobank Relationships
            modelBuilder.Entity<CryobankPosition>()
                .HasOne(cp => cp.Tank)
                .WithMany(ct => ct.Positions)
                .HasForeignKey(cp => cp.TankId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Specimen>()
                .HasOne(s => s.Patient)
                .WithMany(p => p.Specimens)
                .HasForeignKey(s => s.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Specimen>()
                .HasOne(s => s.CryobankPosition)
                .WithMany(cp => cp.Specimens)
                .HasForeignKey(s => s.CryobankPositionId)
                .OnDelete(DeleteBehavior.SetNull);

            // Service Provider Relationships
            modelBuilder.Entity<Service>()
                .HasOne(s => s.ServiceProvider)
                .WithMany(p => p.Services)
                .HasForeignKey(s => s.ServiceProviderId)
                .OnDelete(DeleteBehavior.SetNull);

            // Quality Assessment Relationships
            modelBuilder.Entity<OocyteAssessment>()
                .HasOne(oa => oa.Specimen)
                .WithMany(s => s.OocyteAssessments)
                .HasForeignKey(oa => oa.SpecimenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SpermAnalysis>()
                .HasOne(sa => sa.Specimen)
                .WithMany(s => s.SpermAnalyses)
                .HasForeignKey(sa => sa.SpecimenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmbryoAssessment>()
                .HasOne(ea => ea.Specimen)
                .WithMany(s => s.EmbryoAssessments)
                .HasForeignKey(ea => ea.SpecimenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PGTResult>()
                .HasOne(pgt => pgt.Specimen)
                .WithMany(s => s.PGTResults)
                .HasForeignKey(pgt => pgt.SpecimenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Service Package Relationships
            modelBuilder.Entity<ServicePackageItem>()
                .HasOne(spi => spi.ServicePackage)
                .WithMany(sp => sp.PackageItems)
                .HasForeignKey(spi => spi.ServicePackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServicePackageItem>()
                .HasOne(spi => spi.Service)
                .WithMany()
                .HasForeignKey(spi => spi.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Service Request Relationships
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Patient)
                .WithMany(p => p.ServiceRequests)
                .HasForeignKey(sr => sr.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.ServicePackage)
                .WithMany(sp => sp.ServiceRequests)
                .HasForeignKey(sr => sr.ServicePackageId)
                .OnDelete(DeleteBehavior.SetNull);

            // Appointment Relationships
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // CheckIn Relationships
            modelBuilder.Entity<CheckIn>()
                .HasOne(ci => ci.Patient)
                .WithMany()
                .HasForeignKey(ci => ci.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CheckIn>()
                .HasOne(ci => ci.Appointment)
                .WithMany()
                .HasForeignKey(ci => ci.AppointmentId)
                .OnDelete(DeleteBehavior.SetNull);

            // DoctorSchedule Relationships
            modelBuilder.Entity<DoctorSchedule>()
                .HasOne(ds => ds.Doctor)
                .WithMany()
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Encounter Relationships
            modelBuilder.Entity<Encounter>()
                .HasOne(e => e.Patient)
                .WithMany(p => p.Encounters)
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Feedback Relationships
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Consent Form Relationships
            modelBuilder.Entity<ConsentForm>()
                .HasOne(cf => cf.Patient)
                .WithMany(p => p.ConsentForms)
                .HasForeignKey(cf => cf.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Treatment Timeline Relationships
            modelBuilder.Entity<TreatmentTimeline>()
                .HasOne(tt => tt.TreatmentCycle)
                .WithMany(tc => tc.Timelines)
                .HasForeignKey(tt => tt.TreatmentCycleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TreatmentTimeline>()
                .HasOne(tt => tt.Patient)
                .WithMany()
                .HasForeignKey(tt => tt.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cycle Monitoring Relationships
            modelBuilder.Entity<CycleMonitoring>()
                .HasOne(cm => cm.TreatmentCycle)
                .WithMany(tc => tc.Monitorings)
                .HasForeignKey(cm => cm.TreatmentCycleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment Relationships
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Patient)
                .WithMany(pat => pat.Payments)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Invoice Relationships
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Patient)
                .WithMany()
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany(i => i.Items)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Service)
                .WithMany()
                .HasForeignKey(ii => ii.ServiceId)
                .OnDelete(DeleteBehavior.SetNull);

            // Content Relationships
            modelBuilder.Entity<Content>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.ContentsCreated)
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Medical History
            modelBuilder.Entity<MedicalHistory>()
                .HasOne(mh => mh.Patient)
                .WithMany()
                .HasForeignKey(mh => mh.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lab Tests
            modelBuilder.Entity<LabTest>()
                .HasOne(lt => lt.Patient)
                .WithMany()
                .HasForeignKey(lt => lt.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LabTest>()
                .HasOne(lt => lt.TestType)
                .WithMany()
                .HasForeignKey(lt => lt.TestTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LabTest>()
                .HasOne(lt => lt.OrderedByUser)
                .WithMany()
                .HasForeignKey(lt => lt.OrderedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LabTest>()
                .HasOne(lt => lt.CollectedByUser)
                .WithMany()
                .HasForeignKey(lt => lt.CollectedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TestResult>()
                .HasOne(tr => tr.LabTest)
                .WithOne(lt => lt.Result)
                .HasForeignKey<TestResult>(tr => tr.LabTestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestResult>()
                .HasOne(tr => tr.VerifiedByUser)
                .WithMany()
                .HasForeignKey(tr => tr.VerifiedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Commitment & Contract
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Patient)
                .WithMany()
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Embryo Transfer
            modelBuilder.Entity<EmbryoTransfer>()
                .HasOne(et => et.Patient)
                .WithMany()
                .HasForeignKey(et => et.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmbryoTransfer>()
                .HasOne(et => et.Doctor)
                .WithMany()
                .HasForeignKey(et => et.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmbryoTransfer>()
                .HasOne(et => et.TreatmentTimeline)
                .WithMany()
                .HasForeignKey(et => et.TreatmentTimelineId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<EmbryoTransfer>()
                .HasOne(et => et.Specimen)
                .WithMany()
                .HasForeignKey(et => et.SpecimenId)
                .OnDelete(DeleteBehavior.SetNull);

            // Cryopreservation & Thawing
            modelBuilder.Entity<Cryopreservation>()
                .HasOne(cr => cr.Specimen)
                .WithMany()
                .HasForeignKey(cr => cr.SpecimenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cryopreservation>()
                .HasOne(cr => cr.WitnessUser)
                .WithMany()
                .HasForeignKey(cr => cr.WitnessUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Thawing>()
                .HasOne(th => th.Specimen)
                .WithMany()
                .HasForeignKey(th => th.SpecimenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Thawing>()
                .HasOne(th => th.WitnessUser)
                .WithMany()
                .HasForeignKey(th => th.WitnessUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Audit Log Relationships
            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
