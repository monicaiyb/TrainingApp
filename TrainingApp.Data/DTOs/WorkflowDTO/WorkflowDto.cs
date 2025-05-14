using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Data.Enums;

namespace TrainingApp.Data.DTOs.WorkflowDTO
{
    public class WorkflowConfigDto
    {
          public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EmployeeProcess Process { get; set; }
    }

    public class WorkflowConfigStepDto
    {
        public Guid id { get; set; }
        public string Name { get; set; }
     
        public Guid ConfigurationId { get; set; }
       
        public Guid RoleId { get; set; }
        public int Position { get; set; }
    }
    public class WorkflowEngineDto
    {
        public Guid id { get; set; }
        public int CurrentPosition { get; set; }
        [ForeignKey("WorkflowConfiguration")]
        public Guid ConfigId { get; set; }
        public Guid Record { get; set; }
        public string Process { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
    }

    public class WorkflowStateHistoryDto
    {
        public Guid id { get; set; }
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
    }
}
