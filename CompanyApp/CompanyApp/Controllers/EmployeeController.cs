using Domain.Entities;
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
        public EmployeeController()
        {
            _employeeService = new EmployeeService();
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
            Console.WriteLine("Enter Employee Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Employee Surname:");
            string surname = Console.ReadLine();

            Console.WriteLine("Enter Employee Age:");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Department Id:");
            int departmentId = int.Parse(Console.ReadLine());

            var employee = new Employee
            {
                Name = name,
                Surname = surname,
                Age = age,
                DepartmentId = departmentId
            };

            await _employeeService.CreateAsync(employee);
            Console.WriteLine("Employee created successfully.");
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

        public async Task GetByDepartmentIdAsync()
        {
            Console.WriteLine("Enter Department Id for Employees:");
            int departmentId = int.Parse(Console.ReadLine());

            var employees = await _employeeService.GetByDepartmentIdAsync(departmentId);
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, DepartmentId: {employee.DepartmentId}");
            }
        }

        public async Task GetByDepartmentNameAsync()
        {
            Console.WriteLine("Enter Department Name for Employees:");
            string departmentName = Console.ReadLine();

            var employees = await _employeeService.GetByDepartmentNameAsync(departmentName);
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, DepartmentId: {employee.DepartmentId}");
            }
        }

        public async Task GetEmployeesCountAsync()
        {
            var count = await _employeeService.GetEmployeesCountAsync();
            Console.WriteLine($"Total count for Emplyess: {count}");
        }

    }
}
