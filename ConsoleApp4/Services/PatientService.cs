using Hospital_Appointment_Management_System.Exceptions;
using Hospital_Appointment_Management_System.Models;

namespace Hospital_Appointment_Management_System.Services
{
    public class PatientService
    {
        private readonly IRepository<Patient> _repository;
        private readonly AuditLogService _auditLogService;

        public PatientService(
            IRepository<Patient> repository,
            AuditLogService auditLogService)
        {
            _repository = repository;
            _auditLogService = auditLogService;
        }

        public void AddPatient(Patient patient)
        {
            if (patient == null)
                throw new HospitalException("Patient cannot be null.");

            if (string.IsNullOrWhiteSpace(patient.PersonId))
                throw new HospitalException("Patient ID is required.");

            if (string.IsNullOrWhiteSpace(patient.FullName))
                throw new HospitalException("Patient name is required.");

            if (patient.Age < 0 || patient.Age > 120)
                throw new HospitalException(
                    "Patient age must be between 0 and 120.");

            if (string.IsNullOrWhiteSpace(patient.PhoneNumber))
                throw new HospitalException("Phone number is required.");

            if (_repository.GetById(patient.PersonId) != null)
                throw new HospitalException(
                    $"Patient with ID {patient.PersonId} already exists.");

            _repository.Add(patient);

            _auditLogService.Log(
                "CREATE",
                "Patient",
                patient.PersonId,
                $"Patient {patient.FullName} was created.");
        }

        public List<Patient> GetAllPatients()
        {
            return _repository.GetAll();
        }

        public Patient? GetPatientById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new HospitalException("Patient ID is required.");

            return _repository.GetById(id);
        }

        public List<Patient> SearchPatients(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Patient>();

            return _repository
                .GetAll()
                .Where(p =>
                    p.FullName.Contains(
                        searchText,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void UpdatePatient(
            string id,
            string name,
            int age,
            string phone,
            string? email)
        {
            Patient? existingPatient = _repository.GetById(id);

            if (existingPatient == null)
                throw new HospitalException(
                    $"Patient with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(name))
                throw new HospitalException("Patient name is required.");

            if (age < 0 || age > 120)
                throw new HospitalException(
                    "Patient age must be between 0 and 120.");

            if (string.IsNullOrWhiteSpace(phone))
                throw new HospitalException("Phone number is required.");

            existingPatient.FullName = name;
            existingPatient.Age = age;
            existingPatient.PhoneNumber = phone;
            existingPatient.Email = email;

            _repository.Update(existingPatient);

            _auditLogService.Log(
                "UPDATE",
                "Patient",
                existingPatient.PersonId,
                $"Patient {existingPatient.FullName} was updated.");
        }

        public void DeletePatient(string id)
        {
            Patient? patient = _repository.GetById(id);

            if (patient == null)
                throw new HospitalException(
                    $"Patient with ID {id} not found.");

            _repository.Delete(id);

            _auditLogService.Log(
                "DELETE",
                "Patient",
                id,
                $"Patient {patient.FullName} was deleted.");
        }
    }
}