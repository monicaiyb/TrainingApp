using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Models.Users;

namespace TrainingApp.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<(bool, string)> Register(ApplicationUser user );
    }
}
