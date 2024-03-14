namespace Tests
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void Task_Constructor_InitializesProperties()
        {
            var task = new Library.Task();
            Assert.IsNotNull(task);
        }
    }
}