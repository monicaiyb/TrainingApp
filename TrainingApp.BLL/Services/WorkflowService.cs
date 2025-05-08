using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingApp.BLL.Interfaces;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Models.Workflow;
using TrainingApp.Data.Repository;

namespace TrainingApp.BLL.Services
{
    public class WorkflowService: IWorkflowService
    {
        private readonly IDbRepository _repository;
        public WorkflowService(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<WorkflowConfiguration>> GetAllConfigurations()
        {
            var employees = await _repository.Set<WorkflowConfiguration>().ToListAsync();
            return employees;
        }
        public async Task<bool> SaveConfiguration(WorkflowConfiguration employee)
        {
            try
            {
                var dbEmployee = await _repository.Set<WorkflowConfiguration>().FindAsync(employee.id);

                if (dbEmployee == null)//new record
                {
                    await _repository.Set<WorkflowConfiguration>().AddAsync(employee);
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
