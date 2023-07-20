using Microsoft.EntityFrameworkCore;
using APIIdentity.Models;
using APIIdentity.Repository.Contract;

namespace APIIdentity.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private MyAppDBContext _dbContext;
        public EmployeeRepository(MyAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Employee AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            return employee;
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == employeeId);
            if (employee != null)
                _dbContext.Employees.Remove(employee);
        }

        public Employee GetEmployee(int employeeId)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.Id == employeeId);
        }

        public List<Employee> GetEmployees()
        {
            return _dbContext.Employees.ToList();
        }
        public Employee UpdateEmployee(int employeeId, Employee employee)
        {
            var employeeToUpdate = _dbContext.Employees.FirstOrDefault(x => x.Id == employeeId);
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Salary = employee.Salary;
            employeeToUpdate.Designation = employee.Designation;

            return employeeToUpdate;

        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
