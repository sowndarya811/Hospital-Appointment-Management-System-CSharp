using Hospital_Appointment_Management_System.Exceptions;
using Hospital_Appointment_Management_System.Models;

namespace Hospital_Appointment_Management_System.Services
{
    public class AppointmentService
    {
        private readonly IRepository<Appointment> _repository;
        private readonly AuditLogService _auditLogService;

        public AppointmentService(
            IRepository<Appointment> repository,
            AuditLogService auditLogService)
        {
            _repository = repository;
            _auditLogService = auditLogService;
        }

        // Add Appointment
        public void AddAppointment(Appointment appointment)
        {
            if (appointment == null)
                throw new HospitalException(
                    "Appointment cannot be null.");

            if (string.IsNullOrWhiteSpace(appointment.AppointmentId))
                throw new HospitalException(
                    "Appointment ID is required.");

            if (string.IsNullOrWhiteSpace(appointment.PatientId))
                throw new HospitalException(
                    "Patient ID is required.");

            if (string.IsNullOrWhiteSpace(appointment.DoctorId))
                throw new HospitalException(
                    "Doctor ID is required.");

            if (appointment.AppointmentDateTime <= DateTime.Now)
                throw new HospitalException(
                    "Appointment date and time must be in the future.");

            if (string.IsNullOrWhiteSpace(appointment.Reason))
                throw new HospitalException(
                    "Appointment reason is required.");

            if (_repository.GetById(appointment.AppointmentId) != null)
                throw new HospitalException(
                    $"Appointment with ID {appointment.AppointmentId} already exists.");

            appointment.Status = AppointmentStatus.Scheduled;

            _repository.Add(appointment);

            _auditLogService.Log(
                "CREATE",
                "Appointment",
                appointment.AppointmentId,
                $"Appointment {appointment.AppointmentId} was created.");
        }

        // IMPORTANT: Menu.cs uses GetAll()
        public List<Appointment> GetAll()
        {
            return _repository.GetAll();
        }

        // IMPORTANT: Menu.cs uses GetById()
        public Appointment? GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new HospitalException(
                    "Appointment ID is required.");

            return _repository.GetById(id);
        }

        // IMPORTANT: Menu.cs uses Reschedule()
        public void Reschedule(
            string id,
            DateTime newDateTime)
        {
            Appointment? appointment = _repository.GetById(id);

            if (appointment == null)
                throw new HospitalException(
                    $"Appointment with ID {id} not found.");

            if (appointment.Status != AppointmentStatus.Scheduled)
                throw new HospitalException(
                    "Only scheduled appointments can be rescheduled.");

            if (newDateTime <= DateTime.Now)
                throw new HospitalException(
                    "New appointment date and time must be in the future.");

            appointment.AppointmentDateTime = newDateTime;

            _repository.Update(appointment);

            _auditLogService.Log(
                "RESCHEDULE",
                "Appointment",
                appointment.AppointmentId,
                $"Appointment {appointment.AppointmentId} was rescheduled to {newDateTime}.");
        }

        // IMPORTANT: Menu.cs uses Cancel()
        public void Cancel(string id)
        {
            Appointment? appointment = _repository.GetById(id);

            if (appointment == null)
                throw new HospitalException(
                    $"Appointment with ID {id} not found.");

            if (appointment.Status != AppointmentStatus.Scheduled)
                throw new HospitalException(
                    "Only scheduled appointments can be cancelled.");

            appointment.Status = AppointmentStatus.Cancelled;

            _repository.Update(appointment);

            _auditLogService.Log(
                "CANCEL",
                "Appointment",
                appointment.AppointmentId,
                $"Appointment {appointment.AppointmentId} was cancelled.");
        }

        // IMPORTANT: Menu.cs uses Complete()
        public void Complete(string id)
        {
            Appointment? appointment = _repository.GetById(id);

            if (appointment == null)
                throw new HospitalException(
                    $"Appointment with ID {id} not found.");

            if (appointment.Status != AppointmentStatus.Scheduled)
                throw new HospitalException(
                    "Only scheduled appointments can be completed.");

            appointment.Status = AppointmentStatus.Completed;

            _repository.Update(appointment);

            _auditLogService.Log(
                "COMPLETE",
                "Appointment",
                appointment.AppointmentId,
                $"Appointment {appointment.AppointmentId} was completed.");
        }
    }
}