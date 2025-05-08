using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingApp.BLL.Interfaces;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Repository;

namespace TrainingApp.BLL.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IDbRepository _repository;
        public EmployeeService(IDbRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _repository.Set<Employee>().ToListAsync();
            return  employees;
        }
        public async Task<bool> SaveEmployee(Employee employee)
        {
            try
            {
                var dbEmployee = await _repository.Set<Employee>().FindAsync(employee.id);
                
                if (dbEmployee == null)//new record
                {
                    await _repository.Set<Employee>().AddAsync(employee);
                }
                else
                {
                    await _repository.UpdateDatabaseModel(dbEmployee, employee);
                }
               
              

                await _repository.SaveChanges();
              
            }
            catch (Exception e) 
            {
               
                throw;
            }

            return true;
        }
    
        

    }
}
