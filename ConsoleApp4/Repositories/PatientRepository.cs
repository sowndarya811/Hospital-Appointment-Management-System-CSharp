using Hospital_Appointment_Management_System.Data;
using Hospital_Appointment_Management_System.Models;
using Hospital_Appointment_Management_System.Services;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Appointment_Management_System.Repositories
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Add(Patient item)
        {
            _context.Set<Patient>().Add(item);
            _context.SaveChanges();
        }

        public List<Patient> GetAll()
        {
            return _context.Set<Patient>()
                .AsNoTracking()
                .ToList();
        }

        public Patient? GetById(string id)
        {
            return _context.Set<Patient>()
                .FirstOrDefault(p => p.PersonId == id);
        }

        public void Update(Patient item)
        {
            _context.Set<Patient>().Update(item);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            Patient? patient = GetById(id);

            if (patient != null)
            {
                _context.Set<Patient>().Remove(patient);
                _context.SaveChanges();
            }
        }
    }
}