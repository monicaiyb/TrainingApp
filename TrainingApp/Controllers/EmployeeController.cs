using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.BLL.Interfaces;
using TrainingApp.Data.DTOs;
using TrainingApp.Data.Models.Employee;

namespace TrainingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private APIResponse _response;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _response = new APIResponse();
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public async  Task<APIResponse> GetAll()
        {
            _response.Data = await _employeeService.GetAllEmployees();
            return _response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Employee")]
        public async Task<APIResponse> GetEmployee(Guid id)
        {
            _response.Data = await _employeeService.GetAllEmployees();
            return _response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<APIResponse> AddEmployee(Employee employee)
        {
            if (employee.FirstName== null || employee.FirstName == null)
            {
                //return BadRequest();
            }
            var success = await _employeeService.SaveEmployee(employee);
            _response.IsSuccess = success;
                return _response;
        }

       

    }
}
