using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.BLL.Interfaces;
using TrainingApp.Data.DTOs;
using TrainingApp.Data.DTOs.WorkflowDTO;
using TrainingApp.Data.Enums;
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

        [AllowAnonymous]
        [HttpGet]
        [Route("AllConfigurations")]
        public async  Task<APIResponse> GetAll()
        {
            _response.Data = await _workflowService.GetAllConfigurations();
            return _response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Configuration")]
        public async Task<APIResponse> GetAllWorkflowConfiguration(Guid id)
        {
            _response.Data = await _workflowService.GetAllConfigurations();
            return _response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddConfiguration")]
        public async Task<APIResponse> Post(WorkflowConfigDto configuration)
        {
            var success = await _workflowService.SaveConfiguration(configuration);
            _response.IsSuccess = success;

                return _response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddSteps")]
        public async Task<APIResponse> Post(List<WorkflowConfigStepDto> steps, Guid configId)
        {
            var success = await _workflowService.SaveConfigurationSteps(steps,configId);
            _response.IsSuccess = success;

            return _response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("AllWorkflowSteps")]
        public async Task<APIResponse> GetAllSteps()
        {
            _response.Data = await _workflowService.GetAllConfigurations();
            return _response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfigSteps")]
        public async Task<APIResponse> GetAllSteps(Guid configId)
        {
            _response.Data = await _workflowService.GetAllConfigurations();
            return _response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("StartWorkflow")]
        public async Task<APIResponse> StartWorkflow(WorkflowEngineDto engine)
        {
            _response.Data = await _workflowService.StartWorkflowTask(engine);
            return _response;
        }

    }
}
