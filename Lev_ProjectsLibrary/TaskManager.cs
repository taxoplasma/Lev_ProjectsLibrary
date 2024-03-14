namespace Library
{
    public class TaskManager
    {
        private Dictionary<int, List<Task>> projectTasks;

        public TaskManager()
        {
            projectTasks = new Dictionary<int, List<Task>>();
        }

        public void AddTask(int projectId, Task task)
        {
            if (!projectTasks.ContainsKey(projectId))
            {
                projectTasks.Add(projectId, new List<Task>());
            }

            projectTasks[projectId].Add(task);
        }

        public void MarkTaskAsCompleted(int projectId, int taskId)
        {
            if (projectTasks.ContainsKey(projectId))
            {
                var task = projectTasks[projectId].FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    task.IsCompleted = true;
                }
            }
        }

        public void EditTask(int projectId, int taskId, Task updatedTask)
        {
            if (projectTasks.ContainsKey(projectId))
            {
                var taskIndex = projectTasks[projectId].FindIndex(t => t.Id == taskId);
                if (taskIndex != -1)
                {
                    projectTasks[projectId][taskIndex] = updatedTask;
                }
            }
        }

        public List<Task> GetTasksForProject(int projectId)
        {
            if (projectTasks.ContainsKey(projectId))
            {
                return projectTasks[projectId];
            }
            return new List<Task>();
        }
    }
}