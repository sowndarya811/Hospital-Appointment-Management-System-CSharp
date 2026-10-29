using Hospital_Appointment_Management_System.Exceptions;
using Hospital_Appointment_Management_System.Models;
using Hospital_Appointment_Management_System.Services;

namespace Hospital_Appointment_Management_System.UI
{
    public class Menu
    {
        private readonly PatientService patientService;
        private readonly DoctorService doctorService;
        private readonly AppointmentService appointmentService;
        private readonly ReportService reportService;

        public Menu(
            PatientService patientService,
            DoctorService doctorService,
            AppointmentService appointmentService,
            ReportService reportService)
        {
            this.patientService = patientService;
            this.doctorService = doctorService;
            this.appointmentService = appointmentService;
            this.reportService = reportService;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("   HOSPITAL APPOINTMENT SYSTEM");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Manage Patients");
                Console.WriteLine("2. Manage Doctors");
                Console.WriteLine("3. Manage Appointments");
                Console.WriteLine("4. Reports");
                Console.WriteLine("5. Save Data");
                Console.WriteLine("0. Exit");

                Console.Write("Enter choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManagePatients();
                        break;

                    case "2":
                        ManageDoctors();
                        break;

                    case "3":
                        ManageAppointments();
                        break;

                    case "4":
                        ShowReports();
                        break;

                    case "5":
                        Console.WriteLine("Data saved successfully.");
                        break;

                    case "0":
                        Console.WriteLine("Thank you!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // =========================
        // PATIENT MANAGEMENT
        // =========================

        private void ManagePatients()
        {
            while (true)
            {
                Console.WriteLine("\n----------- PATIENT MANAGEMENT -----------");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. View All Patients");
                Console.WriteLine("3. Search Patient");
                Console.WriteLine("4. Update Patient");
                Console.WriteLine("5. Delete Patient");
                Console.WriteLine("0. Back");

                Console.Write("Enter choice: ");
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddPatient();
                            break;

                        case "2":
                            ViewAllPatients();
                            break;

                        case "3":
                            SearchPatient();
                            break;

                        case "4":
                            UpdatePatient();
                            break;

                        case "5":
                            DeletePatient();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (HospitalException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        private void AddPatient()
        {
            Console.Write("Enter Patient ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter Phone Number: ");
            string phone = Console.ReadLine() ?? "";

            Console.Write("Enter Email: ");
            string? email = Console.ReadLine();

            Patient patient = new Patient(
                id,
                name,
                age,
                phone,
                email);

            patientService.AddPatient(patient);

            Console.WriteLine("Patient added successfully.");
        }

        private void ViewAllPatients()
        {
            List<Patient> patients =
                patientService.GetAllPatients();

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found.");
                return;
            }

            Console.WriteLine("\n----------- ALL PATIENTS -----------");

            foreach (Patient patient in patients)
            {
                patient.DisplayInfo();
                Console.WriteLine("------------------------------------");
            }
        }

        private void SearchPatient()
        {
            Console.Write("Enter patient name: ");
            string name = Console.ReadLine() ?? "";

            List<Patient> patients =
                patientService.SearchPatients(name);

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found.");
                return;
            }

            foreach (Patient patient in patients)
            {
                patient.DisplayInfo();
                Console.WriteLine("------------------------------------");
            }
        }

        private void UpdatePatient()
        {
            Console.Write("Enter Patient ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Enter new name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter new age: ");
            int age = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter new phone number: ");
            string phone = Console.ReadLine() ?? "";

            Console.Write("Enter new email: ");
            string? email = Console.ReadLine();

            patientService.UpdatePatient(
                id,
                name,
                age,
                phone,
                email);

            Console.WriteLine("Patient updated successfully.");
        }

        private void DeletePatient()
        {
            Console.Write("Enter Patient ID: ");
            string id = Console.ReadLine() ?? "";

            patientService.DeletePatient(id);

            Console.WriteLine("Patient deleted successfully.");
        }

        // =========================
        // DOCTOR MANAGEMENT
        // =========================

        private void ManageDoctors()
        {
            while (true)
            {
                Console.WriteLine("\n----------- DOCTOR MANAGEMENT -----------");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. View All Doctors");
                Console.WriteLine("3. Search Doctor");
                Console.WriteLine("4. Update Doctor");
                Console.WriteLine("5. Delete Doctor");
                Console.WriteLine("0. Back");

                Console.Write("Enter choice: ");
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddDoctor();
                            break;

                        case "2":
                            ViewAllDoctors();
                            break;

                        case "3":
                            SearchDoctor();
                            break;

                        case "4":
                            UpdateDoctor();
                            break;

                        case "5":
                            DeleteDoctor();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (HospitalException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        private void AddDoctor()
        {
            Console.Write("Enter Doctor ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Enter Doctor Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Specialization: ");
            string specialization = Console.ReadLine() ?? "";

            Console.Write("Enter Consultation Fee: ");
            decimal fee = decimal.Parse(
                Console.ReadLine() ?? "0");

            Doctor doctor = new Doctor(
                id,
                name,
                specialization,
                fee);

            doctorService.AddDoctor(doctor);

            Console.WriteLine("Doctor added successfully.");
        }

        private void ViewAllDoctors()
        {
            List<Doctor> doctors =
                doctorService.GetAll();

            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            Console.WriteLine("\n----------- ALL DOCTORS -----------");

            foreach (Doctor doctor in doctors)
            {
                doctor.DisplayInfo();
                Console.WriteLine("-----------------------------------");
            }
        }

        private void SearchDoctor()
        {
            Console.Write("Enter name or specialization: ");
            string value = Console.ReadLine() ?? "";

            List<Doctor> doctors =
                doctorService.Search(value);

            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            foreach (Doctor doctor in doctors)
            {
                doctor.DisplayInfo();
                Console.WriteLine("-----------------------------------");
            }
        }

        private void UpdateDoctor()
        {
            Console.Write("Enter Doctor ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Enter new name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter new specialization: ");
            string specialization =
                Console.ReadLine() ?? "";

            Console.Write("Enter new consultation fee: ");
            decimal fee = decimal.Parse(
                Console.ReadLine() ?? "0");

            doctorService.Update(
                id,
                name,
                specialization,
                fee);

            Console.WriteLine("Doctor updated successfully.");
        }

        private void DeleteDoctor()
        {
            Console.Write("Enter Doctor ID: ");
            string id = Console.ReadLine() ?? "";

            doctorService.Delete(id);

            Console.WriteLine("Doctor deleted successfully.");
        }

        // =========================
        // APPOINTMENT MANAGEMENT
        // =========================

        private void ManageAppointments()
        {
            while (true)
            {
                Console.WriteLine("\n-------- APPOINTMENT MANAGEMENT --------");
                Console.WriteLine("1. Add Appointment");
                Console.WriteLine("2. View All Appointments");
                Console.WriteLine("3. View Appointment By ID");
                Console.WriteLine("4. Reschedule Appointment");
                Console.WriteLine("5. Cancel Appointment");
                Console.WriteLine("6. Complete Appointment");
                Console.WriteLine("0. Back");

                Console.Write("Enter choice: ");
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddAppointment();
                            break;

                        case "2":
                            ViewAllAppointments();
                            break;

                        case "3":
                            ViewAppointmentById();
                            break;

                        case "4":
                            RescheduleAppointment();
                            break;

                        case "5":
                            CancelAppointment();
                            break;

                        case "6":
                            CompleteAppointment();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (HospitalException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        private void AddAppointment()
        {
            Console.Write("Enter Appointment ID: ");
            string appointmentId =
                Console.ReadLine() ?? "";

            Console.Write("Enter Patient ID: ");
            string patientId =
                Console.ReadLine() ?? "";

            Console.Write("Enter Doctor ID: ");
            string doctorId =
                Console.ReadLine() ?? "";

            Console.Write("Enter appointment date and time ");
            Console.Write("(yyyy-MM-dd HH:mm): ");

            DateTime dateTime =
                DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Enter Reason: ");
            string reason =
                Console.ReadLine() ?? "";

            Appointment appointment =
                new Appointment(
                    appointmentId,
                    patientId,
                    doctorId,
                    dateTime,
                    reason);

            appointmentService.AddAppointment(
                appointment);

            Console.WriteLine(
                "Appointment added successfully.");
        }

        private void ViewAllAppointments()
        {
            List<Appointment> appointments =
                appointmentService.GetAll();

            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }

            Console.WriteLine(
                "\n-------- ALL APPOINTMENTS --------");

            foreach (Appointment appointment in appointments)
            {
                DisplayAppointment(appointment);
            }
        }

        private void ViewAppointmentById()
        {
            Console.Write("Enter Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            Appointment? appointment =
                appointmentService.GetById(id);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            DisplayAppointment(appointment);
        }

        private void RescheduleAppointment()
        {
            Console.Write("Enter Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Enter new date and time ");
            Console.Write("(yyyy-MM-dd HH:mm): ");

            DateTime dateTime =
                DateTime.Parse(Console.ReadLine() ?? "");

            appointmentService.Reschedule(
                id,
                dateTime);

            Console.WriteLine(
                "Appointment rescheduled successfully.");
        }

        private void CancelAppointment()
        {
            Console.Write("Enter Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            appointmentService.Cancel(id);

            Console.WriteLine(
                "Appointment cancelled successfully.");
        }

        private void CompleteAppointment()
        {
            Console.Write("Enter Appointment ID: ");
            string id = Console.ReadLine() ?? "";

            appointmentService.Complete(id);

            Console.WriteLine(
                "Appointment completed successfully.");
        }

        private void DisplayAppointment(
            Appointment appointment)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine(
                $"Appointment ID: {appointment.AppointmentId}");
            Console.WriteLine(
                $"Patient ID: {appointment.PatientId}");
            Console.WriteLine(
                $"Doctor ID: {appointment.DoctorId}");
            Console.WriteLine(
                $"Date & Time: {appointment.AppointmentDateTime}");
            Console.WriteLine(
                $"Reason: {appointment.Reason}");
            Console.WriteLine(
                $"Status: {appointment.Status}");
            Console.WriteLine("------------------------------------");
        }

        // =========================
        // REPORTS
        // =========================

        private void ShowReports()
        {
            while (true)
            {
                Console.WriteLine("\n--------------- REPORTS ---------------");
                Console.WriteLine("1. Appointments By Status");
                Console.WriteLine("2. Appointments By Date");
                Console.WriteLine("3. Patient Appointments");
                Console.WriteLine("4. Doctor Appointments");
                Console.WriteLine("5. Scheduled Count By Doctor");
                Console.WriteLine("6. Total Revenue");
                Console.WriteLine("7. Top Doctor");
                Console.WriteLine("0. Back");

                Console.Write("Enter choice: ");
                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ReportByStatus();
                            break;

                        case "2":
                            ReportByDate();
                            break;

                        case "3":
                            ReportPatientAppointments();
                            break;

                        case "4":
                            ReportDoctorAppointments();
                            break;

                        case "5":
                            ReportScheduledCount();
                            break;

                        case "6":
                            ReportRevenue();
                            break;

                        case "7":
                            ReportTopDoctor();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Error: {ex.Message}");
                }
            }
        }

        private void ReportByStatus()
        {
            Console.WriteLine("\n1. Scheduled");
            Console.WriteLine("2. Completed");
            Console.WriteLine("3. Cancelled");

            Console.Write("Enter status: ");
            string? choice = Console.ReadLine();

            AppointmentStatus status;

            switch (choice)
            {
                case "1":
                    status = AppointmentStatus.Scheduled;
                    break;

                case "2":
                    status = AppointmentStatus.Completed;
                    break;

                case "3":
                    status = AppointmentStatus.Cancelled;
                    break;

                default:
                    Console.WriteLine("Invalid status.");
                    return;
            }

            List<Appointment> appointments =
                reportService.GetByStatus(status);

            if (appointments.Count == 0)
            {
                Console.WriteLine(
                    "No appointments found.");
                return;
            }

            foreach (Appointment appointment in appointments)
            {
                DisplayAppointment(appointment);
            }
        }

        private void ReportByDate()
        {
            Console.Write("Enter date (yyyy-MM-dd): ");
            DateTime date =
                DateTime.Parse(Console.ReadLine() ?? "");

            List<Appointment> appointments =
                reportService.GetByDate(date);

            if (appointments.Count == 0)
            {
                Console.WriteLine(
                    "No appointments found.");
                return;
            }

            foreach (Appointment appointment in appointments)
            {
                DisplayAppointment(appointment);
            }
        }

        private void ReportPatientAppointments()
        {
            Console.Write("Enter Patient ID: ");
            string patientId =
                Console.ReadLine() ?? "";

            List<Appointment> appointments =
                reportService.GetPatientAppointments(
                    patientId);

            if (appointments.Count == 0)
            {
                Console.WriteLine(
                    "No appointments found.");
                return;
            }

            foreach (Appointment appointment in appointments)
            {
                DisplayAppointment(appointment);
            }
        }

        private void ReportDoctorAppointments()
        {
            Console.Write("Enter Doctor ID: ");
            string doctorId =
                Console.ReadLine() ?? "";

            List<Appointment> appointments =
                reportService.GetDoctorAppointments(
                    doctorId);

            if (appointments.Count == 0)
            {
                Console.WriteLine(
                    "No appointments found.");
                return;
            }

            foreach (Appointment appointment in appointments)
            {
                DisplayAppointment(appointment);
            }
        }

        private void ReportScheduledCount()
        {
            Dictionary<string, int> result =
                reportService.GetScheduledCount();

            if (result.Count == 0)
            {
                Console.WriteLine(
                    "No scheduled appointments.");
                return;
            }

            Console.WriteLine(
                "\n------ SCHEDULED COUNT BY DOCTOR ------");

            foreach (var item in result)
            {
                Console.WriteLine(
                    $"Doctor ID: {item.Key} | " +
                    $"Scheduled: {item.Value}");
            }
        }

        private void ReportRevenue()
        {
            decimal revenue =
                reportService.GetRevenue();

            Console.WriteLine(
                $"Total Revenue: {revenue:C}");
        }

        private void ReportTopDoctor()
        {
            Doctor? doctor =
                reportService.GetTopDoctor();

            if (doctor == null)
            {
                Console.WriteLine(
                    "No completed appointments found.");
                return;
            }

            Console.WriteLine("\n----------- TOP DOCTOR -----------");
            doctor.DisplayInfo();
        }
    }
}