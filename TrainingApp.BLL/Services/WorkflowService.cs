using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingApp.BLL.Interfaces;
using TrainingApp.Data.Enums;
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
        public async Task<List<WorkflowConfigurationStep>> GetAllConfigurationSteps()
        {
            var steps = await _repository.Set<WorkflowConfigurationStep>()
                .Include(r=>r.WorkflowConfiguration).ToListAsync();
            return steps;
        }

        public async Task<bool> SaveConfigurationSteps(List<WorkflowConfigurationStep> newSteps, Guid configId)
        {
            try
            {
                var dbConfiguration = await _repository.Set<WorkflowConfiguration>().FindAsync(configId);

                if (dbConfiguration == null)
                {
                    throw new Exception("No matching workflow config found");
                }

                var steps = _repository.Set<WorkflowConfigurationStep>()
                    .Where(r => r.ConfigurationId == dbConfiguration.id).ToList();
                if (steps.Count <= 0)//new record
                {
                    await _repository.Set<WorkflowConfigurationStep>().AddRangeAsync(newSteps);
                }
                else
                {
                    foreach (var step in steps)
                    {
                        await _repository.Remove(step);
                    }
                    _repository.Set<WorkflowConfigurationStep>().UpdateRange(newSteps);
                }

                await _repository.SaveChanges();

            }
            catch (Exception e)
            {
                throw ;
            }

            return true;
        }

        public async Task<bool> StartWorkflowTask(WorkflowEngine engine)
        {
            try
            {
                var dbConfig = _repository.Set<WorkflowConfiguration>()
                    .Where(r => r.id == engine.ConfigId).FirstOrDefault();
                if (dbConfig == null)
                {
                    throw new Exception("No workflow found");
                }

                var step = dbConfig.StepsList.OrderBy(r => r.Position).FirstOrDefault();
                var dbengine = _repository.Set<WorkflowEngine>().Where(r => r.id == engine.id).FirstOrDefault();
                if (dbengine == null)
                {
                    engine.ApprovalStatus = ApprovalStatus.Pending;
                    engine.CurrentPosition = step.Position;
                    await _repository.Set<WorkflowEngine>().AddAsync(engine);
                }
                UpdateWorkflowState(engine.id,step,WorkflowState.Pending);
                _repository.SaveChanges();

                return true;    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public void UpdateWorkflowState(Guid engineId, WorkflowConfigurationStep step, WorkflowState? state )
        {
            var nextStep=_repository.Set<WorkflowConfigurationStep>().se
        }
    }
}
