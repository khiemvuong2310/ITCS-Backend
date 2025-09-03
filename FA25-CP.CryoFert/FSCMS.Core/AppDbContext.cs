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

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TreatmentTimeline> TreatmentTimelines { get; set; }
        public DbSet<SpecimenQuality> SpecimenQualities { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<FeedbackResponse> FeedbackResponses { get; set; }
        public DbSet<IUIProcedure> IUIProcedures { get; set; }
        public DbSet<IVFProcedure> IVFProcedures { get; set; }
        public DbSet<Specimen> Specimens { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne(u => u.DoctorProfile)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Content>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.ContentsCreated)
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Encounter>()
                .HasOne(e => e.Patient)
                .WithMany(u => u.Encounters)
                .HasForeignKey(e => e.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Encounter>()
                .HasOne(e => e.Provider)
                .WithMany()
                .HasForeignKey(e => e.ProviderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Patient)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Patient)
                .WithMany(u => u.ServiceRequests)
                .HasForeignKey(sr => sr.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Doctor)
                .WithMany()
                .HasForeignKey(sr => sr.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Patient)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.PatientID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
