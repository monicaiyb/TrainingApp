using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using TrainingApp.BLL.Interfaces;
using TrainingApp.BLL.Services;
using TrainingApp.Data.Models.Workflow;
using TrainingApp.Data.Repository;

namespace TrainingApp.Test.WorkflowServiceTest
{
    [TestClass]
    public class WorkflowTest
    {
        private readonly IWorkflowService _workflowService;

        private Mock<IDbRepository> _mockRepository;
     

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDbRepository>();

            var data = new List<WorkflowConfigurationStep>
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
            mockDbSet.As<IQueryable<WorkflowConfigurationStep>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<WorkflowConfigurationStep>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<WorkflowConfigurationStep>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<WorkflowConfigurationStep>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDbSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockDbSet.Object);

            _mockRepository.Setup(r => r.Set<WorkflowConfigurationStep>()).Returns(mockDbSet.Object);

            _workflowService = new WorkflowService(_mockRepository.Object);
        }

        [TestMethod]
        public async Task GetAllConfigurationSteps_ReturnsAllSteps()
        {
            var result = await _workflowService.GetAllConfigurationSteps();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Step 1", result[0].Name);
            Assert.AreEqual("Config A", result[0].WorkflowConfiguration.Name);
        }
    }
}
