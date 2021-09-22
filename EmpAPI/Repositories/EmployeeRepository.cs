using EmpAPI.Context;
using EmpAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAPI.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        public void AddEmployee(Employee model)
        {
            using (var context = new EmpDbContext())
            {
                context.Employees.Add(model);
                context.SaveChanges();
            }
        }

        public void DeleteEmployee(int Id)
        {
            using (var context = new EmpDbContext())
            {
                var emp = context.Employees.FirstOrDefault(e => e.ID == Id);
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (var context = new EmpDbContext())
            {
                return context.Employees.FirstOrDefault(e => e.ID == id);
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            using (var context = new EmpDbContext())
            {
                return context.Employees.ToList();
            }
        }

        public void UpdateEmployee(Employee model)
        {
            using (var context = new EmpDbContext())
            {
                model.ModifiedDate = DateTime.Now;
                context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public bool CheckValidUserKey(string reqkey)
        {
            var userkeyList = new List<string>
            {
                "28236d8ec201df516d0f6472d516d72d",
                "38236d8ec201df516d0f6472d516d72c",
                "48236d8ec201df516d0f6472d516d72b"
            };
            if (userkeyList.Contains(reqkey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
