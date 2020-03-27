using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            int choice;
            do
            {
                Console.WriteLine("Select From Following Options");
                Console.WriteLine("You are 1. Admin 2.Employee 3.Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        p.Admin();
                        break;
                    case 2:
                        p.Employee();
                        break;
                    case 3:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
            while (choice != 3);
        }

        public void Admin()
        {
            int choice;
            string Name, Password;
            Console.WriteLine("Enter your name: ");
            Name = Console.ReadLine();
            Console.WriteLine("Enter Password: ");
            Password = Console.ReadLine();
            if (Name == "admin" && Password == "123456")
            {
                do
                {
                    Console.WriteLine("Select From Following Options");
                    Console.WriteLine("1.Add Business Unit");
                    Console.WriteLine("2.Add Project");
                    Console.WriteLine("3.Assign Project to employees");
                    Console.WriteLine("4.Assign Project Manger");
                    Console.WriteLine("5.View Projects with Manager");
                    Console.WriteLine("6.Remove Business Unit");
                    Console.WriteLine("7.Remove Project");
                    Console.WriteLine("8.Remove Employee from Project");
                    Console.WriteLine("9.Remove Project Manager");
                    Console.WriteLine("10.Exit");
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddBusinessUnit();
                            break;
                        case 2:
                            AddProject();
                            break;
                        case 3:
                            AssignProject();
                            break;
                        case 4:
                            AssignProjectManager();
                            break;
                        case 5:
                            ViewProjectManager();
                            break;
                        case 6:
                            RemoveBusinessUnit();
                            break;
                        case 7:
                            RemoveProject();
                            break;
                        case 8:
                            RemoveEmployeeFromProject();
                            break;
                        case 9:
                            RemoveProjectManager();
                            break;
                        case 10:
                            Console.WriteLine("Exit");
                            break;
                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                while (choice != 10);
            }
            else
            {
                Console.WriteLine("Invalid Credentials");
            }
        }

        public void AddBusinessUnit()
        {
            using (var db = new CompanyDbEntities())
            {
                var businessunit = new BusinessUnit();

                Console.WriteLine("Enter Business Unit Name: " + businessunit.BusinessUnitName);
                businessunit.BusinessUnitName = Console.ReadLine();

                var bu = db.BusinessUnits.SingleOrDefault(t => t.BusinessUnitName == businessunit.BusinessUnitName);
                if (bu == null)
                {
                    db.BusinessUnits.Add(businessunit);
                    db.SaveChanges();
                    Console.WriteLine("Business Unit Added");
                }
                else
                {
                    Console.WriteLine("There is already such unit present");
                }
            }
        }

        public void AddProject()
        {
            using (var db = new CompanyDbEntities())
            {
                var project = new Project();

                Console.WriteLine("Enter Project Name: " + project.ProjectName);
                project.ProjectName = Console.ReadLine();

                var p = db.Projects.SingleOrDefault(t => t.ProjectName == project.ProjectName);
                if (p != null)
                {
                    Console.WriteLine("There is already such project present");
                }
                else
                {
                    var bulist = db.BusinessUnits;
                    {
                        foreach (var bu in bulist)
                        {
                            Console.WriteLine($"List of Business Units - {bu.BusinessUnitId} - {bu.BusinessUnitName}");
                        }
                    }
                    Console.WriteLine($"Enter Business Unit Id from above list:" + project.BusinessUnitId);
                    project.BusinessUnitId = Convert.ToInt32(Console.ReadLine());

                    var bl = db.BusinessUnits.SingleOrDefault(t => t.BusinessUnitId == project.BusinessUnitId);
                    if (bl == null)
                    {
                        Console.WriteLine("There is no such business unit");
                        Console.WriteLine("Please Enter valid details");
                    }
                    db.Projects.Add(project);
                    db.SaveChanges();
                    Console.WriteLine("Project Added");
                }
            }
        }

        public void AssignProject()
        {
            using (var db = new CompanyDbEntities())
            {
                int projectId;
                int employeeId;

                var projemp = new Project_Employees();
                var project = new Project();
                var emp = new Employee();

                Console.WriteLine("Enter Project Name: " + project.ProjectName);
                project.ProjectName = Console.ReadLine();

                var pn = db.Projects.SingleOrDefault(t => t.ProjectName == project.ProjectName);
                projectId = pn.ProjectId;
                projemp.ProjectId = projectId;

                if (pn != null)
                {
                    Console.WriteLine("Enter Employee Name:" + emp.EmployeeName);
                    emp.EmployeeName = Console.ReadLine();

                    var en = db.Employees.SingleOrDefault(t => t.EmployeeName == emp.EmployeeName);
                    employeeId = en.EmployeeId;
                    projemp.EmployeeId = employeeId;

                    if (en != null)
                    {
                        db.Project_Employees.Add(projemp);
                        db.SaveChanges();
                        Console.WriteLine("Project assigned to employee");
                    }
                    else
                    {
                        Console.WriteLine("There is no such employee");
                    }
                }
                else
                {
                    Console.WriteLine("There is no such project");
                    Console.WriteLine("Enter valid name");
                }

            }
        }

        public void AssignProjectManager()
        {
            using (var db = new CompanyDbEntities())
            {
                int projectId;
                int projectemployeeId;
                int employeeId;

                var projectmanager = new ProjectManager();
                var project = new Project();
                var projectemployee = new Project_Employees();

                Console.WriteLine("Enter Project Name: " + project.ProjectName);
                project.ProjectName = Console.ReadLine();

                var pn = db.Projects.SingleOrDefault(t => t.ProjectName == project.ProjectName);
                projectId = pn.ProjectId;
                projectmanager.ProjectId = projectId;

                if (pn != null)
                {
                    var emp = new Employee();
                    Console.WriteLine("Enter employee name: " + emp.EmployeeName);
                    emp.EmployeeName = Console.ReadLine();

                    var en = db.Employees.SingleOrDefault(t => t.EmployeeName == emp.EmployeeName);
                    employeeId = en.EmployeeId;

                    if (en != null)
                    {
                        var e = db.Project_Employees.SingleOrDefault(t => t.EmployeeId == employeeId);
                        projectemployeeId = e.ProjectEmployeeId;
                        if (e != null)
                        {
                            projectmanager.ProjectEmployeeId = projectemployeeId;
                            db.ProjectManagers.Add(projectmanager);
                            db.SaveChanges();
                            Console.WriteLine("Project manager is successfully assigned");
                        }
                        else
                        {
                            Console.WriteLine("No project is assign to this employee");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no such employee!");
                    }
                }
                else
                {
                    Console.WriteLine("There is no such project");
                    Console.WriteLine("Enter valid name");
                }
            }
        }

        public void ViewProjectManager()
        {
            using (var db = new CompanyDbEntities())
            {
                var projectmanager = db.vProjectManagers;
                Console.WriteLine("Detail of project with manager");
                foreach (var list in projectmanager)
                {
                    Console.WriteLine($"Name- {list.EmployeeName}    Project Name - {list.ProjectName}    Business Unit - {list.BusinessUnitName}");
                }
            }
        }

        public void RemoveBusinessUnit()
        {
            using (var db = new CompanyDbEntities())
            {
                var businessunit = new BusinessUnit();
                Console.WriteLine("Enter name of business unit you want to remove:" + businessunit.BusinessUnitName);
                businessunit.BusinessUnitName = Console.ReadLine();

                var bu = db.BusinessUnits.SingleOrDefault(t => t.BusinessUnitName == businessunit.BusinessUnitName);
                if (bu != null)
                {
                    db.BusinessUnits.Remove(db.BusinessUnits.Single(t => t.BusinessUnitName == businessunit.BusinessUnitName));
                    db.SaveChanges();
                    Console.WriteLine("Business Unit Removed");
                }
                else
                {
                    Console.WriteLine("There is no such unit present");
                }


            }
        }

        public void RemoveProject()
        {
            using (var db = new CompanyDbEntities())
            {
                var project = new Project();

                Console.WriteLine("Enter Project Name: " + project.ProjectName);
                project.ProjectName = Console.ReadLine();

                var p = db.Projects.SingleOrDefault(t => t.ProjectName == project.ProjectName);
                if (p != null)
                {
                    db.Projects.Remove(db.Projects.Single(t => t.ProjectName == project.ProjectName));
                    db.SaveChanges();
                    Console.WriteLine("Project Unit Removed");
                }
                else
                {
                    Console.WriteLine("There is no such project present");
                }
            }

        }

        public void RemoveEmployeeFromProject()
        {
            using (var db = new CompanyDbEntities())
            {
                int employeeId;

                var projemp = new Project_Employees();
                var emp = new Employee();

                Console.WriteLine("Enter Employee Name:" + emp.EmployeeName);
                emp.EmployeeName = Console.ReadLine();
                var en = db.Employees.SingleOrDefault(t => t.EmployeeName == emp.EmployeeName);
                employeeId = en.EmployeeId;
                projemp.EmployeeId = employeeId;

                if (en != null)
                {
                    db.Project_Employees.Remove(db.Project_Employees.Single(t => t.EmployeeId == employeeId));
                    db.SaveChanges();
                    Console.WriteLine("Employee removed from assigned project");
                }
                else
                {
                    Console.WriteLine("No project is assign to employee");
                }

            }

        }

        public void RemoveProjectManager()
        {
            using (var db = new CompanyDbEntities())
            {
                int projectId;
                var projectmanager = new ProjectManager();
                var project = new Project();

                Console.WriteLine("Enter Project Name: " + project.ProjectName);
                project.ProjectName = Console.ReadLine();

                var pn = db.Projects.SingleOrDefault(t => t.ProjectName == project.ProjectName);
                projectId = pn.ProjectId;
                projectmanager.ProjectId = projectId;

                if (pn != null)
                {
                    db.ProjectManagers.Remove(db.ProjectManagers.Single(t => t.ProjectId == projectId));
                    db.SaveChanges();
                    Console.WriteLine("Project manager is successfully removed");
                }
                else
                {
                    Console.WriteLine("There is no such project");
                    Console.WriteLine("Enter valid name");
                }
            }
        }

        public void Employee()
        {
            int choice;
            do
            {
                Console.WriteLine("Select From Following Options");
                Console.WriteLine("1. New Employee ");
                Console.WriteLine("2.Existing Employee ");
                Console.WriteLine("3.Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddEmployee();
                        break;
                    case 2:
                        ExistingEmployee();
                        break;
                    case 3:
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
            while (choice != 3);
        }

        public void AddEmployee()
        {
            using (var db = new CompanyDbEntities())
            {
                var employee = new Employee();
                Console.WriteLine("Enter Your Name: " + employee.EmployeeName);
                employee.EmployeeName = Console.ReadLine();

                var emp = db.Employees.SingleOrDefault(t => t.EmployeeName == employee.EmployeeName);
                if (emp != null)
                {
                    Console.WriteLine("There is already such employee present");
                }
                else
                {
                    var bulist = db.BusinessUnits;
                    {
                        foreach (var bu in bulist)
                        {
                            Console.WriteLine($"List of Business Units - {bu.BusinessUnitId} - {bu.BusinessUnitName}");
                        }
                    }
                    Console.WriteLine($"Enter Business Unit Id from above list:" + employee.BusinessUnitId);
                    employee.BusinessUnitId = Convert.ToInt32(Console.ReadLine());

                    var bl = db.BusinessUnits.SingleOrDefault(t => t.BusinessUnitId == employee.BusinessUnitId);
                    if (bl == null)
                    {
                        Console.WriteLine("There is no such business unit");
                        Console.WriteLine("Please Enter valid details");
                    }
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    Console.WriteLine("Employee Added");
                }

            }
        }

        public void ExistingEmployee()
        {
            using (var db = new CompanyDbEntities())
            {
                int employeeId;
                var employee = new Employee();
                Console.WriteLine("Enter Your Name:" + employee.EmployeeName);
                employee.EmployeeName = Console.ReadLine();

                var name = db.Employees.SingleOrDefault(t => t.EmployeeName == employee.EmployeeName);
                employeeId = employee.EmployeeId;

                if (name != null)
                {
                    var employeedetails = db.vEmployees.Where(t => t.EmployeeName == employee.EmployeeName);
                    foreach (var list in employeedetails)
                    {
                        Console.WriteLine("Your Details are as follows:");
                        Console.WriteLine($"Name- {list.EmployeeName}     Project Name - {list.ProjectName}     Business Unit - {list.BusinessUnitName}");
                    }

                }
                else
                {
                    Console.WriteLine("You are not registered");
                    Console.WriteLine("Please register yourself");
                    AddEmployee();
                }
            }
        }


    }
}
