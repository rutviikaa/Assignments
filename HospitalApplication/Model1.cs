namespace Hospital
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Assistant> Assistants { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public virtual DbSet<Dos> Doses { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<PatientDetail> PatientDetails { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assistant>()
                .Property(e => e.AssistantName)
                .IsUnicode(false);

            modelBuilder.Entity<Assistant>()
                .HasMany(e => e.PatientDetails)
                .WithRequired(e => e.Assistant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Assistants)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Doctors)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.DoctorName)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.Doctor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.DoctorSchedules)
                .WithRequired(e => e.Doctor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Prescriptions)
                .WithRequired(e => e.Doctor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Patients)
                .WithMany(e => e.Doctors)
                .Map(m => m.ToTable("Doctor_Patient").MapLeftKey("DoctorId").MapRightKey("PatientId"));

            modelBuilder.Entity<Dos>()
                .Property(e => e.Time)
                .IsUnicode(false);

            modelBuilder.Entity<Dos>()
                .HasMany(e => e.Prescriptions)
                .WithRequired(e => e.Dos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drug>()
                .Property(e => e.DrugName)
                .IsUnicode(false);

            modelBuilder.Entity<Drug>()
                .HasMany(e => e.Prescriptions)
                .WithRequired(e => e.Drug)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.PatientName)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Appointments)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.PatientDetails)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Prescriptions)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);
        }
    }
}
