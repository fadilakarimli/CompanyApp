
using CompanyApp.Controllers;
using Repository.Helpers.Enums;
using System;

while (true)
{
    DepartmentController departmentController = new DepartmentController();
    EmployeeController employeeController = new EmployeeController();

    Console.WriteLine("Choose one operation : 1 - Create Department , 2 - GetAll Department , 3 - GetById Department , 4 - Delete Department," +
        "5-SearchByNameDepartment, 6 - Update Department , 7 - Create Employee, 8 - GetAll Employee , 9 - Update Employee , 10 - GetById Employee," +
        "11 - Delete Employee , 12 - GetEmployeesByDepartmentId , 13 - GetAllEmployeesByDepartmentName, 14 - SearchForEmployeesByNameOrSurname," +
        "15 - GetAllEmloyeesCount , 16 - GetEmployeesByAge");

Operation: string operation = Console.ReadLine();

    bool isCorrectOperation = int.TryParse(operation, out int selectedOperation);

    if (isCorrectOperation)
    {
        switch (selectedOperation)
        {
            case (int)ShowMenus.Create:
                await departmentController.CreateAsync();
                break;
            case (int)ShowMenus.GetAll:
                await departmentController.GetAllAsync();
                break;
            case (int)ShowMenus.GetById:
                await departmentController.GetByIdAsync();
                break;
            case (int)ShowMenus.Delete:
                await departmentController.DeleteAsync();
                break;
            case (int)ShowMenus.Search:
                await departmentController.SearchAsync();
                break;
            case (int)ShowMenus.Update:
                await departmentController.UpdateAsync();
                break;
            case (int)ShowMenus.CreateEmployee:
                await employeeController.CreateAsync();
                break;
            case (int)ShowMenus.GetAllEmployee:
                await employeeController.GetAllAsync();
                break;
            case (int)ShowMenus.UpdateEmployee:
                 await employeeController.UpdateAsync();
                break;
            case (int)ShowMenus.GetByIdEmployee:
                 await employeeController.GetByIdAsync();
                break;
            case (int)ShowMenus.DeleteEmployee:
                await employeeController.DeleteAsync();
                break;
            case (int)ShowMenus.GetEmployeesByDepartmentId:
                 await employeeController.GetByDepartmentIdAsync();
                break;
            case (int)ShowMenus.GetAllEmployeesByDepartmentName:
                await employeeController.GetAllDepartmentNameAsync();
                break;
            case (int)ShowMenus.SearchForEmployeesByNameOrSurname:
                await employeeController.SearchAsync();
                break;
            case (int)ShowMenus.GetAllEmloyeesCount:
                await employeeController.GetEmployeesCountAsync();
                break;
            case (int)ShowMenus.GetEmployeesByAge:
                await employeeController.GetByAgeAsync();
                break;
            default:
                Console.WriteLine("Operation is wrong , please try again ");
                break;
                goto Operation;

        }
    }
    else
    {
        Console.WriteLine("Operation Format is wrong ,select operation again !");
        goto Operation;
    }

}