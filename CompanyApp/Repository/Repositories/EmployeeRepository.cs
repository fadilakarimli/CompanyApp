using Domain.Entities;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployeeWithDepartmentNameAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployeeByAge(int age)
        {
            throw new NotImplementedException();
        }
    }
}
