using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Data.Models.Users
{
    public enum SecuritySubModule
    {
        //security under 1000
        [Description("System Users")]
        SystemUsers = 1,
        [Description("Security Roles")]
        SecurityRoles = 2,
        [Description("Activity Logs")]
        AuditLog = 3,
        [Description("User Permissions")]
        Permissions = 4,
    }
}

