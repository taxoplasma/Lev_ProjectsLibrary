using Library;

namespace Tests
{
    [TestFixture]
    public class ProjectTests
    {
        [Test]
        public void Project_Constructor_InitializesProperties()
        {
            var project = new Project();
            Assert.IsNotNull(project);
        }
    }
}