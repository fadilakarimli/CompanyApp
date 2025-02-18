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
    public class DepartmentController
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController()
        {
            _departmentService = new DepartmentService();
        }

        public async Task CreateAsync()
        {
            Console.WriteLine("Enter department name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter department capacity:");
            int capacity = int.Parse(Console.ReadLine());

            Department department = new Department { Name = name  , Capacity = capacity};

            await _departmentService.CreateAsync(department);
            Console.WriteLine("Department successfully created.");
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



    }
}
