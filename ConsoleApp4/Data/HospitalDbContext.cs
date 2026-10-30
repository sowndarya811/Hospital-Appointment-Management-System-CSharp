using Hospital_Appointment_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Appointment_Management_System.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
        {
        }

        public HospitalDbContext(
            DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;
                      Database=HospitalAppointmentDb;
                      Trusted_Connection=True;
                      TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Person
            modelBuilder.Entity<Person>()
                .HasKey(p => p.PersonId);

            // Patient
            modelBuilder.Entity<Patient>()
                .Property(p => p.FullName)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.PhoneNumber)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.Email)
                .IsRequired(false);

            // Doctor
            modelBuilder.Entity<Doctor>()
                .Property(d => d.Specialization)
                .IsRequired();

            modelBuilder.Entity<Doctor>()
                .Property(d => d.ConsultationFee)
                .HasColumnType("decimal(18,2)");

            // Appointment
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Reason)
                .IsRequired();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();

            // Appointment -> Patient
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment -> Doctor
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}