using System;
using System.Collections.Generic;

namespace Hospital_Appointment_Management_System.Models
{
    public class Patient : Person
    {
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }

        // Navigation property
        public ICollection<Appointment> Appointments { get; set; }

        public Patient()
        {
            PhoneNumber = string.Empty;
            Appointments = new List<Appointment>();
        }

        public Patient(
            string patientId,
            string fullName,
            int age,
            string phoneNumber,
            string? email)
            : base(patientId, fullName)
        {
            Age = age;
            PhoneNumber = phoneNumber;
            Email = email;
            Appointments = new List<Appointment>();
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Patient ID: {PersonId}");
            Console.WriteLine($"Patient Name: {FullName}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
            Console.WriteLine($"Email: {Email}");
        }
    }
}