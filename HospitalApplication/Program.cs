using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Hospital
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cn = new SqlConnection("Data Source=LAPTOP-TJ920A78;Initial Catalog=Hospital;Integrated Security=True");
            cn.Open();
            Program p = new Program();
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
            cn.Close();
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Add Patient details");
            Console.WriteLine("2) Book Appointment");
            Console.WriteLine("3) Add Prescription: ");
            Console.WriteLine("4) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddPatient();
                    return true;
                case "2":
                    BookAppointment();
                    return true;
                case "3":
                    DoctorPrescription();
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }

        private static void AddPatient()
        {
            using (var database = new Model1())
            {
                var patient = new Patient();
                Console.WriteLine($"Enter your name:" + patient.PatientName);
                patient.PatientName = Console.ReadLine();

                Console.WriteLine($"Enter your mobile number: " + patient.MobileNumber);
                patient.MobileNumber = Console.ReadLine();

                Console.WriteLine($"Enter your Address: " + patient.Address);
                patient.Address = Console.ReadLine();

                Console.WriteLine($"Enter your Age: " + patient.Age);
                patient.Age = Convert.ToInt32(Console.ReadLine());

                var patient1 = database.Patients.SingleOrDefault(t => t.PatientName == patient.PatientName);
                if (patient1 == null)
                {
                    database.Patients.Add(patient);
                    database.SaveChanges();
                    Console.WriteLine("Patient added to the list");
                }
                else
                {
                    Console.WriteLine("There is already such patient in the list");
                    throw new DuplicateWaitObjectException(nameof(patient.PatientName), "There is already such patient  in the list");
                }

            }
        }

        private static void BookAppointment()
        {
            using (var database = new Model1())
            {
                var appointment = new Appointment();
                var patientlist = database.Patients;
                {
                    foreach (var pat in patientlist)
                    {
                        Console.WriteLine($"List of Patient - {pat.PatientId} - {pat.PatientName}");
                    }
                }
                Console.WriteLine($"Enter your Id:" + appointment.PatientId);
                appointment.PatientId = Convert.ToInt32(Console.ReadLine());
                var patient = database.Patients.SingleOrDefault(t => t.PatientId == appointment.PatientId);
                if (patient == null)
                {
                    //throw new ArgumentNullException("There is no such patient");
                    Console.WriteLine("There is no such patient");
                    Console.WriteLine("Please Enter your details");
                    AddPatient();
                }

                var doctorlist = database.Doctors;
                {
                    foreach (var doc in doctorlist)
                    {
                        Console.WriteLine($"List of Doctors - {doc.DoctorId} - {doc.DoctorName}");
                    }
                }
                Console.WriteLine($"Enter Doctor Id: " + appointment.DoctorId);
                appointment.DoctorId = Convert.ToInt32(Console.ReadLine());
                var doctor = database.Doctors.SingleOrDefault(t => t.DoctorId == appointment.DoctorId);
                if (doctor == null)
                {
                    //throw new ArgumentNullException("There is no such doctor");
                    Console.WriteLine("There is no such doctor");
                    Console.WriteLine("Please Enter valid details");
                    
                }

                Console.WriteLine($"Enter Date: " + appointment.Date);
                appointment.Date = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine($"Enter Time: " + appointment.Time);
                appointment.Time = TimeSpan.Parse(Console.ReadLine());

                var apt = database.Appointments.SingleOrDefault(t => t.PatientId == appointment.PatientId);
                if (apt == null)
                {
                    database.Appointments.Add(appointment);
                    database.SaveChanges();
                    Console.WriteLine("Appointment booked");
                }
                else
                {
                    Console.WriteLine("There is already such booking");
                    throw new DuplicateWaitObjectException(nameof(appointment.PatientId), "There is already such booking");
                }

            }
        }

        private static void DoctorPrescription()
        {
            using (var database = new Model1())
            {
                var prescription = new Prescription();

                var patientlist = database.Patients;
                {
                    foreach (var pat in patientlist)
                    {
                        Console.WriteLine($"List of Patient - {pat.PatientId} - {pat.PatientName}");
                    }
                }
                Console.WriteLine($"Enter Patient Id:" + prescription.PatientId);
                prescription.PatientId = Convert.ToInt32(Console.ReadLine());
                var patient = database.Patients.SingleOrDefault(t => t.PatientId == prescription.PatientId);
                if (patient == null)
                {
                    //throw new ArgumentNullException("There is no such patient");
                    Console.WriteLine("There is no such patient");
                    Console.WriteLine("Please Enter valid details");
                    
                }

                var doctorlist = database.Doctors;
                {
                    foreach (var doc in doctorlist)
                    {
                        Console.WriteLine($"List of Doctors - {doc.DoctorId} - {doc.DoctorName}");
                    }
                }
                Console.WriteLine($"Enter Doctor Id: " + prescription.DoctorId);
                prescription.DoctorId = Convert.ToInt32(Console.ReadLine());
                var doctor = database.Doctors.SingleOrDefault(t => t.DoctorId == prescription.DoctorId);
                if (doctor == null)
                {
                    //throw new ArgumentNullException("There is no such doctor");
                    Console.WriteLine("There is no such doctor");
                    Console.WriteLine("Please Enter valid details");

                }

                var druglist = database.Drugs;
                {
                    foreach (var dg in druglist)
                    {
                        Console.WriteLine($"List of Drugs - {dg.DrugId} - {dg.DrugName}");
                    }
                }
                Console.WriteLine($"Enter Drug Id:" + prescription.DrugId);
                prescription.DrugId = Convert.ToInt32(Console.ReadLine());
                var drug = database.Drugs.SingleOrDefault(t => t.DrugId == prescription.DrugId);
                if (drug == null)
                {
                    //throw new ArgumentNullException("There is no such drug");
                    Console.WriteLine("There is no such drug");
                    Console.WriteLine("Please Enter valid details");

                }

                var doselist = database.Doses;
                {
                    foreach (var ds in doselist)
                    {
                        Console.WriteLine($"List of Doses - {ds.DoseId} - {ds.Time}");
                    }
                }
                Console.WriteLine($"Enter Dose Id: " + prescription.DoseId);
                prescription.DoseId = Convert.ToInt32(Console.ReadLine());
                var dose = database.Doses.SingleOrDefault(t => t.DoseId == prescription.DoseId);
                if (dose == null)
                {
                    //throw new ArgumentNullException("There is no such dose");
                    Console.WriteLine("There is no such dose");
                    Console.WriteLine("Please Enter valid details");

                }

                var pres = database.Prescriptions.SingleOrDefault(t => t.DrugId == prescription.DrugId);
                if (pres == null)
                {
                    database.Prescriptions.Add(prescription);
                    database.SaveChanges();
                    Console.WriteLine("Prescribed to patient");
                }
                else
                {
                    Console.WriteLine("There is already such prescription");
                    throw new DuplicateWaitObjectException(nameof(prescription.DrugId), "There is already such prescription");
                }

            }
        }
    }
}
