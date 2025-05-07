using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.BLL.Interfaces;
using TrainingApp.Data.DTOs;

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
        [HttpGet(Name = "Employees")]
        public async  Task<APIResponse> GetAll()
        {
            _response.Data = await _employeeService.GetAllEmployees();
            return _response;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
