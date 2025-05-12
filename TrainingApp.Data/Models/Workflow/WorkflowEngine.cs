using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    

    public class WorkflowConfiguration : _Basemodel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public EmployeeProcess Process { get; set; }
        public virtual  List<WorkflowConfigurationStep> StepsList { get; set; }
    }

    public class WorkflowConfigurationStep : _Basemodel
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

    public class WorkflowEngine : _Basemodel
    {
        public int CurrentPosition { get; set; }
        [ForeignKey("WorkflowConfiguration")]
        public Guid ConfigId { get; set; }
        public Guid Record { get; set; }
        public string Process { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual WorkflowConfiguration WorkflowConfiguration { get; set; }
    }

    public class WorkflowStateHistory : _Basemodel
    {
        [ForeignKey("Engine")]
        public Guid EngineId { get; set; }
        
        public DateTime? StartDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }
        public string ExecutorUserId { get; set; }
        public WorkflowState? State { get; set; }
        public Guid? StepId { get; set; }
        public Guid NextStep { get; set; }
        public virtual WorkflowEngine Engine { get; set; }
    }
}
