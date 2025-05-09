using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.BLL.Interfaces;

namespace TrainingApp.BLL.Services
{
   
    public class BackgroundWorkflowService: IBackgroundWorkflowService
    {
        private readonly IWorkflowService _workflowService;
        public BackgroundWorkflowService
            (IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        public void UpdateWorkflowInfo()
        {
            //_workflowService.UpdateCurrencyInfoAsync();
        }

    }

}
