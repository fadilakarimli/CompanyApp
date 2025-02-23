using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Helpers.Exceptions;
using Repository.Helpers;
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
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address : {employee.Address} , DepartmentId: {employee.DepartmentId}");
            }
        }
        public async Task GetByIdAsync()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter Employee Id:");

                    if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
                    {
                        Console.WriteLine("Invalid Id. Please enter a valid positive number.");
                        continue;
                    }

                    var employee = await _employeeService.GetByIdAsync(id);

                    if (employee == null)
                    {
                        throw new NotFoundException(ResponseMessages.NotFound);
                    }

                    string result = $"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, DepartmentId: {employee.DepartmentId}";
                    Console.WriteLine(result);
                    break;
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                if (!Regex.IsMatch(surname, @"^[a-zA-Z\s]+$"))
                {
                    Console.WriteLine("Employee surname can only contain letters and spaces. Please try again.");
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

                if (!Regex.IsMatch(address, @"^[a-zA-Z0-9\s]+$"))
                {
                    Console.WriteLine("Address can only contain letters, numbers, and spaces. Please try again.");
                    goto Address;
                }

                if (address.All(char.IsDigit))
                {
                    Console.WriteLine("Address cannot be only numbers. Please enter a valid address.");
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
        }
        public async Task DeleteAsync()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();

                Console.WriteLine("EMPLOYEES LIST");
                if (employees != null && employees.Any())
                {
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}");
                    }

                    while (true)
                    {
                        Console.WriteLine("Enter Employee Id to delete:");

                        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
                        {
                            Console.WriteLine("Invalid Id. Please enter a valid positive integer.");
                            continue;
                        }

                        var employeeToDelete = await _employeeService.GetByIdAsync(id);

                        if (employeeToDelete == null)
                        {
                            throw new NotFoundException(ResponseMessages.NotFound);
                        }

                        await _employeeService.DeleteAsync(id);
                        Console.WriteLine("Employee successfully deleted.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("No employees available to delete.");
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong format! Please enter a valid numeric value.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task SearchAsync()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter name or surname to search:");
                    string searchKey = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(searchKey))
                    {
                        var allEmployees = await _employeeService.GetAllAsync();
                        if (allEmployees.Any())
                        {
                            foreach (var emp in allEmployees)
                            {
                                Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Surname: {emp.Surname}, Age: {emp.Age}, DepartmentId: {emp.DepartmentId}");
                            }
                        }
                        else
                        {
                            throw new NotFoundException(ResponseMessages.NotFound);
                        }
                        return;
                    }

                    if (!Regex.IsMatch(searchKey, @"^[a-zA-Z\s]+$"))
                    {
                        Console.WriteLine("Format is wrong. Please enter a valid name or surname.");
                        continue;
                    }

                    var employees = await _employeeService.SearchAsync(searchKey);
                    if (employees.Any())
                    {
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, DepartmentId: {employee.DepartmentId}");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task GetByAgeAsync()
        {
            try
            {
                while (true)
                {
                    var allEmployees = await _employeeService.GetAllAsync();
                    var availableAges = allEmployees.Select(e => e.Age).Distinct().OrderBy(a => a).ToList();

                    if (!availableAges.Any())
                    {
                        throw new NotFoundException("No employees in the database.");
                    }

                    Console.WriteLine("Available Ages:");
                    Console.WriteLine(string.Join(", ", availableAges));

                    Console.WriteLine("Enter Employee Age:");
                    string input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Age cannot be empty.");
                        continue;
                    }
                    if (!int.TryParse(input, out int age) || age <= 0)
                    {
                        Console.WriteLine("Invalid input! Please enter a valid positive number.");
                        continue;
                    }

                    var employees = await _employeeService.GetByAgeAsync(age);
                    if (employees.Any())
                    {
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, DepartmentId: {employee.DepartmentId}");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("No employees found with this age.");
                    }
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task GetAllDepartmentNameAsync()
        {
            try
            {
                var allDepartments = await _departmentService.GetAllAsync();
                if (!allDepartments.Any())
                {
                    throw new NotFoundException("No departments found in the database.");
                }

                Console.WriteLine("Departments:");
                foreach (var dept in allDepartments)
                {
                    Console.WriteLine($"=> {dept.Name}");
                }

                while (true)
                {
                    Console.WriteLine("Enter Department Name for Employees:");
                    string departmentName = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(departmentName))
                    {
                        Console.WriteLine("Department name cannot be empty. Please enter a valid department name.");
                        continue;
                    }

                    if (!Regex.IsMatch(departmentName, @"^[a-zA-Z\s]{2,}$"))
                    {
                        Console.WriteLine("Invalid format! Department name can only contain letters and spaces.");
                        continue;
                    }

                    var employees = await _employeeService.GetAllDepartmentNameAsync(departmentName);
                    if (!employees.Any())
                    {
                        Console.WriteLine("No employees found in this department.");
                        continue;
                    }

                    Console.WriteLine($"Employees in {departmentName} Department:");
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, " +
                            $"Department: {(employee.Department != null ? employee.Department.Name : "NotFound")}");
                    }
                    break;
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task GetByDepartmentIdAsync()
        {
        EnterDepartmentId:
            Console.WriteLine("Enter Department Id:");

            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Department Id cannot be empty. Please enter a valid Id.");
                goto EnterDepartmentId;
            }
            if (!Regex.IsMatch(input, @"^[1-9]\d*$"))
            {
                Console.WriteLine("Invalid format! Please enter a valid positive number for Department Id.");
                goto EnterDepartmentId;
            }
            int departmentId = int.Parse(input);
            var employees = await _employeeService.GetByDepartmentIdAsync(departmentId);
            if (!employees.Any())
            {
                Console.WriteLine("No employees found in this department.");
                goto EnterDepartmentId;
            }
            Console.WriteLine($"Employees in Department Id {departmentId}:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, " +
                    $"Department: {(employee.Department != null ? employee.Department.Name : "Not Found")}");
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
                IDepartmentService departmentService = new DepartmentService();
                var employees = await employeeService.GetAllAsync();
                var departments = await departmentService.GetAllAsync();

                if (employees.Any())
                {
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, DepartmentId: {employee.DepartmentId}");
                    }

                    Console.WriteLine("Enter Employee Id for update:");
                    string idInput = Console.ReadLine();
                    int id;
                    if (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out id))
                    {
                        Console.WriteLine("Invalid Employee Id! Please enter a valid number.");
                        return;
                    }

                    var employeeToEdit = await employeeService.GetByIdAsync(id);
                    if (employeeToEdit == null)
                    {
                        throw new NotFoundException(ResponseMessages.NotFound);
                    }

                }
                else
                {
                    Console.WriteLine("No employees available to edit.");
                }
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
