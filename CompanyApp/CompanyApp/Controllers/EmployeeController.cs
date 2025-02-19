using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        }//+
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

            Name:
                Console.WriteLine("Enter Employee Name:");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name is required.");
                    goto Name;
                }
                if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                {
                    Console.WriteLine("Employee name can only contain letters and spaces. Please try again.");
                    goto Name;
                }

            Surname:
                Console.WriteLine("Enter Employee Surname:");
                string surname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(surname))
                {
                    Console.WriteLine("Surname is required.");
                    goto Surname;
                }

            Age:
                Console.WriteLine("Enter Employee Age:");
                string ageInput = Console.ReadLine();
                int age;

                if (!int.TryParse(ageInput, out age) || age <= 0)
                {
                    Console.WriteLine("Invalid age. Please enter a valid age.");
                    goto Age;
                }

                if (age < 18)
                {
                    Console.WriteLine("Employee must be at least 18 years old. Please enter a valid age.");
                    goto Age;
                }

                if (age > 65)
                {
                    Console.WriteLine("Employee must be 65 years old or younger. Please enter a valid age.");
                    goto Age;
                }


            Address:
                Console.WriteLine("Enter Employee Address:");
                string address = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("Address is required.");
                    goto Address;
                }

            DepartmentId:
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
                            goto DepartmentId;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Department Id. Please enter a valid Id.");
                        goto DepartmentId;
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
        }//+
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
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}");
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
            Console.WriteLine("Enter Department Name for Employees:");
            string departmentName = Console.ReadLine();

            var employees = await _employeeService.GetAllDepartmentNameAsync(departmentName);

            if (!employees.Any())
            {
                Console.WriteLine("No employees found this department.");
                return;
            }

            Console.WriteLine($"Employees in {departmentName} Department:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, " +
                    $"Department: {(employee.Department != null ? employee.Department.Name : "Not")}");
            }
        }
        public async Task GetByDepartmentIdAsync()
        {
            Console.WriteLine("Enter Department Id:");
            if (!int.TryParse(Console.ReadLine(), out int departmentId))
            {
                Console.WriteLine("Invalid Department Id.:");
                return;
            }

            var employees = await _employeeService.GetByDepartmentIdAsync(departmentId);

            if (!employees.Any())
            {
                Console.WriteLine("No employees found in this department.");
                return;
            }

            Console.WriteLine($"Employees in Department Id {departmentId}:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, " +
                    $"Department: {(employee.Department != null ? employee.Department.Name : "Not")}");
            }
        }
        public async Task GetEmployeesCountAsync()
        {
            var count = await _employeeService.GetEmployeesCountAsync();
            Console.WriteLine($"Total count for Employees: {count}");
        }

        public async Task UpdateAsync()
        {
            try
            {
                Console.WriteLine("EMPLOYEES LIST");

                IEmployeeService employeeService = new EmployeeService();
                var employees = await employeeService.GetAllAsync();

                if (employees.ToList().Count > 0)
                {
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, DepartmentId: {employee.DepartmentId}");
                    }

                    Console.WriteLine("Enter Employee Id for update:");
                    int id;
                    while (true)
                    {
                        string idInput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out id))
                        {
                            Console.WriteLine("Employee Id cannot be empty. Please enter a valid Id.");
                            continue;
                        }

                        var employeeToEdit = await employeeService.GetByIdAsync(id); 

                        if (employeeToEdit == null)
                        {
                            Console.WriteLine("Employee not found! Please enter a valid Id.");
                            continue;
                        }

                        Console.WriteLine("Enter new Name:");
                    Name:
                        string newName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(newName))
                        {
                            newName = employeeToEdit.Name;
                        }
                        else if (!Regex.IsMatch(newName, @"^[a-zA-Z\s]+$"))
                        {
                            Console.WriteLine("Name can only contain letters and spaces. Please try again.");
                            goto Name;
                        }

                        employeeToEdit.Name = newName;

                    Surname:
                        Console.WriteLine("Enter new Surname:");
                        string newSurname = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(newSurname))
                        {
                            newSurname = employeeToEdit.Surname;
                        }

                        employeeToEdit.Surname = newSurname;

                    Age:
                        Console.WriteLine("Enter new Age:");
                        string ageInput = Console.ReadLine();
                        int newAge = employeeToEdit.Age;

                        if (string.IsNullOrWhiteSpace(ageInput))
                        {
                            newAge = employeeToEdit.Age;
                        }
                        else if (int.TryParse(ageInput, out int parsedAge) && parsedAge >= 18 && parsedAge <= 65)
                        {
                            newAge = parsedAge;
                        }
                        else
                        {
                            Console.WriteLine("Age must be between 18 and 65. Please enter a valid age.");
                            goto Age;
                        }

                        employeeToEdit.Age = newAge;



                    Address:
                        Console.WriteLine("Enter new Address:");
                        string newAddress = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(newAddress))
                        {
                            newAddress = employeeToEdit.Address;
                        }

                        employeeToEdit.Address = newAddress;

                    DepartmentId:
                        Console.WriteLine("Enter new Department Id:");
                        string departmentIdInput = Console.ReadLine();
                        int newDepartmentId = employeeToEdit.DepartmentId;

                        if (!string.IsNullOrWhiteSpace(departmentIdInput) && int.TryParse(departmentIdInput, out int parsedDepartmentId))
                        {
                            newDepartmentId = parsedDepartmentId;
                        }

                        employeeToEdit.DepartmentId = newDepartmentId;

                        await employeeService.UpdateAsync(id, employeeToEdit);

                        Console.WriteLine("Employee updated successfully!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("No employees available to edit.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }//+

    }
}
