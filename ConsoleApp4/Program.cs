using Hospital_Appointment_Management_System.Models;
using Hospital_Appointment_Management_System.Services;
using Hospital_Appointment_Management_System.UI;

namespace Hospital_Appointment_Management_System
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create repositories
            IRepository<Patient> patientRepository =
                new Repository<Patient>(p => p.PersonId);

            IRepository<Doctor> doctorRepository =
                new Repository<Doctor>(d => d.PersonId);

            IRepository<Appointment> appointmentRepository =
                new Repository<Appointment>(a => a.AppointmentId);

            // Create services
            PatientService patientService =
                new PatientService(patientRepository);

            DoctorService doctorService =
                new DoctorService(doctorRepository);

            AppointmentService appointmentService =
                new AppointmentService(appointmentRepository);

            ReportService reportService =
                new ReportService(
                    appointmentRepository,
                    doctorRepository);

            // Create Menu
            Menu menu = new Menu(
                patientService,
                doctorService,
                appointmentService,
                reportService);

            // Start application
            menu.Show();
        }
    }
}