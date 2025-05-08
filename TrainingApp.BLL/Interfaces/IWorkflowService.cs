using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Data.DTOs;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Models.Workflow;

namespace TrainingApp.BLL.Interfaces
{
    public interface IWorkflowService
    {
        Task<List<WorkflowConfiguration>> GetAllConfigurations();
        Task<bool> SaveConfiguration(WorkflowConfiguration employee);
    }
}
