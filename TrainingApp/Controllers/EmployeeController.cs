using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Data.DTOs;

namespace TrainingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        [HttpGet(Name = "Employees")]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            var 
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
