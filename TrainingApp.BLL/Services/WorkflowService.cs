using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TrainingApp.BLL.Interfaces;
using TrainingApp.Data.Enums;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Models.Workflow;
using TrainingApp.Data.Repository;
using TrainingApp.Data.DTOs.WorkflowDTO;


namespace TrainingApp.BLL.Services
{
    public class WorkflowService: IWorkflowService
    {
        private readonly IDbRepository _repository;
        
        public WorkflowService(IDbRepository repository)
        {
            _repository = repository;
            
        }

        public async Task<List<WorkflowConfigDto>> GetAllConfigurations()
        {
            var workflow = await _repository.Set<WorkflowConfigDto>().ToListAsync();
            return workflow;
        }
        public async Task<bool> SaveConfiguration(WorkflowConfigDto configDto)
        {
            try
            {
                var dbConfig = await _repository.Set<WorkflowConfiguration>().FindAsync(configDto.id);
                var config = new WorkflowConfiguration
                {
                    id = configDto.id,
                    Name = configDto.Name,
                    Description = configDto.Description,
                    Process = configDto.Process
                };
                if (dbConfig == null)//new record
                {
                    await _repository.Set<WorkflowConfiguration>().AddAsync(config);
                }
                else
                {
                    await _repository.UpdateDatabaseModel(dbConfig, config);
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

        public async Task<bool> SaveConfigurationSteps(List<WorkflowConfigStepDto> newSteps, Guid configId)
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
                    foreach (var item in newSteps)
                    {
                        var step = new WorkflowConfigurationStep
                        {
                            id = item.id,
                            RoleId = item.RoleId,
                            Position = item.Position,
                            Name = item.Name
                        };
                        steps.Add(step);
                    }
                    await _repository.Set<WorkflowConfigurationStep>().AddRangeAsync(steps);
                }
                else
                {
                    foreach (var step in steps)
                    {
                        await _repository.Remove(step);
                        var newStep = new WorkflowConfigurationStep
                        {
                            id = step.id,
                            RoleId = step.RoleId,
                            Position = step.Position,
                            Name = step.Name
                        };
                        steps.Add(step);
                    }
                    _repository.Set<WorkflowConfigurationStep>().UpdateRange(steps);
                }

                await _repository.SaveChanges();

            }
            catch (Exception e)
            {
                throw ;
            }

            return true;
        }

        public async Task<bool> StartWorkflowTask(WorkflowEngineDto engineDto)
        {
            try
            {
                var dbConfig = _repository.Set<WorkflowConfiguration>()
                    .Where(r => r.id == engineDto.ConfigId).FirstOrDefault();
                if (dbConfig == null)
                {
                    throw new Exception("No workflow found");
                }

                var step = dbConfig.StepsList.OrderBy(r => r.Position).FirstOrDefault();
                var dbengine = _repository.Set<WorkflowEngine>().Where(r => r.id == engineDto.id).FirstOrDefault();
                var engine = new WorkflowEngine
                {
                    id = engineDto.id,
                    Process = engineDto.Process,
                    ConfigId = engineDto.ConfigId,
                    CurrentPosition = engineDto.CurrentPosition,
                    createdon = DateTime.Now


                };
                if (dbengine == null)
                {
                    engine.ApprovalStatus = ApprovalStatus.Pending;
                    engine.CurrentPosition = step.Position;
                    await _repository.Set<WorkflowEngine>().AddAsync(engine);
                }
                UpdateWorkflowState(engine, step,WorkflowState.Pending);
                _repository.SaveChanges();

                return true;    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public  void UpdateWorkflowState(WorkflowEngine engine, WorkflowConfigurationStep step, WorkflowState? state)
        {
             var nextStep = this._repository.Set<WorkflowConfigurationStep>()
                .Where(r => r.Position > step.Position && r.ConfigurationId == step.ConfigurationId).FirstOrDefault();
            var history = new WorkflowStateHistory
            {
                EngineId = engine.id,
                StepId = step.id,
                NextStep = nextStep.id,
                State = state,
                
                DateCreated = engine.createdon,
                EndDate = DateTime.Now

            };
            this._repository.Set<WorkflowStateHistory>().Add(history);
        }
    }
}
