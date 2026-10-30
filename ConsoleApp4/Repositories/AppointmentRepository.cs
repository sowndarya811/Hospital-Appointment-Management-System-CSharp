using Hospital_Appointment_Management_System.Data;
using Hospital_Appointment_Management_System.Models;
using Hospital_Appointment_Management_System.Services;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Appointment_Management_System.Repositories
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private readonly HospitalDbContext _context;

        public AppointmentRepository(HospitalDbContext context)
        {
            _context = context;
        }
            
        public void Add(Appointment item)
        {
            _context.Set<Appointment>().Add(item);
            _context.SaveChanges();
        }

        public List<Appointment> GetAll()
        {
            return _context.Set<Appointment>()
                .AsNoTracking()
                .ToList();
        }

        public Appointment? GetById(string id)
        {
            return _context.Set<Appointment>()
                .FirstOrDefault(a => a.AppointmentId == id);
        }

        public void Update(Appointment item)
        {
            _context.Set<Appointment>().Update(item);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            Appointment? appointment = GetById(id);

            if (appointment != null)
            {
                _context.Set<Appointment>().Remove(appointment);
                _context.SaveChanges();
            }
        }
    }
}