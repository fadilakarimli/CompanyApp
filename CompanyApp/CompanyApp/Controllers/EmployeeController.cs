using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Controllers
{
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController()
        {
            _employeeService = new EmployeeService();
            _departmentService = new DepartmentService();
        }

        public async Task GetAllAsync()
        {
            var employees = await _employeeService.GetAllAsync();
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, DepartmentId: {employee.DepartmentId}");
            }
        }

        public async Task GetByIdAsync()
        {
            Console.WriteLine("Enter Employee Id:");
            int id = int.Parse(Console.ReadLine());

            var employee = await _employeeService.GetByIdAsync(id);
            if (employee != null)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, DepartmentId: {employee.DepartmentId}");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        public async Task CreateAsync()
        {
            try
            {
                var departments = await _departmentService.GetAllAsync();

                if (departments == null || !departments.Any())
                {
                    Console.WriteLine("No departments found. You must create a department first.");
                    return;
                }

                Console.WriteLine("DEPARTMENTS LIST:");
                foreach (var department in departments)
                {
                    Console.WriteLine($"Id: {department.Id}, Name: {department.Name}");
                }

                Console.WriteLine("Enter Employee Name:");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name is required.");
                    return;
                }

                Console.WriteLine("Enter Employee Surname:");
                string surname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(surname))
                {
                    Console.WriteLine("Surname is required.");
                    return;
                }

                Console.WriteLine("Enter Employee Age:");
                string ageInput = Console.ReadLine();
                int age;

                if (!int.TryParse(ageInput, out age) || age <= 0)
                {
                    Console.WriteLine("Invalid age. Please enter a valid age.");
                    return;
                }

                Console.WriteLine("Enter Employee Address:");
                string address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("Address is required.");
                    return;
                }

                Console.WriteLine("Enter Department Id for the employee:");
                int departmentId;
                Department selectedDepartment = null;

                while (selectedDepartment == null)
                {
                    if (int.TryParse(Console.ReadLine(), out departmentId))
                    {
                        selectedDepartment = departments.FirstOrDefault(d => d.Id == departmentId);

                        if (selectedDepartment == null)
                        {
                            Console.WriteLine("Department Id not found. Please enter a valid Department Id.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Department Id. Please enter a valid Id.");
                    }
                }

                Employee employee = new Employee
                {
                    Name = name,
                    Surname = surname,
                    Age = age,
                    Address = address,
                    DepartmentId = selectedDepartment.Id,
                };

                await _employeeService.CreateAsync(employee);

                Console.WriteLine("Employee successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }






        public async Task DeleteAsync()
        {
            Console.WriteLine("Enter Employee Id for delete:");
            int id = int.Parse(Console.ReadLine());

            await _employeeService.DeleteAsync(id);
            Console.WriteLine("Employee deleted successfully.");
        }

        public async Task SearchAsync()
        {
            Console.WriteLine("Enter name or surname to search:");
            string searchKey = Console.ReadLine();

            var employees = await _employeeService.SearchAsync(searchKey);
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, DepartmentId: {employee.DepartmentId}");
            }
        }

        public async Task GetByAgeAsync()
        {
            Console.WriteLine("Enter Employee Age:");
            int age = int.Parse(Console.ReadLine());

            var employees = await _employeeService.GetByAgeAsync(age);
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, DepartmentId: {employee.DepartmentId}");
            }
        }

        public async Task GetAllDepartmentNameAsync()
        {
            var employees = await _employeeService.GetAllDepartmentNameAsync();
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Department: {employee.Department.Name}");
            }
        }

        public async Task GetEmployeesCountAsync()
        {
            var count = await _employeeService.GetEmployeesCountAsync();
            Console.WriteLine($"Total count for Employees: {count}");
        }

    }
}
