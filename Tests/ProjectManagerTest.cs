using Library;

namespace Tests
{
    [TestFixture]
    public class ProjectManagerTests
    {
        private ProjectManager projectManager;

        [SetUp]
        public void Setup()
        {
            projectManager = new ProjectManager();
        }

        [Test]
        public void ProjectManager_AddProject_AddsProjectToList()
        {
            var project = new Project { Name = "Project 1" };
            projectManager.AddProject(project);
            Assert.Contains(project, projectManager.GetProjects());
        }
    }
}
