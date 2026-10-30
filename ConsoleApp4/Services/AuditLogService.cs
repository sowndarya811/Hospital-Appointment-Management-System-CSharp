using Hospital_Appointment_Management_System.Models;
using Hospital_Appointment_Management_System.Repositories;

namespace Hospital_Appointment_Management_System.Services
{
    public class AuditLogService
    {
        private readonly AuditLogRepository _repository;

        public AuditLogService(AuditLogRepository repository)
        {
            _repository = repository;
        }

        public void Log(
            string action,
            string entityName,
            string entityId,
            string description)
        {
            AuditLog auditLog = new AuditLog(
                action,
                entityName,
                entityId,
                description);

            _repository.Add(auditLog);
        }

        public List<AuditLog> GetAll()
        {
            return _repository.GetAll();
        }
    }
}