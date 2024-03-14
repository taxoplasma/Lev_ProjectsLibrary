namespace Library
{
    public class ProjectManager
    {
        private List<Project> projects;

        public ProjectManager()
        {
            projects = new List<Project>();
        }

        public void AddProject(Project project)
        {
            projects.Add(project);
        }

        public void RemoveProject(int projectId)
        {
            projects.RemoveAll(p => p.Id == projectId);
        }

        public void EditProject(int projectId, Project updatedProject)
        {
            var projectIndex = projects.FindIndex(p => p.Id == projectId);
            if (projectIndex != -1)
            {
                projects[projectIndex] = updatedProject;
            }
        }

        public List<Project> GetProjects()
        {
            return projects;
        }
    }
}