namespace Hospital_Appointment_Management_System.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        public string Action { get; set; }

        public string EntityName { get; set; }

        public string EntityId { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }

        public AuditLog()
        {
            Action = string.Empty;
            EntityName = string.Empty;
            EntityId = string.Empty;
            Description = string.Empty;
            Timestamp = DateTime.Now;
        }

        public AuditLog(
            string action,
            string entityName,
            string entityId,
            string description)
        {
            Action = action;
            EntityName = entityName;
            EntityId = entityId;
            Description = description;
            Timestamp = DateTime.Now;
        }
    }
}