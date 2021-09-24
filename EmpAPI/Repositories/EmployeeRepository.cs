using EmpAPI.Context;
using EmpAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAPI.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        public async Task<bool> AddEmployee(Employee model)
        {
            try
            {
                using (var context = new EmpDbContext())
                {
                    context.Employees.Add(model);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            using (var context = new EmpDbContext())
            {
                return await context.Employees.FirstOrDefaultAsync(e => e.ID == id);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            using (var context = new EmpDbContext())
            {
                return await context.Employees.ToListAsync();
            }
        }

        public async Task<bool> DeleteEmployee(int Id)
        {
            try
            {
                using (var context = new EmpDbContext())
                {
                    var emp = context.Employees.FirstOrDefault(e => e.ID == Id);
                    context.Employees.Remove(emp);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEmployee(Employee model)
        {
            try
            {
                using (var context = new EmpDbContext())
                {
                    model.ModifiedDate = DateTime.Now;
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
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

            return false;
        }
    }
}
