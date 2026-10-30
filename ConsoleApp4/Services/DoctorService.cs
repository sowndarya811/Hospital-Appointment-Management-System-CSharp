using Hospital_Appointment_Management_System.Exceptions;
using Hospital_Appointment_Management_System.Models;

namespace Hospital_Appointment_Management_System.Services
{
    public class DoctorService
    {
        private readonly IRepository<Doctor> _repository;
        private readonly AuditLogService _auditLogService;

        public DoctorService(
            IRepository<Doctor> repository,
            AuditLogService auditLogService)
        {
            _repository = repository;
            _auditLogService = auditLogService;
        }

        public void AddDoctor(Doctor doctor)
        {
            if (doctor == null)
                throw new HospitalException("Doctor cannot be null.");

            if (string.IsNullOrWhiteSpace(doctor.PersonId))
                throw new HospitalException("Doctor ID is required.");

            if (string.IsNullOrWhiteSpace(doctor.FullName))
                throw new HospitalException("Doctor name is required.");

            if (string.IsNullOrWhiteSpace(doctor.Specialization))
                throw new HospitalException(
                    "Doctor specialization is required.");

            if (doctor.ConsultationFee < 0)
                throw new HospitalException(
                    "Consultation fee cannot be negative.");

            if (_repository.GetById(doctor.PersonId) != null)
                throw new HospitalException(
                    $"Doctor with ID {doctor.PersonId} already exists.");

            _repository.Add(doctor);

            _auditLogService.Log(
                "CREATE",
                "Doctor",
                doctor.PersonId,
                $"Doctor {doctor.FullName} was created.");
        }

        // IMPORTANT: Menu.cs uses GetAll()
        public List<Doctor> GetAll()
        {
            return _repository.GetAll();
        }

        // IMPORTANT: Menu.cs uses Search()
        public List<Doctor> Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Doctor>();

            return _repository
                .GetAll()
                .Where(d =>
                    d.FullName.Contains(
                        searchText,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    d.Specialization.Contains(
                        searchText,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // IMPORTANT: Menu.cs uses Update()
        public void Update(
            string id,
            string name,
            string specialization,
            decimal consultationFee)
        {
            Doctor? existingDoctor = _repository.GetById(id);

            if (existingDoctor == null)
                throw new HospitalException(
                    $"Doctor with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(name))
                throw new HospitalException("Doctor name is required.");

            if (string.IsNullOrWhiteSpace(specialization))
                throw new HospitalException(
                    "Doctor specialization is required.");

            if (consultationFee < 0)
                throw new HospitalException(
                    "Consultation fee cannot be negative.");

            existingDoctor.FullName = name;
            existingDoctor.Specialization = specialization;
            existingDoctor.ConsultationFee = consultationFee;

            _repository.Update(existingDoctor);

            _auditLogService.Log(
                "UPDATE",
                "Doctor",
                existingDoctor.PersonId,
                $"Doctor {existingDoctor.FullName} was updated.");
        }

        // IMPORTANT: Menu.cs uses Delete()
        public void Delete(string id)
        {
            Doctor? doctor = _repository.GetById(id);

            if (doctor == null)
                throw new HospitalException(
                    $"Doctor with ID {id} not found.");

            _repository.Delete(id);

            _auditLogService.Log(
                "DELETE",
                "Doctor",
                id,
                $"Doctor {doctor.FullName} was deleted.");
        }
    }
}