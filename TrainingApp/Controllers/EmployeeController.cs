using System.DirectoryServices.Protocols;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        [HttpGet(Name = "Employees")]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            return await _accountService.GetAll();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
