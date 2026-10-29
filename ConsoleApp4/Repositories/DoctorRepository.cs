using Hospital_Appointment_Management_System.Data;
using Hospital_Appointment_Management_System.Models;
using Hospital_Appointment_Management_System.Services;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Appointment_Management_System.Repositories
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly HospitalDbContext _context;

        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Add(Doctor item)
        {
            _context.Set<Doctor>().Add(item);
            _context.SaveChanges();
        }

        public List<Doctor> GetAll()
        {
            return _context.Set<Doctor>()
                .AsNoTracking()
                .ToList();
        }

        public Doctor? GetById(string id)
        {
            return _context.Set<Doctor>()
                .FirstOrDefault(d => d.PersonId == id);
        }

        public void Update(Doctor item)
        {
            _context.Set<Doctor>().Update(item);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            Doctor? doctor = GetById(id);

            if (doctor != null)
            {
                _context.Set<Doctor>().Remove(doctor);
                _context.SaveChanges();
            }
        }
    }
}