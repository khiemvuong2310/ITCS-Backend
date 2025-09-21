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
        public DbSet<ServicePackage> ServicePackages { get; set; }
        public DbSet<ServicePackageItem> ServicePackageItems { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        
        // Appointment & Scheduling
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        
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
        public DbSet<Report> Reports { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

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

            // Encounter Relationships
            modelBuilder.Entity<Encounter>()
                .HasOne(e => e.Patient)
                .WithMany(p => p.Encounters)
                .HasForeignKey(e => e.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            // Feedback Relationships
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Patient)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.PatientID)
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
                .HasForeignKey(p => p.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            // Content Relationships
            modelBuilder.Entity<Content>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.ContentsCreated)
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Audit Log Relationships
            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
