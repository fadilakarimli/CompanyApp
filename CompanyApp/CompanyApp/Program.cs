
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
                departmentController.CreateAsync();
                break;
            case (int)ShowMenus.GetAll:
                departmentController.GetAllAsync();
                break;
            case (int)ShowMenus.GetById:
                departmentController.GetByIdAsync();
                break;
            case (int)ShowMenus.Delete:
                departmentController.DeleteAsync();
                break;
            case (int)ShowMenus.Search:
                departmentController.SearchAsync();
                break;
            //case (int)ShowMenus.Update:
            //    departmentController.;
            //    break;
            case (int)ShowMenus.CreateEmployee:
                employeeController.CreateAsync();
                break;
            case (int)ShowMenus.GetAllEmployee:
                employeeController.GetAllAsync();
                break;
            //case (int)ShowMenus.Update:
            //    employeeController();
            //    break;
            case (int)ShowMenus.GetByIdEmployee:
                employeeController.GetByIdAsync();
                break;
            case (int)ShowMenus.DeleteEmployee:
                employeeController.DeleteAsync();
                break;
            case (int)ShowMenus.GetEmployeesByDepartmentId:
                employeeController.GetByIdAsync();
                break;
            case (int)ShowMenus.GetAllEmployeesByDepartmentName:
                employeeController.GetAllDepartmentNameAsync();
                break;
            case (int)ShowMenus.SearchForEmployeesByNameOrSurname:
                employeeController.SearchAsync();
                break;
            case (int)ShowMenus.GetAllEmloyeesCount:
                employeeController.GetEmployeesCountAsync();
                break;
            case (int)ShowMenus.GetEmployeesByAge:
                employeeController.GetByAgeAsync();
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



















//using CompanyApp.Controllers;

//DepartmentController departmentController = new DepartmentController();
//await departmentController.CreateDepartmentAsync();
//await departmentController.GetAllDepartmentsAsync();
//await departmentController.DeleteDepartmentAsync();
//await departmentController.GetDepartmentByIdAsync();
//await departmentController.SearchDepartmentsAsync();

//EmployeeController employeeController = new EmployeeController();
//await employeeController.CreateAsync();
//await employeeController.GetAllDepartmentNameAsync();
//await employeeController.GetAllAsync();
//await employeeController.GetByAgeAsync();
//await employeeController.SearchAsync();
//await employeeController.GetByDepartmentIdAsync();--
//await employeeController.GetEmployeesCountAsync();
//await employeeController.GetByIdAsync();
//await employeeController.DeleteAsync();
//await employeeController.GetAllAsync();