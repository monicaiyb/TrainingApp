using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Data.DTOs;
using TrainingApp.Data.Models.Employee;

namespace TrainingApp.BLL.Interfaces
{
    public interface IEmployeeService
    {
     Task<List<Employee>> GetAllEmployees();
     Task<bool> SaveEmployee(Employee employee);
    }
}
