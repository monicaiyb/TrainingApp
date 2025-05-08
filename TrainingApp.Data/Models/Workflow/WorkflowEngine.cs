using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trainingapp.data.basemodels;
using TrainingApp.Data.Enums;
using TrainingApp.Data.Models.Users;

namespace TrainingApp.Data.Models.Workflow
{
    //public class WorkflowEngine:_Basemodel
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //}

    public class WorkflowConfiguration : _Basemodel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EmployeeProcess Process { get; set; }
        public virtual  List<WorkflowConfigurationSteps> StepsList { get; set; }
    }

    public class WorkflowConfigurationSteps : _Basemodel
    {

        public string Name { get; set; }
        [ForeignKey("WorkflowConfiguration")]
        public Guid ConfigurationId { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public int Position { get; set; }

        public virtual WorkflowConfiguration WorkflowConfiguration { get; set; }
        public virtual Role Role { get; set; }

    }
}
