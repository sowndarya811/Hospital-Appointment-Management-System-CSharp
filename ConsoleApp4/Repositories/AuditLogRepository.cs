using Hospital_Appointment_Management_System.Data;
using Hospital_Appointment_Management_System.Models;

namespace Hospital_Appointment_Management_System.Repositories
{
    public class AuditLogRepository
    {
        private readonly HospitalDbContext _context;

        public AuditLogRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Add(AuditLog auditLog)
        {
            _context.AuditLogs.Add(auditLog);
            _context.SaveChanges();
        }

        public List<AuditLog> GetAll()
        {
            return _context.AuditLogs
                .OrderByDescending(a => a.Timestamp)
                .ToList();
        }
    }
}