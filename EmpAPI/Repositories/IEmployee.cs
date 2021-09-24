using EmpAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAPI.Repositories
{
    public  interface IEmployee
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<bool> AddEmployee(Employee model);
        Task<bool> UpdateEmployee(Employee model);
        Task<bool> DeleteEmployee(int Id);
        bool CheckValidUserKey(string reqkey);
    }
}
