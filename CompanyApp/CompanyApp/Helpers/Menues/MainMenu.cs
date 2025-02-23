using CompanyApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Helpers.Menues
{
    public class MainMenu
    {
        private readonly UserController _userController;
        private readonly EmployeeController _employeeController;
        private readonly DepartmentController _departmentController;

        public MainMenu()
        {
            _userController = new UserController();
            _employeeController = new EmployeeController();
            _departmentController = new DepartmentController();
        }

        public async Task Start()
        {
            while (true)
            {
                Console.WriteLine("Choose one operation: 1 - Create Department , 2 - GetAll Department , 3 - GetById Department , 4 - Delete Department," +
                       "5 - SearchByNameDepartment, 6 - Update Department , 7 - Create Employee, 8 - GetAll Employee , 9 - Update Employee , 10 - GetById Employee," +
                       "11 - Delete Employee , 12 - GetEmployeesByDepartmentId , 13 - GetAllEmployeesByDepartmentName, 14 - SearchForEmployeesByNameOrSurname," +
                      "15 - GetAllEmployeesCount , 16 - GetEmployeesByAge , 17 -LogOut");

                Console.WriteLine("Please select an operation: ");
                string chooseInput = Console.ReadLine();

                if (int.TryParse(chooseInput, out var _chooseInput))
                {
                    switch (_chooseInput)
                    {
                        case 1:
                            await _departmentController.CreateAsync();
                            break;
                        case 2:
                            await _departmentController.GetAllAsync();
                            break;
                        case 3:
                            await _departmentController.GetByIdAsync();
                            break;
                        case 4:
                            await _departmentController.DeleteAsync();
                            break;
                        case 5:
                            await _departmentController.SearchAsync();
                            break;
                        case 6:
                            await _departmentController.UpdateAsync();
                            break;
                        case 7:
                            await _employeeController.CreateAsync();
                            break;
                        case 8:
                            await _employeeController.GetAllAsync();
                            break;
                        case 9:
                            await _employeeController.UpdateAsync();
                            break;
                        case 10:
                            await _employeeController.GetByIdAsync();
                            break;
                        case 11:
                            await _employeeController.DeleteAsync();
                            break;
                        case 12:
                            await _employeeController.GetByDepartmentIdAsync();
                            break;
                        case 13:
                            await _employeeController.GetAllDepartmentNameAsync();
                            break;
                        case 14:
                            await _employeeController.SearchAsync();
                            break;
                        case 15:
                            await _employeeController.GetEmployeesCountAsync();
                            break;
                        case 16:
                            await _employeeController.GetByAgeAsync();
                            break;
                        case 17:
                            await Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid Option, try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Format: Only numbers are allowed.");
                }
            }
        }
        private async Task Logout()
        {
            Console.WriteLine("Logging out...");

            var loginRegisterMenu = new LoginRegisterMenu();
            await loginRegisterMenu.Start();
        }

    }
}
