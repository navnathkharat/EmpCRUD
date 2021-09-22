using EmpAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAPI.Repositories
{
    public  interface IEmployee
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        void AddEmployee(Employee model);
        void UpdateEmployee(Employee model);
        void DeleteEmployee(int Id);
        bool CheckValidUserKey(string reqkey);
    }
}
