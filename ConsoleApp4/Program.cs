using Hospital_Appointment_Management_System.Data;
using Hospital_Appointment_Management_System.Models;
using Hospital_Appointment_Management_System.Repositories;
using Hospital_Appointment_Management_System.Services;
using Hospital_Appointment_Management_System.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hospital_Appointment_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // Register DbContext
            builder.Services.AddDbContext<HospitalDbContext>();

            // Register Repositories
            builder.Services.AddScoped<IRepository<Patient>, PatientRepository>();
            builder.Services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();

            // Register Audit Log Repository
            builder.Services.AddScoped<AuditLogRepository>();

            // Register Services
            builder.Services.AddScoped<PatientService>();
            builder.Services.AddScoped<DoctorService>();
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddScoped<ReportService>();

            // Register Audit Log Service
            builder.Services.AddScoped<AuditLogService>();

            // Register Menu
            builder.Services.AddScoped<Menu>();

            // Build application
            var app = builder.Build();

            // Get Menu from Dependency Injection
            Menu menu = app.Services.GetRequiredService<Menu>();

            // Start application
            menu.Show();
        }
    }
}