using Hospital_Appointment_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Appointment_Management_System.Data
{
    public class HospitalDbContext : DbContext
    {
        // Parameterless constructor
        public HospitalDbContext()
        {
        }

        // Constructor for Dependency Injection - we will use this later
        public HospitalDbContext(
            DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

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

            // Person primary key
            modelBuilder.Entity<Person>()
                .HasKey(p => p.PersonId);

            // Patient
            modelBuilder.Entity<Patient>()
                .Property(p => p.FullName)
                .IsRequired();

            modelBuilder.Entity<Patient>()
                .Property(p => p.PhoneNumber)
                .IsRequired();

            // Doctor
            modelBuilder.Entity<Doctor>()
                .Property(d => d.FullName)
                .IsRequired();

            modelBuilder.Entity<Doctor>()
                .Property(d => d.Specialization)
                .IsRequired();

            modelBuilder.Entity<Doctor>()
                .Property(d => d.ConsultationFee)
                .HasColumnType("decimal(18,2)");

            // Appointment primary key
            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.PatientId)
                .IsRequired();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.DoctorId)
                .IsRequired();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Reason)
                .IsRequired();

            // Appointment -> Patient
            modelBuilder.Entity<Appointment>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .HasPrincipalKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment -> Doctor
            modelBuilder.Entity<Appointment>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .HasPrincipalKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Store enum as text
            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();
        }
    }
}