using System;
using System.Collections.Generic;

namespace Hospital_Appointment_Management_System.Models
{
    public class Doctor : Person
    {
        public string Specialization { get; set; }
        public decimal ConsultationFee { get; set; }

        // Navigation property
        public ICollection<Appointment> Appointments { get; set; }

        public Doctor()
        {
            Specialization = string.Empty;
            Appointments = new List<Appointment>();
        }

        public Doctor(
            string doctorId,
            string fullName,
            string specialization,
            decimal consultationFee)
            : base(doctorId, fullName)
        {
            Specialization = specialization;
            ConsultationFee = consultationFee;
            Appointments = new List<Appointment>();
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Doctor ID: {PersonId}");
            Console.WriteLine($"Doctor Name: {FullName}");
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine($"Consultation Fee: {ConsultationFee:C}");
        }
    }
}