using Domain.Entities;
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
    public class DepartmentController
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController()
        {
            _departmentService = new DepartmentService();
        }

        public async Task CreateAsync()
        {
            try
            {
                Console.WriteLine("Enter department name:");
            Name:
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Department name is required.");
                    goto Name;
                }

                if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                {
                    Console.WriteLine("Department name can only contain letters and spaces. Please try again.");
                    goto Name;
                }

                Console.WriteLine("Enter department capacity:");
            Capacity:
                string capacityInput = Console.ReadLine();
                int capacity;

                if (!int.TryParse(capacityInput, out capacity) || capacity <= 0)
                {
                    Console.WriteLine("Invalid capacity. Please enter a valid number greater than 0.");
                    goto Capacity;
                }

                Department department = new Department
                {
                    Name = name,
                    Capacity = capacity
                };

                await _departmentService.CreateAsync(department);
                Console.WriteLine("Department successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        public async Task DeleteAsync()
        {
            Console.WriteLine("Enter department Id for delete:");
            int id = int.Parse(Console.ReadLine());

            var department = await _departmentService.GetByIdAsync(id);
            if (department != null)
            {
                await _departmentService.DeleteAsync(id);
                Console.WriteLine("Department successfully deleted.");
            }
            else
            {
                Console.WriteLine("Department not found.");
            }
        }

        public async Task GetByIdAsync()
        {
            Console.WriteLine("Enter department Id:");
            int id = int.Parse(Console.ReadLine());

            var department = await _departmentService.GetByIdAsync(id);
            if (department != null)
            {
                Console.WriteLine($"Id: {department.Id}, Name: {department.Name} Capacity: {department.Capacity}");
            }
            else
            {
                Console.WriteLine("Department not found.");
            }
        }

        public async Task GetAllAsync()
        {
            var departments = await _departmentService.GetAllAsync();
            foreach (var department in departments)
            {
                Console.WriteLine($"Id: {department.Id}, Name: {department.Name} , {department.Capacity}");
            }
        }

        public async Task SearchAsync()
        {
            Console.WriteLine("Enter department name for search:");
            string name = Console.ReadLine();

            var departments = await _departmentService.SearchAsync(name);
            if (departments.Any())
            {
                foreach (var department in departments)
                {
                    Console.WriteLine($"Id: {department.Id}, Name: {department.Name}");
                }
            }
            else
            {
                Console.WriteLine("Departments not found with name.");
            }
        }

        public async Task UpdateAsync()
        {
            Console.WriteLine("Enter Department Id for update:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter new Department Name:");
            string newName = Console.ReadLine();

            Console.WriteLine("Enter new Department Capacity:");
            int newCapacity = int.Parse(Console.ReadLine());

            var department = new Department
            {
                Name = newName,
                Capacity = newCapacity
            };

            try
            {
                await _departmentService.UpdateAsync(id, department);
                Console.WriteLine("Department updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }
}
