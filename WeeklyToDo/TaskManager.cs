﻿using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;



namespace ToDoApp
{
    public class TaskManager
    {

        public ObservableCollection<ToDoTask> Tasks { get; private set; } = new();

        public void SaveToFile(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(filePath, JsonSerializer.Serialize(Tasks, options));
        }

        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;

            string json = File.ReadAllText(filePath);
            var rawTasks = JsonSerializer.Deserialize<List<ToDoTask>>(json);

            Tasks.Clear();
            foreach (var baseTask in rawTasks)
            {
                var task = TaskFactory.CreateTaskFromUrgency(baseTask.UrgencyString);
                task.Name = baseTask.Name;
                task.Date = baseTask.Date;
                task.Location = baseTask.Location;
                task.Description = baseTask.Description;
                Tasks.Add(task);
            }
        }

        //Add Task
        public void AddTask(ToDoTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null");
            Tasks.Add(task);
        }
        //Remove Task
        public void RemoveTask(ToDoTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null");
            Tasks.Remove(task);
        }
        //Edit Task
        public void EditTask(int index, ToDoTask newTask)
        {
            if (index >= 0 && index < Tasks.Count)
            {
                if (newTask == null)
                    throw new ArgumentNullException(nameof(newTask), "New task cannot be null");
                Tasks[index] = newTask;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
            }
        }

        
        //clear form
        public void ClearForm()
        {
            foreach (var task in Tasks)
            {
                task.Name = string.Empty;
                task.Date = DateTime.Now;
                task.Location = string.Empty;
                task.Description = string.Empty;
            }
        }
        //Get Task by Urgency
        public List<ToDoTask> GetTasksByUrgency(string urgency)
        {
            return Tasks.Where(t => t.UrgencyString == urgency).ToList();
        }
        //Get Task by Date
        public List<ToDoTask> GetTasksByDate(DateTime date)
        {
            return Tasks.Where(t => t.Date.Date == date.Date).ToList();
        }
        //Get Task by Name
        public List<ToDoTask> GetTasksByName(string name)
        {
            return Tasks.Where(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
