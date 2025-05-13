using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Data.DTOs;
using TrainingApp.Data.DTOs.WorkflowDTO;
using TrainingApp.Data.Enums;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Models.Workflow;

namespace TrainingApp.BLL.Interfaces
{
    public interface IWorkflowService
    {
        Task<List<WorkflowConfigDto>> GetAllConfigurations();
        Task<bool> SaveConfiguration(WorkflowConfigDto config);
        Task<List<WorkflowConfigurationStep>> GetAllConfigurationSteps();
        Task<bool> SaveConfigurationSteps(List<WorkflowConfigStepDto> steps, Guid configId);
       Task<bool> StartWorkflowTask(WorkflowEngineDto engine);

       void UpdateWorkflowState(WorkflowEngine engine, WorkflowConfigurationStep step,
           WorkflowState? state);
    }
}
