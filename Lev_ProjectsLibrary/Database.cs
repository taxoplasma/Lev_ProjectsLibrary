using Npgsql;

namespace Library
{
    public class Database
    {
        private string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateTables()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS projects (
                            id SERIAL PRIMARY KEY,
                            name TEXT,
                            description TEXT,
                            due_date DATE
                        );
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS tasks (
                            id SERIAL PRIMARY KEY,
                            project_id INTEGER,
                            name TEXT,
                            description TEXT,
                            is_completed BOOLEAN,
                            due_date DATE,
                            FOREIGN KEY (project_id) REFERENCES projects(id)
                        );
                    ";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddProject(Project project)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO projects (name, description, due_date) VALUES (@name, @description, @dueDate)";
                    cmd.Parameters.AddWithValue("name", project.Name);
                    cmd.Parameters.AddWithValue("description", project.Description);
                    cmd.Parameters.AddWithValue("dueDate", project.DueDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveProject(int projectId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM projects WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", projectId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditProject(int projectId, Project updatedProject)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE projects SET name = @name, description = @description, due_date = @dueDate WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", projectId);
                    cmd.Parameters.AddWithValue("name", updatedProject.Name);
                    cmd.Parameters.AddWithValue("description", updatedProject.Description);
                    cmd.Parameters.AddWithValue("dueDate", updatedProject.DueDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM projects", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projects.Add(new Project
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString(),
                            DueDate = Convert.ToDateTime(reader["due_date"])
                        });
                    }
                }
            }

            return projects;
        }

        public void AddTask(Task task)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO tasks (project_id, name, description, is_completed, due_date) VALUES (@projectId, @name, @description, @isCompleted, @dueDate)";
                    cmd.Parameters.AddWithValue("projectId", task.ProjectId);
                    cmd.Parameters.AddWithValue("name", task.Name);
                    cmd.Parameters.AddWithValue("description", task.Description);
                    cmd.Parameters.AddWithValue("isCompleted", task.IsCompleted);
                    cmd.Parameters.AddWithValue("dueDate", task.DueDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveTask(int taskId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM tasks WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditTask(int taskId, Task updatedTask)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE tasks SET project_id = @projectId, name = @name, description = @description, is_completed = @isCompleted, due_date = @dueDate WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", taskId);
                    cmd.Parameters.AddWithValue("projectId", updatedTask.ProjectId);
                    cmd.Parameters.AddWithValue("name", updatedTask.Name);
                    cmd.Parameters.AddWithValue("description", updatedTask.Description);
                    cmd.Parameters.AddWithValue("isCompleted", updatedTask.IsCompleted);
                    cmd.Parameters.AddWithValue("dueDate", updatedTask.DueDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Task> GetTasksForProject(int projectId)
        {
            List<Task> tasks = new List<Task>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM tasks WHERE project_id = @projectId", conn))
                {
                    cmd.Parameters.AddWithValue("projectId", projectId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new Task
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                ProjectId = Convert.ToInt32(reader["project_id"]),
                                Name = reader["name"].ToString(),
                                Description = reader["description"].ToString(),
                                IsCompleted = Convert.ToBoolean(reader["is_completed"]),
                                DueDate = Convert.ToDateTime(reader["due_date"])
                            });
                        }
                    }
                }
            }

            return tasks;
        }
    }
}