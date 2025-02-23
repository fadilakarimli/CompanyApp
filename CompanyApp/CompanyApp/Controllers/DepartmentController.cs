using Domain.Entities;
using Repository.Helpers;
using Repository.Helpers.Exceptions;
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
                var departments = await _departmentService.GetAllAsync();
                if (departments.Any())
                {
                    Console.WriteLine("EXISTING DEPARTMENTS:");
                    foreach (var item in departments)
                    {
                        Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Capacity: {item.Capacity}");
                    }
                }
                else
                {
                    Console.WriteLine("No existing departments found.");
                }

                Console.WriteLine("Enter Department Name:");
            Name:
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name) || name.Trim() != name)
                {
                    Console.WriteLine("Department name cannot be empty. Cannot start or end with spaces. Please enter a valid name.");
                    goto Name;
                }
                if (!Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
                {
                    Console.WriteLine("Department name can only contain letters and spaces. Please try again.");
                    goto Name;
                }

                var existingDepartment = departments.FirstOrDefault(d => d.Name.ToLower() == name.ToLower());
                if (existingDepartment != null)
                {
                    Console.WriteLine($"A department with the name already exists. Please choose a different name.");
                    goto Name;
                }

                Console.WriteLine("Enter Department Capacity:");
            Capacity:
                string capacityInput = Console.ReadLine();
                int capacity;

                if (!int.TryParse(capacityInput, out capacity) || capacity <= 0)
                {
                    Console.WriteLine("Invalid capacity. Please enter positive number and greater than 0.");
                    goto Capacity;
                }

                name = char.ToUpper(name[0]) + name.Substring(1).ToLower();

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
            try
            {
                var departments = await _departmentService.GetAllAsync();

                Console.WriteLine("DEPARTMENTS LIST");
                if (departments.Any())
                {
                    foreach (var department in departments)
                    {
                        Console.WriteLine($"Id: {department.Id}, Name: {department.Name}, Capacity: {department.Capacity}");
                    }

                DeleteId:
                    Console.WriteLine("Enter Department Id to delete:");
                    string input = Console.ReadLine();
                    int id;

                    if (!int.TryParse(input, out id))
                    {
                        Console.WriteLine("Invalid input! Please enter a valid numeric Id.");
                        goto DeleteId;
                    }

                    try
                    {
                        await _departmentService.DeleteAsync(id);
                        Console.WriteLine("Department successfully deleted.");
                    }
                    catch (NotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto DeleteId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("No departments available to delete.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task GetByIdAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Department Id:");
                    int id;

                    while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
                    {
                        Console.WriteLine("Invalid Id. Please enter a valid positive integer.");
                    }

                    var department = await _departmentService.GetByIdAsync(id);

                    Console.WriteLine($"Id: {department.Id}, Name: {department.Name}, Capacity: {department.Capacity}");
                    break;
                }
                catch (NotFoundException ex)
                {
                    Console.WriteLine(ex.Message);  
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                    break; 
                }
            }
        }
        public async Task GetAllAsync()
        {
            var departments = await _departmentService.GetAllAsync();
            foreach (var department in departments)
            {
                Console.WriteLine($"Id: {department.Id}, Name: {department.Name} , Capacity: {department.Capacity} , DateTime :{department.CreatedDate}");
            }
        }
        public async Task SearchAsync()
        {
            try
            {
            EnterName:
                Console.WriteLine("Enter department name for search:");
                string name = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(name))
                {
                    var allDepartments = await _departmentService.GetAllAsync();
                    if (!allDepartments.Any())
                    {
                        throw new NotFoundException(ResponseMessages.NotFound);
                    }

                    foreach (var dept in allDepartments)
                    {
                        Console.WriteLine($"Id: {dept.Id}, Name: {dept.Name} , Capacity: {dept.Capacity} , DateTime :{dept.CreatedDate}");
                    }
                    return;
                }

                if (int.TryParse(name, out int num) || name.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)))
                {
                    Console.WriteLine("Format is wrong. Please enter department name.");
                    goto EnterName;
                }

                var departments = await _departmentService.SearchAsync(name);
                if (!departments.Any())
                {
                    throw new NotFoundException(ResponseMessages.NotFound);
                }

                foreach (var department in departments)
                {
                    Console.WriteLine($"Id: {department.Id}, Name: {department.Name}");
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
        public async Task UpdateAsync()
        {
            try
            {
                Console.WriteLine("DEPARTMENTS LIST");

                var departments = await _departmentService.GetAllAsync();

                if (!departments.Any())
                {
                    throw new NotFoundException(ResponseMessages.NotFound);
                }

                foreach (var department in departments)
                {
                    Console.WriteLine($"Id: {department.Id}, Name: {department.Name}, Capacity: {department.Capacity}");
                }

                Console.WriteLine("Enter Department Id for update:");
                int id;
                while (true)
                {
                    string idInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(idInput) || !int.TryParse(idInput, out id) || id <= 0)
                    {
                        Console.WriteLine(ResponseMessages.WrongFormat);
                        continue;
                    }

                    var departmentToUpdate = await _departmentService.GetByIdAsync(id);

                    if (departmentToUpdate == null)
                    {
                        throw new NotFoundException(ResponseMessages.NotFound);
                    }

                    string newName;
                    bool isNameExists = false;

                    do
                    {
                    EnterName:
                        Console.WriteLine("Enter new Name:");
                        newName = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(newName))
                        {
                            newName = departmentToUpdate.Name;
                            break;
                        }

                        else if (!Regex.IsMatch(newName, @"^[a-zA-Z\s]+$"))
                        {
                            Console.WriteLine("Name can only contain letters and spaces. Please try again.");
                            break;
                        }
                        else
                        {
                            isNameExists = departments.Any(d =>
                                d.Name.Equals(newName, StringComparison.OrdinalIgnoreCase) && d.Id != departmentToUpdate.Id);

                            if (isNameExists)
                            {
                                Console.WriteLine($"This department name '{newName}' already exists. Please enter a different name.");
                            }
                            else if (departmentToUpdate.Name.Equals(newName, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("You cannot update the department with the same name.");
                                goto EnterName;
                            }
                        }
                    }
                    while (isNameExists);

                    departmentToUpdate.Name = newName;

                    int newCapacity = departmentToUpdate.Capacity;
                    bool isCapacity;
                    do
                    {
                        Console.WriteLine("Enter new Capacity:");
                        string capacityInput = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(capacityInput))
                        {
                            newCapacity = departmentToUpdate.Capacity;
                            isCapacity = true;
                        }
                        else if (int.TryParse(capacityInput, out int convertedCapacity) && convertedCapacity > 0)
                        {
                            newCapacity = convertedCapacity;
                            isCapacity = true;
                        }
                        else
                        {
                            Console.WriteLine("Capacity must be a positive number. Please enter a valid number for capacity.");
                            isCapacity = false;
                        }
                    }
                    while (!isCapacity);

                    departmentToUpdate.Capacity = newCapacity;

                    await _departmentService.UpdateAsync(id, departmentToUpdate);

                    Console.WriteLine("Department updated successfully!");
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
    }
}
