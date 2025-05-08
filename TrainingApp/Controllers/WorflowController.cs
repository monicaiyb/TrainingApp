using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.BLL.Interfaces;
using TrainingApp.Data.DTOs;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Models.Workflow;

namespace TrainingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : Controller
    {
        private readonly IWorkflowService _workflowService;
        private APIResponse _response;
        public WorkflowController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
            _response = new APIResponse();
        }
    
        [HttpGet]
        [Route("AllEmployees")]
        public async  Task<APIResponse> GetAll()
        {
            _response.Data = await _workflowService.GetAllConfigurations();
            return _response;
        }
        [HttpGet]
        [Route("Employee")]
        public async Task<APIResponse> GetEmployee(Guid id)
        {
            _response.Data = await _workflowService.GetAllConfigurations();
            return _response;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<APIResponse> Post(WorkflowConfiguration configuration)
        {
            var success = await _workflowService.SaveConfiguration(configuration);
            _response.IsSuccess = success;

                return _response;
        }

    }
}
