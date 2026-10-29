# Hospital Appointment Management System

## Project Overview

The Hospital Appointment Management System is a console-based application developed using C# and .NET.

The application manages patients, doctors, appointments, and reports.

## Technologies Used

- C#
- .NET 10
- Entity Framework Core
- SQL Server
- Git and GitHub

## Main Modules

### Patient Management

- Add Patient
- View Patients
- Search Patient
- Update Patient
- Delete Patient

### Doctor Management

- Add Doctor
- View Doctors
- Search Doctor
- Update Doctor
- Delete Doctor

### Appointment Management

- Create Appointment
- View Appointments
- Reschedule Appointment
- Cancel Appointment
- Complete Appointment

### Reports

- Appointment status
- Appointments by date
- Patient appointments
- Doctor appointments
- Scheduled appointment count
- Revenue
- Top doctor

## Project Architecture

The application follows a layered structure:

- Models
- Repositories
- Services
- Data
- UI
- Exceptions

## Repository Pattern

Repositories are responsible for database operations.

The application uses repository interfaces such as:

- IRepository<Patient>
- IRepository<Doctor>
- IRepository<Appointment>

## Service Layer

Services contain the business logic of the application.

Examples:

- PatientService
- DoctorService
- AppointmentService
- ReportService

## Dependency Injection

Dependency Injection is used to provide required dependencies to services and repositories.

The application registers:

- HospitalDbContext
- Repositories
- Services
- Menu

Dependency Injection reduces tight coupling and makes the application easier to maintain and test.

## Entity Framework Core

Entity Framework Core is used as the ORM for connecting the C# application with SQL Server.

The application uses:

- DbContext
- DbSet
- Relationships
- Migrations

## Database

The application uses SQL Server database:

HospitalAppointmentDb

Main tables include:

- Person
- Appointments
- EF Migrations History

## Exception Handling

A custom HospitalException class is used to handle business validation errors.

Examples:

- Duplicate ID
- Invalid patient age
- Invalid consultation fee
- Invalid appointment date
- Appointment not found