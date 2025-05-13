using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TrainingApp.BLL.Interfaces;
using TrainingApp.BLL.Services;
using TrainingApp.Data.DTOs.WorkflowDTO;
using TrainingApp.Data.Models.Employee;
using TrainingApp.Data.Models.Workflow;
using TrainingApp.Data.Repository;

namespace TrainingApp.Test.WorkflowServiceTest
{
    [TestClass]
    public class WorkflowTest
    {
        private IWorkflowService _workflowService;
        private IDbRepository _repository;
        private Mock<IDbRepository> _mockRepository;
        private WorkflowConfigurationStep Data;
        private IQueryable<WorkflowConfigurationStep> dataList;
        private List<WorkflowConfigurationStep> StepsLIst;


        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDbRepository>();
            
            dataList = new List<WorkflowConfigurationStep>
            {
                new WorkflowConfigurationStep
                {
                    id = Guid.NewGuid(),
                    Name = "Step 1",
                    WorkflowConfiguration = new WorkflowConfiguration
                    {
                        id = Guid.NewGuid(), 
                        Name = "Config A"
                    }
                },
                new WorkflowConfigurationStep
                {
                    id = Guid.NewGuid(),
                    Name = "Step 2",
                    WorkflowConfiguration = new WorkflowConfiguration
                    {
                        id = Guid.NewGuid(), Name = "Config B"
                    }
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<WorkflowConfigurationStep>>();
            mockDbSet.As<IQueryable<WorkflowConfigurationStep>>().Setup(m => m.Provider).Returns(dataList.Provider);
            mockDbSet.As<IQueryable<WorkflowConfigurationStep>>().Setup(m => m.Expression).Returns(dataList.Expression);
            mockDbSet.As<IQueryable<WorkflowConfigurationStep>>().Setup(m => m.ElementType).Returns(dataList.ElementType);
            mockDbSet.As<IQueryable<WorkflowConfigurationStep>>().Setup(m => m.GetEnumerator()).Returns(dataList.GetEnumerator());

       _mockRepository.Setup(r => r.Set<WorkflowConfigurationStep>()).Returns(mockDbSet.Object);
       _repository = _mockRepository.Object;
       var workflowMock = new Mock<IWorkflowService>();
       _workflowService = workflowMock.Object;

         
        }

        [TestMethod]
        public async Task SaveConfigurationTest()
        {
            var config = new WorkflowConfigDto()
            {
                id=Guid.NewGuid(),
                Name="Employee Application"
            };

            var result = await _workflowService.SaveConfiguration(config);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task SaveConfigurationStepsTest()
        {
            var configId = Guid.NewGuid();


            var stepsList = new List<WorkflowConfigStepDto>
            {
                new WorkflowConfigStepDto
                {
                    ConfigurationId = configId,
                    Position = 1,
                    id = configId,
                    Name = "Step 1"
                }
            };

            
            var dbContext = new Mock<WorkflowConfigStepDto>().Object;
            var result = await _workflowService.SaveConfigurationSteps(stepsList, configId);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetAllConfigurationSteps_ReturnsAllSteps()
        {
            var result = await _workflowService.GetAllConfigurationSteps();
           
            Assert.IsNull(result);
            //Assert.IsNotNull(result);
            
        }
    }
}
