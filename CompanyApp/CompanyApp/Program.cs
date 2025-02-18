


using CompanyApp.Controllers;

DepartmentController departmentController = new DepartmentController();
//await departmentController.CreateDepartmentAsync();
//await departmentController.GetAllDepartmentsAsync();
//await departmentController.DeleteDepartmentAsync();
//await departmentController.GetDepartmentByIdAsync();
//await departmentController.SearchDepartmentsAsync();

EmployeeController employeeController = new EmployeeController();   
//await employeeController.CreateAsync();
await employeeController.GetAllAsync();
await employeeController.GetByDepartmentNameAsync();